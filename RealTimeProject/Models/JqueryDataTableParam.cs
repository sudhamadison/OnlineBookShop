namespace RealTimeProject.Models
{
    public class JqueryDataTableParam
    {
        public string Search {  get; set; }
        public int PageSize { get; set; }   
        public int PageNumber { get; set; }
        public int Column { get; set; }
        public string SortColumn { get; set; }

        public string SortOrder { get; set; }
        public int DiaplayStart { get; set; }
        public int DisplayLength { get; set; }
    }
}
