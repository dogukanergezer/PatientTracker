using Microsoft.EntityFrameworkCore;
using PatientTracker.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PatientTracker.DataAccess.Concrete
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected PatientDbContext patientDbContext { get; set; }

        public RepositoryBase(PatientDbContext patientDbContext)
        {
            this.patientDbContext = patientDbContext;
        }

      
        public IQueryable<T> FindAll()
        {

            return patientDbContext.Set<T>().AsNoTracking();

        }
      
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return patientDbContext.Set<T>()
                        .Where(expression)
                        .AsNoTracking();
        }
    }
}
