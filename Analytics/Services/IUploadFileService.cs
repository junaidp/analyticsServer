using Analytics;

namespace Services
{
    public interface IUploadFileService
    {
        List<AnalyticsModal> UploadExcelFile(UploadExcelFileRequest request, string Path);
    }
}