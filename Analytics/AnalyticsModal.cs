namespace Analytics
{
    public class UploadExcelFileRequest
    {
        public IFormFile File { get; set; }
    }
    public class UploadExcelFileResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
    public class AnalyticsModal
    {
        public int Id { get; set; }

        public string? JcCode { get; set; }
        public string? Date { get; set; }
        public string? ClDate { get; set; }
        public string? Type { get; set; }
        public string? CusDescription { get; set; }
        public string? Service { get; set; }
        public string? AMT { get; set; }
        public string? WarAMT { get; set; }
        public string? TaxAMT { get; set; }
        public string? TotAMT { get; set; }
        public string? Day { get; set; }
        public string? Month1 { get; set; }
        public string? Month { get; set; }
        public string? Year { get; set; }
        public string? Dt { get; set; }

    }}
