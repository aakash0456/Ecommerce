
using API.Controllers;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AccountController(SignInManager<User> signInManager) : BaseApiController
{
    [HttpPost("register")]

    public async Task<ActionResult> RegisterUser(RegisterDto registerDto)
    {

        var user = new User{UserName = registerDto.Email, Email = registerDto.Email};
        var result = await signInManager.UserManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded) 
        {
            foreach (var error in result.Errors) 
            {
                ModelState.AddModelError("errors", error.Description);
            }
            return ValidationProblem();
        }

        await signInManager.UserManager.AddToRoleAsync(user, "Member");
await signInManager.SignInAsync(user, isPersistent: false); // ✅ This issues the cookie
return Ok();

    }
    
   
    [HttpGet("user-info")]
    public async Task<ActionResult> GetUserInfo()
    {
       if(User.Identity?.IsAuthenticated != true) return NoContent();
       var user = await signInManager.UserManager.GetUserAsync(User);
       if(user == null) return Unauthorized();
       var roles = await signInManager.UserManager.GetRolesAsync(user);
    
         return Ok(new { user.Email, user.UserName, Roles = roles }); // ✅ cleaner response

    }

    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return Ok(new {message = "Logout successful"});
    }

    [Authorize]
    [HttpPost("address")]
    public async Task<ActionResult<Address>> CreateOrUpdateAddress(Address address)
    {
        var user = await signInManager.UserManager.Users
            .Include(x => x.Address)
            .FirstOrDefaultAsync(x => x.UserName == User.Identity!.Name);
        if (user == null) return Unauthorized(new {message = "User not found"});

        user.Address = address;

        var result = await signInManager.UserManager.UpdateAsync(user);
        if (!result.Succeeded) return BadRequest(new {message = "Failed to update address"});
        return Ok(new {message = "Address updated successfully"});
    }


    [Authorize]
    [HttpGet("address")]
    public async Task<ActionResult<Address>> GetSavedAddress()
    {
        var address = await signInManager.UserManager.Users
            .Where(x => x.UserName == User.Identity!.Name)
            .Select(x => x.Address)
            .FirstOrDefaultAsync();

        if (address == null) return NoContent();
        return address;
    }


}