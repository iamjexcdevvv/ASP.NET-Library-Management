using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.Data;

namespace LibraryManagement.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BookManagementContext _dbContext;

    public HomeController(ILogger<HomeController> logger, BookManagementContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult AddBook()
    {
        return View();
    }
    public IActionResult Books()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}