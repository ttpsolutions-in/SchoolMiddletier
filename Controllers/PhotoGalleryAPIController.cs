using Newtonsoft.Json.Linq;
using schools.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace StPauls.Controllers
{
    public class PhotoGalleryAPIController : ApiController
    {
        public void LoadImage(string imgPath,string imgstr)
        {
            //data:image/gif;base64,
            //this image is a single pixel (black)
            ////byte[] bytes = Convert.FromBase64String(imgstr);

            ////Image image;
            ////using (MemoryStream ms = new MemoryStream(bytes))
            ////{
            ////    image = Image.FromStream(ms);
            ////}

            ////return image;
            File.WriteAllBytes(imgPath, Convert.FromBase64String(imgstr));
        }
        [HttpPost]
        [Route("api/saveimage")]
         public HttpResponseMessage something()
        {
            string imageName = "";
            var httpRequest = HttpContext.Current.Request;
            var postedFile = httpRequest.Files["Image"];
            var postedFileBytes = Convert.FromBase64String(postedFile.ToString());
            //if (postedFile...Contains(","))
            //{
            //    theFile.FileAsBase64 = theFile.FileAsBase64.Substring(theFile.FileAsBase64.IndexOf(",") + 1);
            //}
            imageName = new string(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);

            var filePath = HttpContext.Current.Server.MapPath("~/Uploads/Images/" + imageName);

            using (var fs = new FileStream(filePath, FileMode.CreateNew))
            {
                fs.Write(postedFileBytes, 0, postedFileBytes.Length);
            }
            
            //postedFileBytes.SaveAs(filePath);
            ////IImageData imageData ;
            //dynamic obj = Request.Content.ReadAsAsync<JObject>();
            //var y = obj.Result;
            TTPEntities db = new TTPEntities();
            //byte[] bytes = Encoding.ASCII.GetBytes(y.filebytes.Value);
            ////string path = @"~/Uploads/Stpauls/" + y.album.Value + "/" + y.filename.Value + ".png";
            //string path = @"D:\projects\stpauls\Uploads\" + y.album.Value + "\\" + y.filename.Value;
            ////Image image;
            ////using (MemoryStream ms = new MemoryStream(bytes))
            ////{
            ////    image = Image.FromStream(ms);
            ////}

            ////image.Save(path, System.Drawing.Imaging.ImageFormat.Png);
            //File.WriteAllBytes(path, bytes);

            PhotoGallery photoGallery = new PhotoGallery();
            photoGallery.PhotoPath =  "/Uploads/Images/" +imageName;
            photoGallery.Active = 1;
            photoGallery.UploadDate = DateTime.Now;
            photoGallery.AlbumId = Convert.ToInt16(httpRequest["album"]);// y.album.Value;
            db.PhotoGalleries.Add(photoGallery);
            db.SaveChangesAsync();

            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [HttpPost]
        public HttpResponseMessage CropAndSaveImage(string moduleName, string fileName, int x, int y, int w, int h)
        {
            TTPEntities db = new TTPEntities();
            //Create the Directory.
            string path = HttpContext.Current.Server.MapPath("~/Uploads/" + moduleName + "/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Save the Files.
            foreach (string key in HttpContext.Current.Request.Files)
            {
                HttpPostedFile postedFile = HttpContext.Current.Request.Files[key];
                postedFile.SaveAs(path + fileName);

                string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads/" + moduleName), fileName);
                string cropFileName = "";
                string cropFilePath = "";
                if (File.Exists(filePath))
                {
                    System.Drawing.Image orgImg = System.Drawing.Image.FromFile(filePath);
                    Rectangle CropArea = new Rectangle(x, y, w, h);
                    try
                    {
                        Bitmap bitMap = new Bitmap(CropArea.Width, CropArea.Height);
                        using (Graphics g = Graphics.FromImage(bitMap))
                        {
                            g.DrawImage(orgImg, new Rectangle(0, 0, bitMap.Width, bitMap.Height), CropArea, GraphicsUnit.Pixel);
                        }
                        cropFileName = "crop_" + fileName;
                        cropFilePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads/" + moduleName), cropFileName);
                        bitMap.Save(cropFilePath);

                        PhotoGallery photoGallery = new PhotoGallery();
                        photoGallery.PhotoPath = @"~/ Uploads /" + moduleName + "/" + cropFileName;
                        photoGallery.Active = 1;
                        photoGallery.UploadDate = DateTime.Now;
                        photoGallery.AlbumId = 1;
                        db.PhotoGalleries.Add(photoGallery);
                        db.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }

            //Send OK Response to Client.
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
public interface IImageData
{
    string filename { get; set; }
    string album { get; set; }
    string filebytes { get; set; }
}

