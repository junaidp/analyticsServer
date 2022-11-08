using Abp.Extensions;
using Abp.UI;
using Analytics;
using Analytics.viewModals;
using ExcelDataReader;
using System.Data;
using System.Text;

namespace Services
{
    public class UploadFileService : IUploadFileService
    {

        public readonly IConfiguration _configuration;
        public UploadFileService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public ParentViewModal UploadExcelFile(UploadExcelFileRequest request, string Path)
        {

            try
            {
                var parentViewModal = new ParentViewModal();
                UploadExcelFileResponse response = new UploadExcelFileResponse();
                List<AnalyticsModal> parameters = new List<AnalyticsModal>();
                response.IsSuccess = true;
                response.Message = "Successfull";
                if (!request.File.FileName.ToLower().Contains(value: ".xlsx"))
                {
                    throw new UserFriendlyException("Only Accept .xlsx file");
                }
                FileStream stream = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
                DataSet dataset = reader.AsDataSet(
                   configuration: new ExcelDataSetConfiguration()
                   {
                       UseColumnDataType = false,
                       ConfigureDataTable = (IExcelDataReader tableReader) => new ExcelDataTableConfiguration()
                       {
                           UseHeaderRow = true
                       }
                   });

                var col = new List<ColumnsModal>();

                var columns = dataset.Tables[index: 0].Columns;

               
                for (int i = 0; i < columns.Count; i++)
                {
                    var c = new ColumnsModal();
                    if (columns[i] != null)
                    {
                        c.Name = columns[i].ToString();
                    }
                    col.Add(c);
                }


                for (int i = 0; i < dataset.Tables[index: 0].Rows.Count; i++)
                {
                    AnalyticsModal rows = new AnalyticsModal
                    {
                        JcCode = dataset.Tables[index: 0].Rows[i].ItemArray[0] != null ? Convert.ToString(dataset.Tables[index: 0].Rows[i].ItemArray[0]) : "-1",
                        Date = dataset.Tables[index: 0].Rows[i].ItemArray[1] != null ? Convert.ToString(dataset.Tables[index: 0].Rows[i].ItemArray[1]) : "-1",
                        ClDate = dataset.Tables[index: 0].Rows[i].ItemArray[2] != null ? Convert.ToString(dataset.Tables[index: 0].Rows[i].ItemArray[2]) : "-1",
                        Type = dataset.Tables[index: 0].Rows[i].ItemArray[3] != null ? Convert.ToString(dataset.Tables[index: 0].Rows[i].ItemArray[3]) : "-1",
                        CusDescription = dataset.Tables[index: 0].Rows[i].ItemArray[4] != null ? Convert.ToString(dataset.Tables[index: 0].Rows[i].ItemArray[4]) : "-1",
                        Service = dataset.Tables[index: 0].Rows[i].ItemArray[5] != null ? Convert.ToString(dataset.Tables[index: 0].Rows[i].ItemArray[5]) : "-1",
                        AMT = dataset.Tables[index: 0].Rows[i].ItemArray[6] != null ? Convert.ToString(dataset.Tables[index: 0].Rows[i].ItemArray[6]) : "-1",
                        WarAMT = dataset.Tables[index: 0].Rows[i].ItemArray[7] != null ? Convert.ToString(dataset.Tables[index: 0].Rows[i].ItemArray[7]) : "-1",
                        TaxAMT = dataset.Tables[index: 0].Rows[i].ItemArray[8] != null ? Convert.ToString(dataset.Tables[index: 0].Rows[i].ItemArray[8]) : "-1",
                        TotAMT = dataset.Tables[index: 0].Rows[i].ItemArray[9] != null ? Convert.ToString(dataset.Tables[index: 0].Rows[i].ItemArray[9]) : "-1",
                        Day = dataset.Tables[index: 0].Rows[i].ItemArray[10] != null ? Convert.ToString(dataset.Tables[index: 0].Rows[i].ItemArray[10]) : "-1",
                        Month1 = dataset.Tables[index: 0].Rows[i].ItemArray[11] != null ? Convert.ToString(dataset.Tables[index: 0].Rows[i].ItemArray[11]) : "-1",
                        Month = dataset.Tables[index: 0].Rows[i].ItemArray[12] != null ? Convert.ToString(dataset.Tables[index: 0].Rows[i].ItemArray[12]) : "-1",
                        Year = dataset.Tables[index: 0].Rows[i].ItemArray[13] != null ? Convert.ToString(dataset.Tables[index: 0].Rows[i].ItemArray[13]) : "-1",
                        Dt = dataset.Tables[index: 0].Rows[i].ItemArray[14] != null ? Convert.ToString(dataset.Tables[index: 0].Rows[i].ItemArray[14]) : "-1"
                    };
                    parameters.Add(rows);
                }

                stream.Close();

                parentViewModal.Columns = col;
                parentViewModal.AnalyticsData = parameters;

                return parentViewModal;


            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
