using System.Collections.Generic;

namespace QuanLyThietBi
{
    // Chien luoc chon mot doi tuong tu danh sach ung vien
    public interface ISelectionStrategy<T>
    {
        T Select(IReadOnlyList<T> candidates);
    }
}