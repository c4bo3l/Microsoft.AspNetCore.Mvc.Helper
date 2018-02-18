using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Mvc.Helper
{
    public class PagingModel<T> where T:BaseLinksModel
    {
        public PagedList<T> Values { get; set; }
        public PagingMetadata PagingInfo { get; set; }
    }
}
