using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IObjectMapper = Chic.Core.ObjectMapping.IObjectMapper;

namespace Chic.Core.ObjectMapping
{
    public class AutoMapperObjectMapper : IObjectMapper
    {
        protected readonly IMapper Mapper;

        public AutoMapperObjectMapper(IMapper mapper)
        {
            Mapper = mapper;
        }

        public TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            return Mapper.Map(source, sourceType, destinationType);
        }


        public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source)
        {
            return Mapper.ProjectTo<TDestination>(source);
        }




    }
}
