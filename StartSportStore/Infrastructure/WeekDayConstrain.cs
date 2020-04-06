using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;


namespace StartSportStore.Infrastructure
{
    public class WeekDayConstrain : IRouteConstraint
    {
        private string[] Days = new[] { "sat", "sun", "mon", "tues", "wed", "thurs", "fri" };
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return Days.Contains(values[routeKey]?.ToString().ToLowerInvariant());
        }
    }
    public class MonthConstrain : IRouteConstraint
    {
        public string[] months = new string[] { "jan", "feb", "mar", "april", "jun" };

        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return months.Contains(values[routeKey]?.ToString().ToLowerInvariant());
        }
    }
}
