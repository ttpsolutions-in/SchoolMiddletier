using schools.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Configuration;

namespace StPauls.Controllers
{
    public class ImageController : ApiController
    {
        string errorPath = ConfigurationManager.AppSettings["dev"];
        [HttpPost]
        [Route("api/uploadimage")]
        public HttpResponseMessage UploadImage()
        {
            string response = "";
            int parentId = 0;
            string imageName = null;
            StringBuilder sb = new StringBuilder();
            var httpRequest = HttpContext.Current.Request;
            var batch = httpRequest["batch"]==null?"0": httpRequest["batch"];
            var folderName = httpRequest["folderName"];
            var fileOrPhoto = httpRequest["fileOrPhoto"];
            var description = httpRequest["description"];
            var postedFile = httpRequest.Files["image"];
            var StudentId = 0;
            var StudentClassId = 0;
            var DocTypeId = 0;
            var PageId = 0;
            if (httpRequest["StudentId"] != null)
                StudentId = Convert.ToInt32(httpRequest["StudentId"]);
            if (httpRequest["StudentClassId"] != null)
                StudentClassId = Convert.ToInt32(httpRequest["StudentClassId"]);
            if (httpRequest["DocTypeId"] != null)
                DocTypeId = Convert.ToInt32(httpRequest["DocTypeId"]);
            if (httpRequest["PageId"] != null)
                PageId = Convert.ToInt32(httpRequest["PageId"]);

            if (httpRequest["parentId"] != null)
                parentId = Convert.ToInt16(httpRequest["parentId"]);

            FilesNPhoto fileNPhoto = null;
            using (StpaulsEntities db = new StpaulsEntities())
            {
                if (parentId == 0)
                {
                    fileNPhoto = new FilesNPhoto()
                    {
                        UpdatedFileFolderName = folderName,
                        FileOrFolder = 1,//Convert.ToByte(fileOrFolder),
                        FileOrPhoto = Convert.ToByte(fileOrPhoto),
                        FileName = folderName,
                        Active = 1,
                        ParentId = 0
                    };
                    fileNPhoto = db.FilesNPhotos.Add(fileNPhoto);
                    db.SaveChanges();
                    parentId = fileNPhoto.FileId;
                }
            }
            var fileDir = HttpContext.Current.Server.MapPath("~/Image/" + folderName);
            var photoPath = "Image/" + folderName;

            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }

            try
            {
                imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(20).ToArray()).Replace(" ", "-");
                imageName = imageName + DateTime.Now.ToString("yymmssfff")+ Path.GetExtension(postedFile.FileName);
                response = imageName;

                var filepath = fileDir + "/" + imageName;
                postedFile.SaveAs(filepath);
                //browsePath = photoPath + "/" + imageName;
                using (StpaulsEntities db = new StpaulsEntities())
                {
                    //StudentId = Convert.ToInt32(StudentId);
                    if (StudentId > 0)
                    {
                        Student student = db.Students.First(s => s.StudentId == StudentId);
                        student.Photo = imageName;
                        db.SaveChanges();
                    }
                    else if (PageId > 0)
                    {
                        Page page = db.Pages.First(s => s.PageId == PageId);
                        page.PhotoPath = imageName;
                        db.SaveChanges();
                    }
                    else
                    {
                        FilesNPhoto file = new FilesNPhoto()
                        {
                            ParentId = parentId,
                            Description = description,
                            FileName = imageName,
                            UpdatedFileFolderName = imageName,
                            FileOrPhoto = Convert.ToByte(fileOrPhoto),
                            FileOrFolder = 0,
                            Batch = Convert.ToInt16(batch),
                            StudentClassId = Convert.ToInt32(StudentClassId),
                            DocTypeId = Convert.ToInt16(DocTypeId),
                            Active = 1,
                            UploadDate = DateTime.Now,
                            CreatedDate = DateTime.Now
                        };
                        db.FilesNPhotos.Add(file);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                
                File.AppendAllText(errorPath, e.StackTrace);
                //File.AppendAllText(@"D:\ProjectGit\stpauls\Uploads\log.txt", e.Message);
            
                throw e;
            }
            //}
            return Request.CreateResponse(response);
        }

        [HttpPost]
        [Route("api/uploadimages")]
        public HttpResponseMessage UploadImages()
        {
            string imageName = null;
            //string filePath = @"D:\projects\stpauls\Uploads\";
            StringBuilder sb = new StringBuilder();
            var httpRequest = HttpContext.Current.Request;
            var folderName = httpRequest["folderName"];
            //var fileOrFolder = httpRequest["fileOrFolder"];
            var fileOrPhoto = httpRequest["fileOrPhoto"];
            var fileName = httpRequest["fileName"];
            var description = httpRequest["description"] == null ? "" : httpRequest["description"];
            //var postedFile = httpRequest.Files["image"];
            var parentId = 0;
            if (httpRequest["parentId"] != null)
                parentId = Convert.ToInt16(httpRequest["parentId"]);

            try
            {
                FilesNPhoto fileNPhoto = null;
                using (StpaulsEntities db = new StpaulsEntities())
                {
                    if (parentId == 0)
                    {
                        fileNPhoto = new FilesNPhoto()
                        {
                            UpdatedFileFolderName = folderName,
                            FileOrFolder = 1,
                            FileOrPhoto = Convert.ToByte(fileOrPhoto),
                            FileName = folderName,
                            Active = 1,
                            ParentId = 0
                        };
                        fileNPhoto = db.FilesNPhotos.Add(fileNPhoto);
                        db.SaveChanges();
                        parentId = fileNPhoto.FileId;
                    }
                }

                var fileDir = HttpContext.Current.Server.MapPath("~/Image/" + folderName);
                var photoPath = "Image/" + folderName;
                if (!Directory.Exists(fileDir))
                {
                    Directory.CreateDirectory(fileDir);
                }
                using (StpaulsEntities db = new StpaulsEntities())
                {
                    foreach (string fName in httpRequest.Files)
                    {
                        try
                        {
                            var postedFile = httpRequest.Files[fName];
                            imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).ToArray()).Replace(" ", "-");
                            imageName = imageName + Path.GetExtension(postedFile.FileName);
                            var filepath = fileDir + "/" + imageName;
                            postedFile.SaveAs(filepath);

                            FilesNPhoto file = new FilesNPhoto()
                            {
                                ParentId = parentId,
                                Description = description,
                                FileName = imageName,
                                UpdatedFileFolderName = imageName,
                                FileOrFolder = 0,
                                FileOrPhoto = Convert.ToByte(fileOrPhoto),
                                Active = 1,
                                UploadDate = DateTime.Now,
                                CreatedDate = DateTime.Now
                            };
                            //File.AppendAllText(@"D:\ProjectGit\stpauls\Uploads\log.txt", "\n" + albumId.ToString()+ ":" + DateTime.Now);
                            db.FilesNPhotos.Add(file);
                            db.SaveChanges();

                        }
                        catch (Exception e)
                        {
                            File.AppendAllText(errorPath, e.StackTrace);
                            //File.AppendAllText(@"D:\ProjectGit\stpauls\Uploads\log.txt", e.Message);
                            throw e;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                File.AppendAllText(errorPath, e.StackTrace);
                //File.AppendAllText(@"D:\ProjectGit\stpauls\Uploads\log.txt", e.Message);
                throw e;
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
