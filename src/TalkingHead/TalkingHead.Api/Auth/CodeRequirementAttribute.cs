using Microsoft.AspNetCore.Mvc;

namespace TalkingHead.Api.Auth
{
    public class CodeRequirementAttribute : TypeFilterAttribute
    {
        public CodeRequirementAttribute()
            : base(typeof(CodeRequirementFilter))
        {
        }
    }
}
