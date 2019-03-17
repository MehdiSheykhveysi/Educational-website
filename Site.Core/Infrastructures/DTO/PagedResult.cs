using System;
using System.Collections.Generic;

namespace Site.Core.Infrastructures.DTO
{
    public class PagedResult<T> where T : class
    {
        public List<T> ListItem { get; set; } = new List<T>();
        public PageData PageData { get; set; } = new PageData();
    }
    public class PageData
    {
        public decimal TotalItem { get; set; }
        public decimal ItemPerPage { get; set; }
        public int CurentItem { get; set; }
        public int TotalPages()
        {
            int x = 0;
            if (TotalItem > 0)
                x = (int)(Math.Ceiling((TotalItem / ItemPerPage)));
            return x;
        }

    }
}