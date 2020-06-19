using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;

namespace Chic.Core.Specifications
{
    public static class ExpressionFuncExtender
    {

        /// <summary>
        /// 组合表达式
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="first">当前表达式</param>
        /// <param name="second">要组合的表达式</param>
        /// <param name="merge">组合条件</param>
        /// <returns></returns>
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // 构建参数映射（从第二个参数到第一个参数）
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // 用第一个lambda表达式中的参数替换第二个lambda表达式中的参数
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // 将lambda表达式体的组合应用于第一个表达式中的参数
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        /// <summary>
        /// 建立Or查询条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="leftExpression">左表达式</param>
        /// <param name="rightExpression">右表达式</param>
        /// <returns>组合后的表达式</returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.AndAlso);
        }


        /// <summary>
        /// 建立And查询条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="leftExpression">左表达式</param>
        /// <param name="rightExpression">右表达式</param>
        /// <returns>组合后的表达式</returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.OrElse);
        }
    }
}
