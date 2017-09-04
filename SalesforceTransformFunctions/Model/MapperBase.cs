using AutoMapper;
using AutoMapper.Configuration;

namespace SalesforceTransformFunctions.Model
{
    public class MapperBase
    {
        protected MapperConfigurationExpression mappings;

        /// <summary>
        /// Default constructor. Initializes the MapperConfigurationExpression variable
        /// </summary>
        public MapperBase()
        {
            mappings = new MapperConfigurationExpression();

        }

        /// <summary>
        /// Creates an instance of IMapper
        /// </summary>
        /// <returns></returns>
        protected IMapper CreateMap()
        {
            Mapper.Initialize(mappings);
            var config = new MapperConfiguration(mappings);
            IMapper mapper = new AutoMapper.Mapper(config);
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
            return mapper;
        }
    }
}