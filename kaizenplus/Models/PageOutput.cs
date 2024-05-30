using System.Collections.Generic;

namespace kaizenplus.Models
{
    public class PageOutput<T>
    {
        public PageOutput(IEnumerable<T> data, int totalRows)
        {
            Data = data;
            TotalRows = totalRows;
        }

        public IEnumerable<T> Data { get; set; }
        public int TotalRows { get; set; }
    }
}