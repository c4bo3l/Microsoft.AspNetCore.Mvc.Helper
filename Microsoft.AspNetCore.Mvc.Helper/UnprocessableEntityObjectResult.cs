using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Microsoft.AspNetCore.Mvc.Helper
{
    public class UnprocessableEntityObjectResult : ObjectResult
    {
        public UnprocessableEntityObjectResult(ModelStateDictionary error) 
            : base(new SerializableError(error))
        {
            if (error == null)
                throw new ArgumentNullException();
            StatusCode = 422;
        }
    }
}
