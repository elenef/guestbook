using AutoMapper;

namespace GuestBook.Mapper
{
    public class ContractMapper : IContractMapper
    {
        private static IMapper _mapper;

        public ContractMapper()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        public TItem2 Map<TItem1, TItem2>(TItem1 source)
            where TItem1 : class
            where TItem2 : class
        {
            return _mapper.Map<TItem1, TItem2>(source);
        }

        public void Map<TItem1, TItem2>(TItem1 source, TItem2 destination)
            where TItem1 : class
            where TItem2 : class
        {
            _mapper.Map(source, destination);
        }
    }
}
