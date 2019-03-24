using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SignalRMVCChat.Models;

namespace SignalRMVCChat.Controllers
{
    public class HomeController : Controller
    {
         IChatContext _context;
        public HomeController(IChatContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            return View(_context.QuestionSet);
        }

        public IActionResult Chat()
        {

            return View();
        }
        public IActionResult Specific(Guid QId)
        {
            var question = _context.QuestionSet.Where(x => x.Id == QId).FirstOrDefault();
            return View(question);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
