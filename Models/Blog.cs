using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Blog.Models;

public class Blog
{
    public int Id { get; set; }
    [Required, MaxLength(90)]
    public string Name { get; set; }
    [Required, MinLength(10)]
    public string Content { get; set; }
    public DateTime CreatedTime { get; set; }
    public string UserId { get; set; }
}