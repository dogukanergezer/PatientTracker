using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PatientTracker.Entities;
using PatientTracker.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientTracker.DataAccess
{
    public class PatientDbContext : DbContext
    {
        public PatientDbContext(DbContextOptions<PatientDbContext> options):base(options)
        {

        }
      

        public DbSet<Patient> Patients{ get; set; }

    }
}
