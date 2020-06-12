using AutoMapper;

namespace CurrencyExchange.Business.Services.Mapping
{
    public class ModelMapper
    {
        private readonly IMapper _mapper;
        public ModelMapper(IMapper mapper)
        {
            _mapper = mapper;
        }
        public static MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.AuditModel, Data.Models.Audit>().ReverseMap();
                cfg.CreateMap<Data.Models.Core.BaseEntity, Models.Core.BaseEntityModel>();
            }
            );
            return config;
        }
    }
}
