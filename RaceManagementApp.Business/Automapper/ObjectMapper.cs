using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace RaceManagementApp.Business.Automapper
{
    public class ObjectMapper
    {
        private readonly IMapper _mapper;

        public ObjectMapper(IMapper mapper)
        {
            _mapper = mapper;
        }
        public TDestination MapObject<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TDestination>(source);
        }

        public List<TDestination> MapList<TSource, TDestination>(List<TSource> sourceList)
        {
            return _mapper.Map<List<TDestination>>(sourceList);
        }
    }
}
