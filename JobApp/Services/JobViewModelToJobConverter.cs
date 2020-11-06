using System;
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
        _context = context;
    }

    public async Task<Job> Convert(JobViewModel jobViewModel)
    {
        Job job = new Job();

        Publisher publisherFromViewModel = await _context.Publisher
            .Where(publisher => publisher.ID == jobViewModel.PublisherId)
            .FirstAsync();

        List<JobSkill> jobSkillsFromViewModel = new List<JobSkill>();
        foreach (string JobSkillsId in jobViewModel.JobSkillsId)
        {
            Skill foundJobSKill = await _context.Skill.Where(jobSkill => jobSkill.Name == JobSkillsId).FirstAsync();
            JobSkill jobSkill = new JobSkill
            {
                Skill = foundJobSKill,
                Job = job
            };
            jobSkillsFromViewModel.Add(jobSkill);
        }

        List<CityJob> jobCitiesFromViewModel = new List<CityJob>();
        foreach (string cityName in jobViewModel.JobCities)
        {
            City foundJobCity = await _context.City.Where(jc => jc.Name == cityName).FirstAsync();
            CityJob jobCity = new CityJob
            {
                City = foundJobCity,
                Job = job
            };
            jobCitiesFromViewModel.Add(jobCity);
        }

        job.Description = jobViewModel.Description;
        job.Title = jobViewModel.Title;
        job.Publisher = publisherFromViewModel;
        job.JobSkills = jobSkillsFromViewModel;
        job.JobCities = jobCitiesFromViewModel;
        job.Lon = jobViewModel.Lon;
        job.Lat = jobViewModel.Lat;
        return job;
    }

    public JobViewModel Convert(Job job)
    {
        JobViewModel jvm = new JobViewModel();

        jvm.ID = job.ID;
        jvm.PublisherId = job.PublisherId;
        jvm.Title = job.Title;
        jvm.Description = job.Description;
        jvm.LastEdited = job.LastEdited;
        jvm.Lon = job.Lon;
        jvm.Lat = job.Lat;
        jvm.JobSeekers = job.JobSeekers;

        jvm.JobSkillsId = new List<string>();
        foreach (JobSkill js in job.JobSkills)
        {
            jvm.JobSkillsId.Add(js.SkillName);
        }

        jvm.JobCities = new List<string>();
        foreach (CityJob cj in job.JobCities)
        {
            jvm.JobCities.Add(cj.CityName);
        }

        return jvm;
    }
}
