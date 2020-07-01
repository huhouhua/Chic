using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chic.Core.ObjectMapping
{
    public static class INeedMapperExtensions
    {
        public static TDestination MapTo<TDestination>(this INeedMapper item) where TDestination : class, new()
        {
            return ObjectMapperFactory.GetObjectMapper().MapTo<TDestination>(item);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source)
            where TSource : INeedMapper
            where TDestination : class, new()
        {
            return ObjectMapperFactory.GetObjectMapper().MapTo<TSource, TDestination>(source,null);
        }


        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
           where TSource : INeedMapper
           where TDestination : class, new()
        {
            return ObjectMapperFactory.GetObjectMapper().MapTo(source, destination);
        }

        public static object MapTo(this object source, Type sourceType, Type destinationType)
        {
            return ObjectMapperFactory.GetObjectMapper().MapTo(source,sourceType,destinationType);
        }


        public static IEnumerable<TDestination> MapToCollection<TSource, TDestination>(this IEnumerable<TSource> list)
            where TSource : INeedMapper
            where TDestination : class, new()
        {
            return ObjectMapperFactory.GetObjectMapper().MapToCollection<TSource, TDestination>(list);
        }

        public static IQueryable<TDestination> ProjectTo<TDestination>(this IQueryable source)
         where TDestination : class, new()
        {
            return ObjectMapperFactory.GetObjectMapper().ProjectTo<TDestination>(source);
        }





    }
}
