using Relive.Server.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Relive.Server.Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification() { }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public Expression<Func<T, object>> GroupBy { get; private set; }

        protected virtual void AddInclude(Expression<Func<T, object>> include) 
        {
            Includes.Add(include);
        }

        protected virtual void AddInclude(string include)
        {
            IncludeStrings.Add(include);
        }

        protected virtual void AddOrderBy(Expression<Func<T, object>> orderBy)
        {
            OrderBy = orderBy;
        }

        protected virtual void AddOrderByDesc(Expression<Func<T, object>> orderByDesc)
        {
            OrderByDescending = orderByDesc;
        }

        protected virtual void AddGroupBy(Expression<Func<T, object>> groupBy)
        {
            GroupBy = groupBy;
        }
    }
}
