using System.Collections.Generic;

namespace QuanLyThietBi
{
    // Chon phan tu dau tien trong danh sach
    public class FirstAvailableStrategy<T> : ISelectionStrategy<T>
    {
        public T Select(IReadOnlyList<T> candidates)
        {
            if (candidates == null || candidates.Count == 0)
                throw new NoSuitableItemException();

            return candidates[0];
        }
    }
}