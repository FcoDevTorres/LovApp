using System;
using System.Security.Cryptography;
using System.Text;
using API.Configuration;
using API.Dto;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
{
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await context.User.SingleOrDefaultAsync(user => user.Username == loginDto.Username.ToLower());
        if (user == null)
        {
            return Unauthorized("Invalid user or password");
        }

        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
        for (int i = 0; i < user.PasswordHash.Length; i++)
        {
            if (user.PasswordHash[i] != computedHash[i])
            {
                return Unauthorized("Invalid username or pass");
            }
        }

        return new UserDto
        {
            Username = user.Username,
            Token = tokenService.ObtainToken(user)
        };
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Username))
        {
            return BadRequest("Username is taken");
        }

        using var hmac = new HMACSHA512();
        var user = new LovUser
        {
            Username = registerDto.Username,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };
        context.Add(user);
        await context.SaveChangesAsync();

        return new UserDto
        {
            Username = user.Username,
            Token = tokenService.ObtainToken(user)
        };
    }

    private async Task<bool> UserExists(string username)
    {
        return await context.User.AnyAsync(x => x.Username.ToLower() == username.ToLower());
    }
}