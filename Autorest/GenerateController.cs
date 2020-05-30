using System;
using System.Collections.Generic;
using System.Text;

namespace Autorest
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class GenerateController : Attribute
    {

        public GenerateController(string route = "")
        {
            Route = route;
        }

        public string Route { get; set; }
    }
}

