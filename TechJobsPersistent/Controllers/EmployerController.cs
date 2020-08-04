using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechJobsPersistent.Data;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechJobsPersistent.Controllers
{
    public class EmployerController : Controller
    {
        private JobDbContext context;

        public EmployerController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Employer> employers = context.Employers.ToList();
            return View(employers);            
        }

        public IActionResult Add() //Display form to let users add an employer.
        {
            AddEmployerViewModel addEmployerViewModel = new AddEmployerViewModel();
            return View(addEmployerViewModel);
        }

        public IActionResult ProcessAddEmployerForm(Employer employer)
        {
            if (ModelState.IsValid)
            {
                context.Employers.Add(employer);
                context.SaveChanges();
                return Redirect("/Employer/");
            }

            return View("Add", employer);
        }


        public IActionResult About(int id)
        {
            Employer employer = context.Employers
                .Where(e => e.Id == id)
                .Include(e => e.Name)
                .Include(e => e.Location)
                .First();

            return View(employer);
        }


        public IActionResult AddJob( AddJobViewModel addJobViewModel)
        {
            return View(addJobViewModel);
        }
    }
}
