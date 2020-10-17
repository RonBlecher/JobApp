using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobApp.Data;
using JobApp.Models;
using Microsoft.EntityFrameworkCore;

public class JobViewModelToJobConverter
{
    private readonly JobAppContext _context;

    public JobViewModelToJobConverter(JobAppContext context)
    {
        this._context = context;
    }

    public Job Convert(JobViewModel jobViewModel)
    {
        Job job = new Job();

        var publishers = await _context.Publisher.Where(publisher => publisher.ID == jobViewModel.PublisherId).ToListAsync();
        Publisher publisherFromViewModel = publishers[0];
        List<JobSkill> jobSkillesFromViewModel = new List<JobSkill>();
        foreach (string JobSkillsId in jobViewModel.JobSkillsId)
        {
            Skill foundJobSKill = (await _context.Skill.Where(jobSkill => jobSkill.Name == JobSkillsId).ToListAsync())[0];
            JobSkill jobSkill = new JobSkill
            {
                Skill = foundJobSKill,
                Job = job
            };
            _context.Add(jobSkill);
            jobSkillesFromViewModel.Add(jobSkill);
        }
        job.Description = jobViewModel.Description;
        job.Title = jobViewModel.Title;
        job.Publisher = publisherFromViewModel;
        job.JobSkills = jobSkillesFromViewModel;
        job.Lon = jobViewModel.Lon;
        job.Lat = jobViewModel.Lat;

        return job;
    }
}