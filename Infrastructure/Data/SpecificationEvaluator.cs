using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<T> where T:BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
        {
            if (spec.Criteria != null)
            { 
                query = query.Where(spec.Criteria);//x=> a.Brand == brand
            }

            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);//x=> a.Brand == brand
            }

            if (spec.OrderByDesc != null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);//x=> a.Brand == brand
            }
            if (spec.IsDistinct)
            { 
                query = query.Distinct();
            }
            return query;
        }

        public static IQueryable<TResult> GetQuery<TSpec, TResult>(IQueryable<T> query,
            ISpecification<T,TResult> spec)
        {
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);//x=> a.Brand == brand
            }

            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);//x=> a.Brand == brand
            }

            if (spec.OrderByDesc != null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);//x=> a.Brand == brand
            }
            var selectQuery = query as IQueryable<TResult>;
            if (spec.Select != null)
            {
                selectQuery = query.Select(spec.Select);
            }
            if(spec.IsDistinct)
            {
                selectQuery = selectQuery?.Distinct();
            }

            return selectQuery ?? query.Cast<TResult>();
        }

    }
}
