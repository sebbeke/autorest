using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Autorest
{
    public class GenericControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var controller = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(m => m.GetExportedTypes())
                .Where(m => m.GetCustomAttributes(typeof(GenerateController), false).Any());
            foreach (var i in controller)
            {
                var c = typeof(AutoRestController<>).MakeGenericType(i).GetTypeInfo();
                feature.Controllers.Add(c
                    
                );

            }
        }
    }
}
