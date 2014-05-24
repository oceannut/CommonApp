using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.Common.Data
{

    public class GenericDao<T> : IDao<T>
    {

        public virtual void Save(T entity)
        {
            
        }

        public virtual void Save(ICollection<T> col)
        {
            
        }

        public virtual bool Update(T entity)
        {
            return false;
        }

        public virtual void Update(ICollection<T> col)
        {
            
        }

        public virtual bool Delete(T entity)
        {
            return false;
        }

        public virtual void Delete(ICollection<T> col)
        {
            
        }

        public virtual T Get(object id)
        {
            return default(T);
        }

        public virtual bool IsExist(object id)
        {
            return false;
        }

        public virtual IList<T> Find(params object[] values)
        {
            return new List<T>();
        }

    }

}
