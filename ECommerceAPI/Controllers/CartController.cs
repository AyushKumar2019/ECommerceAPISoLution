﻿using Core.Entities;
using Core.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    public class CartController(ICartService cartService) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ShoppingCart>> GetCartById(string id)
        {
            var cart = await cartService.GetCartAsync(id);
            return Ok(cart ?? new ShoppingCart { Id=id});
        }
        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> SetCart(ShoppingCart cart)
        {
            var updatedCart = await cartService.SetCartAsync(cart);
            if(updatedCart == null) return BadRequest("Problem with the cart");
            return Ok(updatedCart);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteCart(string id)
        {
            var result = await cartService.DeleteCartAsync(id);
            if(!result) return BadRequest("Problem deleting the cart");

            return Ok();
        }
    }
}
