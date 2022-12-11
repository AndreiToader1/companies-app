namespace CompaniesApplication.Application.Utilities.Contracts;

public interface ISortingAlgorithm<T1, T2> where T1: class where T2: Enum
{
    IList<T1> Sort(IList<T1> source, Func<T1, T1, T2, bool> swappingPredicate, T2 sortingField);
}