namespace Core.Entities
{
    public record LoginDto(string Email, string Password);
    public record SignupDto(string Username, string Email, string Password);
    public record LoginResult(LoginResultType Type, string? Token = null, DateTime? Expires = null); 
    
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalCount { get; set; }
    }
    
    public class CarFilters
    {
        public string? Mark        { get; set; }
        public string? Model       { get; set; }
        public int?    MinPrice    { get; set; }
        public int?    MaxPrice    { get; set; }
        public int?    Year        { get; set; }
        public int?    MaxMileage  { get; set; }
        public int?    Range       { get; set; }
        public int?    Page        { get; set; }
        public int?    PageSize    { get; set; }
    }


}
