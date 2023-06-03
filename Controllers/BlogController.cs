using System.Diagnostics;
using Blog.Data;
using Blog.Models;
using Blog.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers;

public class BlogController : Controller
{
    private readonly ApplicationDbContext _db;
    public BlogController(ApplicationDbContext db) { _db = db; }
    public IActionResult Index()
    {
        if (_db.Blogs == null) return View();
        var posts = _db.Blogs.ToList();
        return View(posts);
    }

    public IActionResult GetRandom()
    {
        int totalCount = _db.Blogs.Count();
        int randomIndex = new Random().Next(totalCount);
        int randomId = _db.Blogs.OrderBy(b => b.Id)
            .Skip(randomIndex)
            .Select(b => b.Id)
            .FirstOrDefault();
        var newObj = new { id = randomId };
        return RedirectToAction("Show", newObj);
    } 
    
    public IActionResult Show(int id)
    {
        var post = _db.Blogs.FindAsync(id).Result;
        // Eğer id ile bulunmazsa normal indexe döndür.
        TempData["information"] = "No id provided returning to index";
        if (post == null)
        {
            return RedirectToAction("Index", "Blog");
        }
        return View(post);
    }
    [Authorize(Roles = $"{Sd.RoleUserAdmin}, {Sd.RoleUserWriter}")]
    public IActionResult Create()
    {
        return View();
    }
    [Authorize(Roles = $"{Sd.RoleUserAdmin}, {Sd.RoleUserWriter}")]
    [HttpPost]
    public IActionResult Create(Models.Blog obj)
    {
        if (ModelState.IsValid)
        {
            TempData["success"] = "Blog created successfully";
            obj.CreatedTime = DateTime.Now;
            _db.Blogs.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", "Blog");
        }
        return View();
    }
    [Authorize(Roles = $"{Sd.RoleUserAdmin}, {Sd.RoleUserWriter}")]
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        Models.Blog? _tempBlog = _db.Blogs.Find(id);
        return View(_tempBlog);
    }
    [Authorize(Roles = $"{Sd.RoleUserAdmin}, {Sd.RoleUserWriter}")]
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int id)
    {
        Models.Blog? obj = _db.Blogs.Find(id);
        if (obj == null)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            TempData["success"] = "Category deleted successfully";
            _db.Blogs.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", "Blog");
        }
        return View();
    }
}