using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chic.Core.ObjectMapping
{
    public interface IObjectMapper
    {
        /// <summary>
        /// 将一个对象转换为另一个对象。创建的新对象 <typeparamref name="TDestination"/>.
        /// </summary>
        /// <typeparam name="TDestination">目标对象的类型</typeparam>
        /// <param name="source">源对象</param>
        TDestination Map<TDestination>(object source);

        /// <summary>
        /// 从源对象到现有目标对象的映射
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <param name="source">源对象</param>
        /// <param name="destination">目标对象</param>
        /// <returns><paramref name="destination"/> 映射后的对象</returns>
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);


        /// <summary>
        /// 从源对象到现有目标对象的映射
        /// </summary>
        /// <param name="source">源对象</param>
        /// <param name="sourceType">源类型</param>
        /// <param name="destinationType">目标类型</param>
        /// <returns><paramref name="object"/> 映射后的对象</returns>
        object Map(object source, Type sourceType, Type destinationType);


        /// <summary>
        /// 将一个对象转换为另一个对象。创建的新对象 Queryable
        /// </summary>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <param name="source">源Queryable</param>
        IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source);
    }
}
