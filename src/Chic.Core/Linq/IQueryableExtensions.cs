using Chic.Core.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace System.Linq
{
    public static class IQueryableExtensions
    {
        private static readonly MethodInfo OrderByMethod;
        private static readonly MethodInfo OrderByDescendingMethod;
        private static readonly MethodInfo ThenByMethod;
        private static readonly MethodInfo ThenByDescendingMethod;
        static IQueryableExtensions()
        {
            var type = typeof(Queryable);
            OrderByMethod = type.GetMethods().Where(q => q.Name == "OrderBy" && q.IsGenericMethodDefinition && q.GetParameters().Length == 2 && q.GetGenericArguments().Length == 2)
                .FirstOrDefault();

            OrderByDescendingMethod = type.GetMethods().Where(q => q.Name == "OrderByDescending" && q.IsGenericMethodDefinition && q.GetParameters().Length == 2 && q.GetGenericArguments().Length == 2)
                .FirstOrDefault();

            ThenByMethod = type.GetMethods().Where(q => q.Name == "ThenBy" && q.IsGenericMethodDefinition && q.GetParameters().Length == 2 && q.GetGenericArguments().Length == 2)
                .FirstOrDefault();

            ThenByDescendingMethod = type.GetMethods().Where(q => q.Name == "ThenByDescending" && q.IsGenericMethodDefinition && q.GetParameters().Length == 2 && q.GetGenericArguments().Length == 2)
                .FirstOrDefault();
        }
        public static IOrderedQueryable<IEntity> ApplyOrder<IEntity>(this IQueryable<IEntity> source
            , IDictionary<Expression<Func<IEntity, object>>, bool> dictOrder)
        {
            #region OrderBy
            IOrderedQueryable<IEntity> orderedQueryable = null;
            if (dictOrder == null || dictOrder.Count == 0)
            {
                orderedQueryable = source as IOrderedQueryable<IEntity>;
            }
            else
            {
                foreach (var item in dictOrder)
                {
                    LambdaExpression lambdaExpr = item.Key.ToLambdaExpression();
                    MethodInfo method = null;
                    object obj = null;
                    if (orderedQueryable == null)
                    {
                        method = item.Value ? OrderByMethod : OrderByDescendingMethod;
                        obj = method.MakeGenericMethod(lambdaExpr.Parameters[0].Type, lambdaExpr.Body.Type).Invoke(null, new object[] { source, lambdaExpr });
                    }
                    else
                    {
                        method = item.Value ? OrderByMethod : OrderByDescendingMethod;
                        obj = method.MakeGenericMethod(lambdaExpr.Parameters[0].Type, lambdaExpr.Body.Type).Invoke(null, new object[] { orderedQueryable, lambdaExpr });
                    }
                    orderedQueryable = obj as IOrderedQueryable<IEntity>;
                }
            }
            #endregion
            return orderedQueryable;
        }
    }
}
