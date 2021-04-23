using PatientTracker.Entities;
using PatientTracker.Entities.Helpers;
using PatientTracker.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PatientTracker.Business.Abstract
{
    public interface IPatientService
    {
        PagedList<Patient> GetPatients(PatientParameters patientParameters);

        Task<Patient> GetPatientById(int id);

        Task<Patient> AddPatient(Patient patient);

        Task<Patient> UpdatePatient(Patient patient);

        Task DeletePatient(int id);


    }
}