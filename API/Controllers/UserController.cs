using System;
using API.Configuration;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(DataContext context) : ControllerBase
{
 
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
