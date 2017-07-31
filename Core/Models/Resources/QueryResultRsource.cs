using System;
using System.Collections.Generic;


namespace mego.Core.Models.Resourses
{
    public class QueryResultRsource<T>
    {
        public int TotalItems { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}
