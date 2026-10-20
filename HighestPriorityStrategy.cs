using System;
using System.Collections.Generic;

namespace QuanLyThietBi
{
    // Chon doi tuong co diem uu tien cao nhat
    public class HighestPriorityStrategy<T> : ISelectionStrategy<T>
    {
        private readonly Func<T, double> scoreSelector;

        public HighestPriorityStrategy(Func<T, double> scoreSelector)
        {
            this.scoreSelector = scoreSelector;
        }

        public T Select(IReadOnlyList<T> candidates)
        {
            if (candidates == null || candidates.Count == 0)
                throw new NoSuitableItemException();

            // TODO: chua cai dung, dang tra ve phan tu dau tien thay vi phan tu cao nhat
            return candidates[0];
        }
    }
}