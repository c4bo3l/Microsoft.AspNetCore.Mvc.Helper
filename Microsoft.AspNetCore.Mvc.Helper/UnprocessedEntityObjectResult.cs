using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Microsoft.AspNetCore.Mvc.Helper
{
    public class UnprocessedEntityObjectResult : ObjectResult
    {
        public UnprocessedEntityObjectResult(ModelStateDictionary error) 
            : base(new SerializableError(error))
        {
            if (error == null)
                throw new ArgumentNullException();
            StatusCode = 422;
        }
    }
}
