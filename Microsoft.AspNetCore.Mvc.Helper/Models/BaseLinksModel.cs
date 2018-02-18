using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Mvc.Helper
{
    public abstract class BaseLinksModel
    {
        public List<LinkModel> Links { get; set; } = new List<LinkModel>();
    }
}
