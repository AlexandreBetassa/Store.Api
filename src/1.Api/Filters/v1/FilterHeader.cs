using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;

namespace Fatec.Store.User.Api.Filters.v1
{
    public class FilterHeader : ActionFilterAttribute
    {
        private IDistributedCache _cache { get; set; }

        public FilterHeader(IDistributedCache cache)
        {
            _cache = cache;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out var token))
            {
                var tokenJwt = token.ToString().Replace("Bearer ", "");

                var cache = _cache.GetString(tokenJwt);
                if (cache is null)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                base.OnActionExecuted(context);
            }
        }
    }
}