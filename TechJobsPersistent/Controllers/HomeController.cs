using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext context;

        public HomeController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        [HttpGet("/Add")]  //added instance of AddJobViewModel
        public IActionResult AddJob() //Display form to let users add a Job.
        {
            List<Skill> skills = context.Skills.ToList();
            List<Employer> employers = context.Employers.ToList();
            AddJobViewModel addJobViewModel = new AddJobViewModel(null, employers, skills);
            return View(addJobViewModel);
        }

        [HttpPost]
        public IActionResult ProcessAddJobForm(AddJobViewModel addJobViewModel, string[] selectedSkills) //added instance of AddJobViewModel
        {
            if (ModelState.IsValid)
            {
                Job job = new Job
                {
                    Name = addJobViewModel.JobName,
                    EmployerId = addJobViewModel.EmployerId,
                    JobSkills = new List<JobSkill>()
                };

                foreach (string skillId in selectedSkills)
                {
                    JobSkill jobSkill = new JobSkill()
                    { 
                        //JobId = job.Id,
                        Job = job,
                        SkillId = int.Parse(skillId)
                    };

                    context.JobSkills.Add(jobSkill);
                }

                context.Jobs.Add(job);
                context.SaveChanges();
                return Redirect("/");
            }

            return View(addJobViewModel);
        }

        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }
    }
}
