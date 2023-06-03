using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Blog.Models;

public class ApplicationUser : IdentityUser
{
	[Required]
	public string? Name { get; set; }
	
}