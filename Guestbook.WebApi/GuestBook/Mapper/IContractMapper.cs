namespace GuestBook.Mapper
{
    public interface IContractMapper
    {
        TItem2 Map<TItem1, TItem2>(TItem1 source)
            where TItem1 : class where TItem2 : class;
        void Map<TItem1, TItem2>(TItem1 source, TItem2 destination)
            where TItem1 : class where TItem2 : class;
    }
}
