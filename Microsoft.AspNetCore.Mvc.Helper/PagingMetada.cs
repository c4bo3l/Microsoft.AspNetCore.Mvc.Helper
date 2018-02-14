﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Mvc.Helper
{
    public class PagingMetada
    {
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalItems { get; set; }
        public string PreviousPageLink { get; set; }
        public string NextPageLink { get; set; }
    }
}
