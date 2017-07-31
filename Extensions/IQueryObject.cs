namespace mego.Extensions
{
    public interface IQueryObject
    {
         string SortBy { get; set; }

         bool IsSort { get; set; }

         int Page { get; set; }

         byte PageSize { get; set; }
    }
}
