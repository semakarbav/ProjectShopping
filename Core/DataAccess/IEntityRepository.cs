using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null); //filtre zorunlu değil,tüm datayı getirir
        T Get(Expression<Func<T, bool>> filter);  //filtre zorunlu,idye göre getirir
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
