using PatientTracker.Business.Abstract;
using PatientTracker.DataAccess;
using PatientTracker.DataAccess.Abstract;
using PatientTracker.Entities;
using PatientTracker.Entities.Helpers;
using PatientTracker.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PatientTracker.Business.Concrete
{
    public class PatientManager : IPatientService
    {

        private IPatientRepository _patientRepository;


        public PatientManager( IPatientRepository patientRepository)
        {

            _patientRepository = patientRepository;
        }

        public async Task<Patient> AddPatient(Patient patient)
        {
            await _patientRepository.AddPatient(patient);
            return patient;
        }


        public async Task DeletePatient(int id)
        {
            await _patientRepository.DeletePatient(id);

        }

        public  PagedList<Patient> GetPatients(PatientParameters patientParameters)
        {
             return _patientRepository.GetPatients(patientParameters);
             
        }

        public async Task<Patient> GetPatientById(int id)
        {
            return await _patientRepository.GetPatientById(id);
        }
        public async Task<Patient> UpdatePatient(Patient patient)
        {
            await _patientRepository.UpdatePatient(patient);
            return patient;

        }

    }
}

