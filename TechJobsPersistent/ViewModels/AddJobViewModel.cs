using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TechJobsPersistent.Models;

namespace TechJobsPersistent.ViewModels
{
    public class AddJobViewModel
    {
        [Required(ErrorMessage = "Job's name is required")]
        public string JobName { get; set; }

        [Required(ErrorMessage = "Employer's Id is required")]
        public int EmployerId { get; set; }

        public List<Skill> AllSkills { get; set; }

        public Job Job { get; set; }

        public List<SelectListItem> AllEmployers { get; set; }

        public AddJobViewModel(Job newJob, List<Employer> allEmployers, List<Skill> allSkills)
        {
            AllEmployers = new List<SelectListItem>();
            
            foreach (var employer in allEmployers)
            {
                AllEmployers.Add(new SelectListItem
                {
                    Value = employer.Id.ToString(),
                    Text = employer.Name
                });
            }

            AllSkills = allSkills;
            Job = newJob;
        }

        public AddJobViewModel()
        {
        }
    }   
}
