using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class Page
    {
        public int  PageIndex { get; set; }
        public int PageSize { get; set; }
        public int SkipNumber
        {
            get
            {
                return (PageIndex - 1) * PageSize;
            }
        }
    }
}
