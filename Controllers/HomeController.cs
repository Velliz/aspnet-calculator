using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using callculator.Models;
using System.IO;
using System.Text;

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

            //doing shunting yard
            var infix = number.Val;
            
            string[] tokens = infix.Split(' ');

            Stack<string> s = new Stack<string>();
            List<string> outputList = new List<string>();
            int n;
            foreach (string c in tokens)
            {
                if(int.TryParse(c.ToString(), out n))
                {
                    outputList.Add(c);
                }
                if (c == "(")
                {
                    s.Push(c);
                }
                if (c == ")")
                {
                    while (s.Count != 0 && s.Peek() != "(")
                    {
                        outputList.Add(s.Pop());
                    }
                    s.Pop();
                }
                if (isOperator(c) == true)
                {
                    while(s.Count != 0 && Priority(s.Peek()) >= Priority(c))
                    {
                        outputList.Add(s.Pop());
                    }
                    s.Push(c);
                }
            }
            
            while (s.Count != 0) 
            {
                //if any operators remain in the stack, pop all & add to output list until stack is empty
                outputList.Add(s.Pop());
            }
            
            for (int i = 0; i < outputList.Count; i++)
            {
                Console.Write("{0}", outputList[i]);
            }
            //number.Results = String.Join(", ", outputList.ToArray());

            // test case is 10 + 7 * 14 + (12 / 2)
            // return NotFound();
            return Json(outputList.ToArray());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        static int Priority(string c)
        {
            if (c == "^")
            {
                return 3;
            }
            else if (c == "*" || c == "/")
            {
                return 2;
            }
            else if (c == "+" || c == "-")
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        static bool isOperator(string c)
        {
            if (c == "+" || c == "-" || c == "*" || c == "/" || c == "^")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class Numbers
    {
        public string Key { get; set; }
        public string Val { get; set; }
        public string Verbs { get; set; }
        public string Results { get; set; }
    }

}
