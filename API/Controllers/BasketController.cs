using  System;
using System.Security.Cryptography.X509Certificates;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extentions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;



namespace API.Controllers;


public class BasketController(StoreContext context) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<BasketDto>> GetBasket()
    {
        var basket = await RetrieveBasket();

        if(basket == null) return NoContent();

       return basket.ToDto();

    }


    [HttpPost]
    public async Task<ActionResult<BasketDto>> AddItemToBasket(int productId, int quantity)
    {
        // get basket
         var basket = await RetrieveBasket();
        //create basket

        basket ??= CreateBasket();
        // get product

        var product =  await context.Products.FindAsync(productId);
        if (product ==null) return BadRequest("Problem adding item to basket");

        // add item to basket

        basket.AddItem(product, quantity);

       
        // save changes
      var result = await context.SaveChangesAsync() > 0;

       if ( result  == true) return CreatedAtAction(nameof(GetBasket), basket.ToDto());




        return BadRequest("Problem updating basket");


    }

    

    public async Task<ActionResult> RemoveBasketItem(int productId, int quantity)
    {

         var basket = await RetrieveBasket();

         if(basket == null) return BadRequest("Unable to retrieve basket");

         basket.RemoveItem(productId, quantity);

         var result = await context.SaveChangesAsync() > 0;

       if ( result  == true) return Ok();
        return BadRequest("Problem Updating Basket");
    }



    private async Task<Basket?> RetrieveBasket()
    {
       return await context.Baskets
                   .Include(x => x.Items)
                          .ThenInclude(x => x.Product)
                          .FirstOrDefaultAsync(x => x.BasketId == Request.Cookies["basketId"]);
    }


    private Basket CreateBasket()
    {
        var basketId = Guid.NewGuid().ToString();
        var CookieOptions = new CookieOptions
        {
            IsEssential = true, 
            Expires = DateTime.UtcNow.AddDays(30)
        };
        Response.Cookies.Append("basketId", basketId, CookieOptions);
        var basket  = new Basket{BasketId  = basketId};

        context.Baskets.Add(basket);
        return basket;
    }

}