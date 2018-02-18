using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc.Helper
{
    public class PagingParameters
    {
        #region Properties
       public int PageNumber { get; set; }

        public int PageSize { get; set; }
        #endregion
    }
}
