using Analytics;
using Analytics.viewModals;

namespace Services
{
    public interface IUploadFileService
    {
        ParentViewModal UploadExcelFile(UploadExcelFileRequest request, string Path);
    }
}