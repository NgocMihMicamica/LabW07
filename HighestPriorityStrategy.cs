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

            // FIX lan 2: tim dung phan tu co diem cao nhat
            T best = candidates[0];
            double bestScore = scoreSelector(candidates[0]);

            for (int i = 1; i < candidates.Count; i++)
            {
                double score = scoreSelector(candidates[i]);
                if (score > bestScore)
                {
                    bestScore = score;
                    best = candidates[i];
                }
            }

            return best;
        }
    }
}
