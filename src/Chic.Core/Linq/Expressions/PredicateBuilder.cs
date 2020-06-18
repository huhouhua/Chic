using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using System.Reflection;

namespace Chic.Core.Linq.Expressions
{
    /// <summary>
    /// 运算符
    /// </summary>
    public enum PredicateOperator
    {
        /// <summary> 或者 </summary>
        Or,

        /// <summary> 并且 </summary>
        And
    }


    public static class PredicateBuilder
    {
        private class RebindParameterVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _oldParameter;
            private readonly ParameterExpression _newParameter;

            public RebindParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
            {
                _oldParameter = oldParameter;
                _newParameter = newParameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                if (node == _oldParameter)
                {
                    return _newParameter;
                }

                return base.VisitParameter(node);
            }
        }

        /// <summary>使用true或false创建表达式，以便在表达式尚未启动时使用 默认为false </summary>
        public static ExpressionStarter<T> New<T>(Expression<Func<T, bool>> expr = null) { return new ExpressionStarter<T>(expr); }

        /// <summary>
        /// 使用true或false创建表达式，以便在表达式尚未启动时使用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="defaultExpression"></param>
        /// <returns></returns>
        public static ExpressionStarter<T> New<T>(bool defaultExpression) { return new ExpressionStarter<T>(defaultExpression); }

        /// <summary>
        /// 建立Or查询条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="leftExpression">左表达式</param>
        /// <param name="rightExpression">右表达式</param>
        /// <returns>组合后的表达式</returns>
        public static Expression<Func<T, bool>> Or<T>([NotNull] this Expression<Func<T, bool>> leftExpression, [NotNull] Expression<Func<T, bool>> rightExpression)
        {
            var expr2Body = new RebindParameterVisitor(rightExpression.Parameters[0], leftExpression.Parameters[0]).Visit(rightExpression.Body);
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(leftExpression.Body, expr2Body), leftExpression.Parameters);
        }

        /// <summary>
        /// 建立And查询条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="leftExpression">左表达式</param>
        /// <param name="rightExpression">右表达式</param>
        /// <returns>组合后的表达式</returns>
        public static Expression<Func<T, bool>> And<T>([NotNull] this Expression<Func<T, bool>> leftExpression, [NotNull] Expression<Func<T, bool>> rightExpression)
        {
            var expr2Body = new RebindParameterVisitor(rightExpression.Parameters[0], leftExpression.Parameters[0]).Visit(rightExpression.Body);
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(leftExpression.Body, expr2Body), leftExpression.Parameters);
        }

        /// <summary>
        /// 扩展 Or / And 
        /// </summary>
        /// <typeparam name="T">当前 类型</typeparam>
        /// <param name="first">当前表达式</param>
        /// <param name="second">要组合的表达式</param>
        /// <param name="operator">组合类型 ("And" 或者 "Or") 默认 Or 使用PredicateOperator枚举</param>
        /// <returns>组合后的表达式</returns>
        public static Expression<Func<T, bool>> Extend<T>([NotNull] this Expression<Func<T, bool>> first, [NotNull] Expression<Func<T, bool>> second, PredicateOperator @operator = PredicateOperator.Or)
        {
            return @operator == PredicateOperator.Or ? first.Or(second) : first.And(second);
        }

        /// <summary>
        /// 扩展 Or / And  指定的表达式运算符
        /// </summary>
        /// <typeparam name="T">当前 类型</typeparam>
        /// <param name="first">当前表达式</param>
        /// <param name="second">要组合的表达式</param>
        /// <param name="operator">组合类型 ("And" 或者 "Or") 默认 Or 使用PredicateOperator枚举</param>
        /// <returns>组合后的表达式</returns>
        public static Expression<Func<T, bool>> Extend<T>([NotNull] this ExpressionStarter<T> first, [NotNull] Expression<Func<T, bool>> second, PredicateOperator @operator = PredicateOperator.Or)
        {
            return @operator == PredicateOperator.Or ? first.Or(second) : first.And(second);
        }


        /// <summary>
        /// 条件表达式且（第一个true才执行第二个）
        /// </summary>
        /// <param name="left">左表达式</param>
        /// <param name="right">右表达式</param>
        /// <returns></returns>
        public static Expression AndAlso(this Expression left, Expression right)
        {
            return Expression.AndAlso(left, right);
        }

        /// <summary>
        /// 创建一个回调带有参数方法的表达式
        /// </summary>
        /// <param name="instance">表达式</param>
        /// <param name="methodName">方法名字</param>
        /// <param name="arguments">参数</param>
        /// <returns></returns>
        public static Expression Call(this Expression instance, string methodName, params Expression[] arguments)
        {
            return Expression.Call(instance, instance.Type.GetMethod(methodName), arguments);
        }

        /// <summary>
        /// 创建一个比较表达式
        /// </summary>
        /// <param name="left">左表达式</param>
        /// <param name="right">右表达式</param>
        /// <returns></returns>
        public static Expression GreaterThan(this Expression left, Expression right)
        {
            return Expression.GreaterThan(left, right);
        }

        /// <summary>
        /// 创建一个访问属性的表达式
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns></returns>
        public static Expression Property(this Expression expression, string propertyName)
        {
            return Expression.Property(expression, propertyName);
        }

        /// <summary>
        /// 获取表达式对象属性名称
        /// </summary>
        /// <typeparam name="IEntity"></typeparam>
        /// <typeparam name="IEntityKey"></typeparam>
        /// <param name="keyExpression"></param>
        /// <returns>返回属性名称</returns>
        public static string GetPropertyName<IEntity, IEntityKey>(this Expression<Func<IEntity, IEntityKey>> keyExpression)
            where IEntity : class
        {
            var body = keyExpression.Body;
            switch (keyExpression.Body.NodeType)
            {
                case ExpressionType.MemberAccess:
                    return ((MemberExpression)body).Member.Name;
                case ExpressionType.Convert:
                    return ((MemberExpression)((UnaryExpression)body).Operand).Member.Name;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 获取表达式对象属性名称
        /// </summary>
        /// <typeparam name="IEntity"></typeparam>
        /// <param name="keyExpression"></param>
        /// <returns></returns>
        public static string GetPropertyName<IEntity>(this Expression<Func<IEntity, object>> keyExpression)
        {
            var body = keyExpression.Body;
            switch (keyExpression.Body.NodeType)
            {
                case ExpressionType.MemberAccess:
                    return ((MemberExpression)body).Member.Name;
                case ExpressionType.Convert:
                    return ((MemberExpression)((UnaryExpression)body).Operand).Member.Name;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 获取表达式对象属性
        /// </summary>
        /// <typeparam name="IEntity"></typeparam>
        /// <typeparam name="IEntityKey"></typeparam>
        /// <param name="keyExpression"></param>
        /// <returns></returns>
        public static PropertyInfo GetProperty<IEntity, IEntityKey>(this Expression<Func<IEntity, IEntityKey>> keyExpression)
            where IEntity : class
        {
            var type = typeof(IEntity);
            var property = type.GetProperty(keyExpression.GetPropertyName());
            return property;
        }
        /// <summary>
        /// 获取表达式对象属性
        /// </summary>
        /// <typeparam name="IEntity"></typeparam>
        /// <typeparam name="IEntityKey"></typeparam>
        /// <param name="keyExpression"></param>
        /// <returns></returns>
        public static PropertyInfo GetProperty<IEntity, IEntityKey>(this Expression<Func<IEntity, object>> keyExpression)
            where IEntity : class
        {
            var type = typeof(IEntity);
            var property = type.GetProperty(keyExpression.GetPropertyName());
            return property;
        }

        /// <summary>
        /// Lambda表达式
        /// </summary>
        /// <typeparam name="TEntity">类型</typeparam>
        /// <param name="objectExpr">表达式</param>
        /// <returns></returns>
        public static LambdaExpression ToLambdaExpression<TEntity>(this Expression<Func<TEntity, object>> objectExpr)
        {
            MemberExpression operand = null;
            var body = (objectExpr.Body as UnaryExpression);
            var parameters = objectExpr.Parameters.ToList();
            if (body == null)
            {
                operand = (objectExpr.Body as MemberExpression);
            }
            else
            {
                operand = body.Operand as MemberExpression;
            }
            var lambdaExpression = Expression.Lambda(operand, parameters);
            return lambdaExpression;
        }

        /// <summary>
        /// Lambda表达式
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="body">表达式</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public static Expression<TEntity> ToLambda<TEntity>(this Expression body, params ParameterExpression[] parameters)
        {
            return Expression.Lambda<TEntity>(body, parameters);
        }

        public static Expression<Func<T, bool>> ToNot<T>(this Expression<Func<T, bool>> expression)
        {
            return Expression.Lambda<Func<T, bool>>(Expression.Not(expression.Body), expression.Parameters);
        }

    }

    public class ExpressionStarter<T>
    {
        public ExpressionStarter() : this(false) { }

        public ExpressionStarter(bool defaultExpression)
        {
            if (defaultExpression)
                DefaultExpression = f => true;
            else
                DefaultExpression = f => false;
        }

        public ExpressionStarter(Expression<Func<T, bool>> exp) : this(false)
        {
            _predicate = exp;
        }

        /// <summary>通过调用Start来设置</summary>
        private Expression<Func<T, bool>> Predicate => (IsStarted || !UseDefaultExpression) ? _predicate : DefaultExpression;

        private Expression<Func<T, bool>> _predicate;

        /// <summary>确定谓词是否已启动。</summary>
        public bool IsStarted => _predicate != null;

        /// <summary> 表达式为空时使用的默认表达式 </summary>
        public bool UseDefaultExpression => DefaultExpression != null;

        /// <summary>默认表达式</summary>
        public Expression<Func<T, bool>> DefaultExpression { get; set; }

        /// <summary>设置表达式谓词</summary>
        /// <param name="exp">当前表达式</param>
        public Expression<Func<T, bool>> Start(Expression<Func<T, bool>> exp)
        {
            if (IsStarted)
                throw new Exception("Predicate cannot be started again.");

            return _predicate = exp;
        }

        /// <summary>Or</summary>
        public Expression<Func<T, bool>> Or([NotNull] Expression<Func<T, bool>> expr2)
        {
            return (IsStarted) ? _predicate = Predicate.Or(expr2) : Start(expr2);
        }

        /// <summary>And</summary>
        public Expression<Func<T, bool>> And([NotNull] Expression<Func<T, bool>> expr2)
        {
            return (IsStarted) ? _predicate = Predicate.And(expr2) : Start(expr2);
        }


        public override string ToString()
        {
            return Predicate == null ? null : Predicate.ToString();
        }

        #region Implicit Operators
        /// <summary>
        /// 将此对象隐式转换为表达式
        /// </summary>
        /// <param name="right"></param>
        public static implicit operator Expression<Func<T, bool>>(ExpressionStarter<T> right)
        {
            return right == null ? null : right.Predicate;
        }

        /// <summary>
        /// 将此对象隐式转换为委托
        /// </summary>
        /// <param name="right"></param>
        public static implicit operator Func<T, bool>(ExpressionStarter<T> right)
        {
            return right == null ? null : (right.IsStarted || right.UseDefaultExpression) ? right.Predicate.Compile() : null;
        }

        /// <summary>
        /// 将此对象隐式转换为表达式
        /// </summary>
        /// <param name="right"></param>
        public static implicit operator ExpressionStarter<T>(Expression<Func<T, bool>> right)
        {
            return right == null ? null : new ExpressionStarter<T>(right);
        }
        #endregion

        #region 实现Expression<TDelagate>方法和属性
#if !(NET35)

        /// <summary></summary>
        public Func<T, bool> Compile() { return Predicate.Compile(); }
#endif

#if !(NET35 || WINDOWS_APP || NETSTANDARD || PORTABLE || PORTABLE40 || UAP)
        /// <summary></summary>
        public Func<T, bool> Compile(DebugInfoGenerator debugInfoGenerator) { return Predicate.Compile(debugInfoGenerator); }

        /// <summary></summary>
        public Expression<Func<T, bool>> Update(Expression body, IEnumerable<ParameterExpression> parameters) { return Predicate.Update(body, parameters); }
#endif
        #endregion

# region  实现LamdaExpression方法和属性

        /// <summary></summary>
        public Expression Body => Predicate.Body;


        /// <summary></summary>
        public ExpressionType NodeType => Predicate.NodeType;

        /// <summary></summary>
        public ReadOnlyCollection<ParameterExpression> Parameters => Predicate.Parameters;

        /// <summary></summary>
        public Type Type => Predicate.Type;

#if !(NET35)
        /// <summary></summary>
        public string Name => Predicate.Name;

        /// <summary></summary>
        public Type ReturnType => Predicate.ReturnType;

        /// <summary></summary>
        public bool TailCall => Predicate.TailCall;
#endif

#if !(NET35 || WINDOWS_APP || NETSTANDARD || PORTABLE || PORTABLE40 || UAP)
        /// <summary></summary>
        public void CompileToMethod(MethodBuilder method) { Predicate.CompileToMethod(method); }

        /// <summary></summary>
        public void CompileToMethod(MethodBuilder method, DebugInfoGenerator debugInfoGenerator) { Predicate.CompileToMethod(method, debugInfoGenerator); }

#endif
        #endregion

        #region 实现表达式方法和属性
#if !(NET35)
        /// <summary></summary>
        public virtual bool CanReduce => Predicate.CanReduce;
#endif
        #endregion
    }

}
