using System;
using API.Configuration;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class UserController(DataContext context) : BaseApiController
{
    [AllowAnonymous]
    [HttpGet(Name = "GetUsers")]
    public async Task<ActionResult<IEnumerable<LovUser>>> GetUsers()
    {
        return await context.User.ToListAsync();
    }

    [HttpGet("{id}", Name = "GetUser")]
    public async Task<ActionResult<LovUser>> GetUser(int id)
    {
        var user= await context.User.FindAsync(id);
        if(user == null) return NotFound();
        return user;
    }
}
