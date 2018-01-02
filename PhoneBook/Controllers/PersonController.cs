using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Services;
using PhoneBook.Models;
using System.Data;
using System.Data.SqlClient;
using PagedList;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhoneBook.Controllers
{
    public class PersonController : Controller
    {
        protected SourceManager SourceManager;
        string connectionString = @"Data Source=.\\SQLEXPRESS;Initial Catalog=PhoneBook;Integrated Security=True";


        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            List<PersonModel> list = new List<PersonModel>();
            list = SourceManager.GetAllPerson().ToList();
            
            
            return View(list);
        }
        public IActionResult Test()
        {
            List<PersonModel> list = new List<PersonModel>();
            list = SourceManager.GetAllPerson().ToList();
            return View(list);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PersonModel personModel)
        {
            SourceManager.Add(personModel);
            return RedirectToAction("Index");
        }


     
        public PersonController()
        {
            
            SourceManager = new SourceManager();
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            var s = SourceManager.GetByID(id);
            return View(s);
        }
        [HttpPost]
        public IActionResult ConfirmRemove(int id)
        {
            SourceManager.Remove(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var s = SourceManager.GetByID(id);
            return View(s);
        }
        [HttpPost]
        public IActionResult Update(PersonModel personModel)
        {
            SourceManager.Update(personModel);
            return RedirectToAction("Index");
        }
    




    }

}
