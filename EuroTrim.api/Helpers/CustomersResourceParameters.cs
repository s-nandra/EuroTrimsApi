using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuroTrim.api.Helpers
{
    public class CustomersResourceParameters
    {
        const int maxPageSize = 20;

        public int PageNumber { get; set; } = 1;

        public int _pageSize = 10;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public string City { get; set; }

        public string SearchQuery { get; set; }

        public string OrderBy { get; set; } = "Name";
    }
}
