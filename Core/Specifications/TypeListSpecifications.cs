using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class TypeListSpecifications:BaseSpecification<Product ,string>
    {
        public TypeListSpecifications()
        {
            AddSelect(x=> x.Type);
            ApplyDistinct();
        }
    }
}
