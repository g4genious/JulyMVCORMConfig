using JulyMVCORMConfig.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JulyMVCORMConfig.Controllers
{
    public class FirstController:Controller
    {
        July2018MVCContext _ORM = null;
        IHostingEnvironment _ENV = null;





        public FirstController(July2018MVCContext ORM, IHostingEnvironment ENV)
        {
            _ORM = ORM;
            _ENV = ENV;
        }


        [HttpGet]
        public IActionResult RegisterStudent()
        {
            return View();
        }


        [HttpPost]
        public IActionResult RegisterStudent(Student S, IFormFile PP, IFormFile Resume)
        {
            string wwwRootPath = _ENV.WebRootPath;
            string FTPPathForPPs = wwwRootPath + "/WebData/PPs/";

            //if (!Directory.Exists(FTPPathForPPs))
            //{
            //    Directory.CreateDirectory(FTPPathForPPs);
            //}


            string UniqueName = Guid.NewGuid().ToString();
            string FileExtension =  Path.GetExtension(PP.FileName);

            FileStream FS = new FileStream(FTPPathForPPs + UniqueName+FileExtension,FileMode.Create);

            PP.CopyTo(FS);






            FS.Close();

            S.ProfilePicture = "/WebData/PPs/" + UniqueName + FileExtension;

            string CVPath = "/WebData/CVs/" + Guid.NewGuid().ToString() + Path.GetExtension(Resume.FileName);
            Resume.CopyTo(new FileStream(wwwRootPath+CVPath, FileMode.Create));
            S.CV = CVPath;


            _ORM.Student.Add(S);
            _ORM.SaveChanges();
            ViewBag.Message = "Registration is done.";
            return View();
        }




        public IActionResult Index()
        {

            Student S = new Student();
            S.Name = "Usman";

            Teacher T = new Teacher();
            T.Name = "T1";

            _ORM.Student.Add(S);
            _ORM.Teacher.Add(T);



            _ORM.SaveChanges();




            return View();
        }


        public FileResult DownloadCV(string Path)
        {
            if(string.IsNullOrEmpty(Path))
            {
                ViewBag.Message = "Invalid Path";
                return null;
            }
            return File(Path, new MimeSharp.Mime().Lookup(Path),DateTime.Now.ToString("ddMMyyyyhhmmss")+System.IO.Path.GetExtension(Path));
        }






        public IActionResult AllStudents()
        {
            return View( _ORM.Student.ToList<Student>());
        }




    }
}
