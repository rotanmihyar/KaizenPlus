using kaizenplus.Attributes;

namespace kaizenplus.Models
{
    public class PageInput
    {
        [GreaterThanZero]
        public int PageSize { get; set; }

        [GreaterThanZero]
        public int PageNumber { get; set; }
    }
}