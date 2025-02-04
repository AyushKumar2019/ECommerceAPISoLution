using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class BrandListSpecifications : BaseSpecification<Product, string>
    {
        public BrandListSpecifications()
        {
            AddSelect(x => x.Brand);
            ApplyDistinct();
        }
    }
}
