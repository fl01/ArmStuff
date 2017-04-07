using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TalkingHead.Api.Definitions;
using TalkingHead.Api.Services;

namespace TalkingHead.Api.Auth
{
    public class CodeRequirementFilter : IAsyncActionFilter
    {
        private readonly ISettingsService _settingsService;

        public CodeRequirementFilter(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string rawCode = context.HttpContext.Request.Headers[HttpConsts.Headers.Code].FirstOrDefault();
            Guid.TryParse(rawCode, out Guid code);

            if (!_settingsService.AuthCode.Equals(code))
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                await next();
            }
        }
    }
}
