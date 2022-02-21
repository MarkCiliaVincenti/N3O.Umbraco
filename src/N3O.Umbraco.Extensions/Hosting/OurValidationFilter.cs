﻿using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace N3O.Umbraco.Hosting {
    [AttributeUsage(AttributeTargets.Class)]
    public class OurValidationFilter : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext context) {
            if (!context.ModelState.IsValid) {
                var errors = context.ModelState.ToDictionary(x => x.Key.Camelize(),
                                                             x => x.Value.Errors.Select(e => e.ErrorMessage).ToArray());

                var problemDetails = new ValidationProblemDetails(errors);
                
                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }
    }
}