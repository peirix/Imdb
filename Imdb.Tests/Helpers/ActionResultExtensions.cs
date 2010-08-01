using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Imdb.Tests.Helpers
{
    public static class ActionResultExtensions
    {
        public static T GetViewModel<T>(this ActionResult ar)
        {
            return (T)((ViewResult)ar).ViewData.Model;
        }
    }
}
