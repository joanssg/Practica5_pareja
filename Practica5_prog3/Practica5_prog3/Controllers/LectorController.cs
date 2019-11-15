using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Practica5_prog3.Models;

namespace Practica5_prog3.Controllers
{
    public class LectorController : Controller
    {
        // GET: Lector
        public ActionResult Index()
        {
            return View(new List<Customer_Lector>());
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            List<Customer_Lector> customers = new List<Customer_Lector>();
            string filePath = string.Empty;
            if(postedFile != null){

                string path = Server.MapPath("/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                string csvData = System.IO.File.ReadAllText(filePath);
                foreach(String row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        customers.Add(new Customer_Lector
                        {

                            Id = row.Split(',')[0],
                            Nombre = row.Split(',')[1],
                            Pais = row.Split(',')[2]
                        });
                    }
                }
            }
            return View(customers);
        }
    }
}