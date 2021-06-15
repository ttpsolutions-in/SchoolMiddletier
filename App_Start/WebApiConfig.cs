using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Cors;
using System.Web.Http.Cors;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using schools.Models;

namespace schools
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //var cors = new EnableCorsAttribute("http://localhost:4200", "*", "*");
            //config.EnableCors(cors);
            //Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //var cors = new EnableCorsAttribute("http://localhost:4200", "*", "*");
            //config.EnableCors(cors);

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));


            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Page>("Pages");
            builder.EntitySet<PageHistory>("PageHistories");
            builder.EntitySet<PhotoGallery>("PhotoGalleries");
            builder.EntitySet<Message>("Messages");
            builder.EntitySet<FilesNPhoto>("FilesNPhotoes");
            builder.EntitySet<MasterData>("MasterDatas");
            builder.EntitySet<ClassFee>("ClassFees");
            builder.EntitySet<Student>("Students");
            builder.EntitySet<StudentClass>("StudentClasses");
            builder.EntitySet<StudentFeePayment>("StudentFeePayments");
            builder.EntitySet<PaymentDetail>("PaymentDetails");
            builder.EntitySet<StudentFeeReceipt>("StudentFeeReceipts");
            builder.EntitySet<StudentDocument>("StudentDocuments");
            builder.EntitySet<TaskConfiguration>("TaskConfigurations");
            builder.EntitySet<AppUser>("AppUsers");
            builder.EntitySet<Application>("Applications");
            builder.EntitySet<ApplicationRole>("ApplicationRoles");
            builder.EntitySet<RoleUser>("RoleUsers");
            builder.EntitySet<Attendance>("Attendances");
            builder.EntitySet<Organization>("Organizations");            
            builder.EntitySet<ClassSubject>("ClassSubjects");
            builder.EntitySet<Exam>("Exams");            
            builder.EntitySet<ExamSlot>("ExamSlots");
            builder.EntitySet<ExamStudentClass>("ExamStudentClasses");
            builder.EntitySet<SlotAndClassSubject>("SlotAndClassSubjects");
            builder.EntitySet<ExamStudentSubject>("ExamStudentSubjects");
            builder.EntitySet<StudentClassSubject>("StudentClassSubjects");
            builder.EntitySet<StudentActivity>("StudentActivities");
            builder.EntitySet<SubjectType>("SubjectTypes");
            builder.EntitySet<ClassSubjectMarkComponent>("ClassSubjectMarkComponents");

            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());


            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;

            config.Formatters.Remove(config.Formatters.XmlFormatter);

           

        }
    }
}
