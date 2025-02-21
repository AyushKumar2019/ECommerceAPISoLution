using Core.Entities;
using Core.Interfaces;
using ECommerceAPI.RequestHelpers;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected async Task<ActionResult> CreatePagedResult<T>(IGenricRepository<T> repo ,
            ISpecification<T> spec , int pageIndex , int pageSize) where T:BaseEntity
        { 
            var items = await repo.ListAsync(spec);
            var count = await repo.CountAsync(spec);

            var pagination = new Pagination<T>(pageIndex,pageSize, count,items);
            return Ok(pagination);
        }
    }
}
