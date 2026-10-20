using System;
using System.Collections.Generic;

namespace QuanLyThietBi
{
    // Kho luu tru tong quat, chi lam viec voi doi tuong co Id
    public interface IRepository<T> where T : IIdentifiable
    {
        void Add(T item);
        bool Remove(string id);
        T FindById(string id);
        IReadOnlyList<T> GetAll();
        IReadOnlyList<T> FindAll(Predicate<T> condition);
    }
}