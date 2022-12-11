using CompaniesApplication.Application.Utilities.Contracts;

namespace CompaniesApplication.Application.Utilities;

public class BubbleSortAlgorithm<T1, T2> : ISortingAlgorithm<T1, T2> where T1: class where T2: Enum
{
    public IList<T1> Sort(IList<T1> source, Func<T1, T1, T2, bool> swappingPredicate, T2 sortingField)
    {
        var sourceLength = source.Count();

        for (int i = 0; i < sourceLength; i++)
        {
            for (int j = 0; j < sourceLength - i - 1; j++)
            {
                if (swappingPredicate(source[j], source[j + 1], sortingField))
                {
                    (source[j], source[j + 1]) = (source[j + 1], source[j]);
                }
            }
        }

        return source;
    }
}