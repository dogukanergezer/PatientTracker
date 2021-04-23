using PatientTracker.DataAccess.Abstract;
using PatientTracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using PatientTracker.Entities.Models;
using PatientTracker.Entities.Helpers;

namespace PatientTracker.DataAccess.Concrete
{
    public class PatientRepository : RepositoryBase<Patient>, IPatientRepository
    {
        private ISortHelper<Patient> _sortHelper;
        public PatientRepository(PatientDbContext patientDbContext, ISortHelper<Patient> sortHelper) : base(patientDbContext)
        {
            _sortHelper = sortHelper;
        }
        public PagedList<Patient> GetPatients(PatientParameters patientParameters)
        {
            var patients = FindByCondition(o =>(!string.IsNullOrEmpty(patientParameters.Genus)? o.Genus == patientParameters.Genus:true)
                &&(!string.IsNullOrEmpty(patientParameters.OwnerInfo) ? o.OwnerInfo == patientParameters.OwnerInfo : true)
                && (!string.IsNullOrEmpty(patientParameters.Diagnosis) ? o.Diagnosis == patientParameters.Diagnosis : true));

            SearchByName(ref patients, patientParameters.Name);

            var sortedPatients = _sortHelper.ApplySort(patients, patientParameters.OrderBy);

            return PagedList<Patient>.ToPagedList(sortedPatients,
                patientParameters.PageNumber,
                patientParameters.PageSize);

        }

        private void SearchByName(ref IQueryable<Patient> patients, string name)
        {
            if (!patients.Any() || string.IsNullOrWhiteSpace(name))
                return;
            patients = (IOrderedQueryable<Patient>)patients.Where(o => o.Name.ToLower().Contains(name.Trim().ToLower()));
        }


        public async Task<Patient> AddPatient(Patient patient)
        {

            patientDbContext.Patients.Add(patient);
            await patientDbContext.SaveChangesAsync();
            return patient;

        }
        public async Task<Patient> UpdatePatient(Patient patient)
        {
            patientDbContext.Entry<Patient>(patient).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
            await patientDbContext.SaveChangesAsync();
            return patient;

        }
        public async Task DeletePatient(int id)
        {

            var deletedPatient = await GetPatientById(id);
            patientDbContext.Patients.Remove(deletedPatient);
            await patientDbContext.SaveChangesAsync();

        }
        public async Task<Patient> GetPatientById(int id)
        {

            var patient = await patientDbContext.Patients.FindAsync(id);
            return patient;

        }

    }
}

