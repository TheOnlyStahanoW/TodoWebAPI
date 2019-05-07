using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TodoWebAPI.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        public static IEnumerable<object> GetErrors(this ModelStateDictionary modelState)
        {
            return modelState.Where(x => x.Value.ValidationState == ModelValidationState.Invalid)
                    .Select(x => new { item = x.Key, message = x.Value.Errors.Select(y => y.ErrorMessage)})
                    .AsEnumerable();
        }
    }
}
