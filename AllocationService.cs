using System;
using System.Collections.Generic;

namespace QuanLyThietBi
{
    // Dich vu cap phat thiet bi, phoi hop repository + chien luoc chon
    public class AllocationService<T> where T : TeachingDevice
    {
        private readonly IRepository<T> repository;
        private ISelectionStrategy<T> strategy;

        public AllocationService(IRepository<T> repository, ISelectionStrategy<T> strategy)
        {
            this.repository = repository;
            this.strategy = strategy;
        }

        public void ChangeStrategy(ISelectionStrategy<T> newStrategy)
        {
            this.strategy = newStrategy;
        }

        public T Allocate(Predicate<T> additionalCondition)
        {
            // 1. lay tat ca tu repository
            IReadOnlyList<T> all = repository.GetAll();

            // 2+3. loc thiet bi san sang va dieu kien them
            List<T> ready = new List<T>();
            foreach (T device in all)
            {
                if (device.IsReadyForUse())
                {
                    if (additionalCondition == null || additionalCondition(device))
                        ready.Add(device);
                }
            }

            // 4. dung chien luoc hien tai de chon
            T selected = strategy.Select(ready);

            // 5. muon thiet bi duoc chon
            selected.Borrow();

            // 6. tra ve thiet bi da cap phat
            return selected;
        }
    }
}