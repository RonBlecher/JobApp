using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobApp.Data;
using JobApp.Models;

public class JobViewModelToJobConverter
{
    private readonly JobAppContext _context;

    public JobViewModelToJobConverter(JobAppContext context)
    {
        this._context = context;
    }

    public async Task<Job> Convert(JobViewModel jobViewModel)
    {
        Job job = new Job();

        var publishers = _context.Publisher.Where(publisher => publisher.ID == jobViewModel.PublisherId).ToList();
        Publisher publisherFromViewModel = publishers[0];
        List<JobSkill> jobSkillesFromViewModel = new List<JobSkill>();
        foreach (string JobSkillsId in jobViewModel.JobSkillsId)
        {
            Skill foundJobSKill = _context.Skill.Where(jobSkill => jobSkill.Name == JobSkillsId).ToList()[0];
            JobSkill jobSkill = new JobSkill();
            jobSkill.Skill = foundJobSKill;
            jobSkill.Job = job;
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