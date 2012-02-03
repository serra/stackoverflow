using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spring.Context.Support;

namespace q9114762_aop_on_mvc_controllers.Controllers
{
    public class HomeController : Controller, IPostMan
    {
        [SetMethodInfoAsMessage]
        public virtual ActionResult Index()
        {
            DoStuff1();
            DoStuff2();
            DoStuff3();

            ViewBag.Message = "Welcome to Index! ";

            ViewBag.MessageDetails = _messages;

            return View();
        }

        [SetMethodInfoAsMessage]
        public virtual void DoStuff1()
        {
            // intercepted, because it is virtual and has the attribute applied
        }

        [SetMethodInfoAsMessage]
        public void DoStuff2()
        {
            // NOT intercepted, because not virtual
        }

        [SetMethodInfoAsMessage]
        protected virtual void DoStuff3()
        {
            // intercepted
        }

        public ActionResult About()
        {
            return View();
        }

        private List<string> _messages = new List<string>();
        public void AddMessage(string message)
        {
            _messages.Add(message);
        }
    }
}
