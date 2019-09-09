using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using callculator.Models;
using System.Collections.Generic;

namespace callculator.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Callculator()
        {
            return View();
        }

        [HttpPost]
        [Produces("application/json")]
        public JsonResult Callculator(String strings)
        {
            var number = new Numbers()
            {
                Key = "parameter",
                Val = HttpContext.Request.Form["strings"],
                Verbs = strings
            };

            // Shunting yard
            ShuntingYard parser = new ShuntingYard(Lexer.Tokenize(number.Val));
            Queue<Token> postfixNotation = parser.PostfixTokens;
            Evaluator evaluator = new Evaluator(postfixNotation);

            number.Results = evaluator.Evaluate();
            return Json(number);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
