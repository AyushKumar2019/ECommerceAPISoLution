﻿using Core.Entities;
using ECommerceAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    public class BuggyController:BaseApiController
    {
        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorized()
        { 
            return Unauthorized();
        }
        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest("Not A good Request");
        }
        [HttpGet("notfound")]
        public IActionResult GetNotFound()
        {
            return NotFound();
        }
        [HttpGet("internalerror")]
        public IActionResult GetInternalError()
        {
            throw new Exception("This is a Test Exception");
        }
        [HttpPost("validationerror")]
        public IActionResult GetValidationError(CreateProductDto product)
        {
            return Ok(product);
        }
    }
}
