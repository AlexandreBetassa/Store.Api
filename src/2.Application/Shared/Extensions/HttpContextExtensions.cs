﻿using Microsoft.AspNetCore.Http;

namespace Fatec.Store.User.Application.Shared.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetUserName(this IHttpContextAccessor context) =>
             context?.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type.Equals("name"))?.Value ?? string.Empty;

        public static string GetUserId(this IHttpContextAccessor context) =>
             context?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type.Equals("id"))?.Value ?? string.Empty;

        public static string GetUserEmail(this IHttpContextAccessor context) =>
            context?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type.Equals("email"))?.Value ?? string.Empty;
    }
}
