using System;
using System.Collections.Generic;

namespace QuanLyThietBi
{
    // Kho luu tru trong bo nho, dung List cho don gian
    public class InMemoryRepository<T> : IRepository<T> where T : IIdentifiable
    {
        private readonly List<T> items = new List<T>();

        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Khong cho phep them phan tu null");

            // TODO: chua kiem tra trung Id, dang add luon
            items.Add(item);
        }

        public bool Remove(string id)
        {
            T found = FindById(id);
            if (found == null)
                return false;

            return items.Remove(found);
        }

        public T FindById(string id)
        {
            foreach (T item in items)
            {
                if (item.Id == id)
                    return item;
            }
            return default(T); // khong tim thay
        }

        public IReadOnlyList<T> GetAll()
        {
            // TODO: dang tra ve collection noi bo, ma ngoai co the sua duoc
            return items;
        }

        public IReadOnlyList<T> FindAll(Predicate<T> condition)
        {
            List<T> result = new List<T>();
            foreach (T item in items)
            {
                if (condition(item))
                    result.Add(item);
            }
            return result;
        }
    }
}