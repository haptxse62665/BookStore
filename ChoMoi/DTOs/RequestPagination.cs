using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChoMoi.DTOs
{
    public class RequestPagination
    {
        public string FileterKey { get; set; }
        public string SortBy { get; set; }
        public string SearchKey { get; set; }
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
        //public int? Page
        //{
        //    get { return Page ?? 1; }
        //    set { Page = value; }
        //}
        //public int? PageSize
        //{
        //    get { return PageSize ?? 10; }
        //    set { PageSize = value; }
        //}
    }
}
