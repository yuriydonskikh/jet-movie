using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace JetMovie.Helpers
{
    public static class Errors
    {
        public static ModelStateDictionary AddErrorsToModelState(this ModelStateDictionary modelState,
            IdentityResult identityResult)
        {
            foreach (var e in identityResult.Errors)
                modelState.TryAddModelError(e.Code, e.Description);
            return modelState;
        }

        public static ModelStateDictionary AddErrorToModelState(this ModelStateDictionary modelState, string code,
            string description)
        {
            modelState.TryAddModelError(code, description);
            return modelState;
        }

        public static ModelStateDictionary AddErrorToModelState(this ModelStateDictionary modelState,
            Exception exception)
        {
            modelState.TryAddModelError(nameof(exception), exception.Message);
            return modelState;
        }
    }
}

