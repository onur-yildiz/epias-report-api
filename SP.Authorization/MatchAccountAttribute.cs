using Microsoft.AspNetCore.Mvc.Filters;
using MongoDB.Bson;
using SP.Exceptions;

namespace SP.Authorization
{
    public class MatchAccountAttribute : Attribute, IActionFilter
    {
        public virtual bool AdminAllowed { get; set; }

        public MatchAccountAttribute()
        {
            AdminAllowed = false;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments.TryGetValue("userId", out var userIdObj);
            var userId = ((ObjectId?)context.HttpContext.Items["UserId"]).ToString();
            var isAdmin = (bool?)context.HttpContext.Items["IsAdmin"];

            if (userIdObj is not string userIdParam || userId == null || (userId != userIdParam && !(AdminAllowed && isAdmin == true)))
                throw HttpResponseException.AccountMismatch();
        }

        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {
            //
        }
    }
}
