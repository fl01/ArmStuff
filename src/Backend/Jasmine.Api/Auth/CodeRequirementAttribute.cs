using Microsoft.AspNetCore.Mvc;

namespace Jasmine.Api.Auth
{
    public class CodeRequirementAttribute : TypeFilterAttribute
    {
        public CodeRequirementAttribute()
            : base(typeof(CodeRequirementFilter))
        {
        }
    }
}
