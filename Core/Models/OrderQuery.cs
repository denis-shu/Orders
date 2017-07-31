using mego.Extensions;

namespace mego.Core.Models
{
    public class OrderQuery: IQueryObject
    {
        public int? MakeId { get; set; }

        public int? ModelId { get; set; }

        public string SortBy  { get; set; }

        public bool IsSort { get; set; }

        public int Page { get; set; }

        public byte PageSize  { get; set; }
    }
}
