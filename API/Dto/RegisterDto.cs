using System;
using System.ComponentModel.DataAnnotations;

namespace API.Dto;

public class RegisterDto
{
    [MaxLength(10)]
    [Required]
    public required string Username { get; set; }
    [Required]
    public required string Password { get; set; }
}
