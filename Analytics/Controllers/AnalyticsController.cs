﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Analytics.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        public readonly IUploadFileService _uploadFileService;
        private readonly DataContext _context;
        public AnalyticsController(IUploadFileService uploadFileDL, DataContext context)
        {
            _uploadFileService = uploadFileDL;
            _context = context;
        }



        [HttpGet]
        public async Task<ActionResult<List<AnalyticsModal>>> Get()
        {

            return Ok(await _context.Analytics.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<AnalyticsModal>>> Get(long id)
        {
            var Data = await _context.Analytics.FindAsync(id);
            if (Data == null)
                return BadRequest("Data not found.");
            return Ok(Data);
        }

        [HttpPost]
        public async Task<ActionResult<List<AnalyticsModal>>> AddAnalytics(AnalyticsModal Data)
        {
            _context.Analytics.Add(Data);
            await _context.SaveChangesAsync();
            return Ok(await _context.Analytics.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<AnalyticsModal>>> UpdateAnalytics(AnalyticsModal request)
        {
            var Data = await _context.Analytics.FindAsync(request.Id);
            if (Data == null)
                return BadRequest("Data not found.");

            Data.JcCode = request.JcCode;
            Data.Date = request.Date;
            Data.ClDate = request.ClDate;
            Data.Type = request.Type;
            Data.CusDescription = request.CusDescription;
            Data.Service = request.Service;
            Data.AMT = request.AMT;
            Data.WarAMT = request.WarAMT;
            Data.TaxAMT = request.TaxAMT;
            Data.TotAMT = request.TotAMT;
            Data.Day = request.Day;
            Data.Month1 = request.Month1;
            Data.Month = request.Month;
            Data.Year = request.Year;
            Data.Dt = request.Dt;

            await _context.SaveChangesAsync();

            return Ok(await _context.Analytics.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<AnalyticsModal>>> Delete(long id)
        {
            var Data = await _context.Analytics.FindAsync(id);
            if (Data == null)
                return BadRequest("Data not found.");

            _context.Analytics.Remove(Data);

            await _context.SaveChangesAsync();

            return Ok(await _context.Analytics.ToListAsync());
        }


        [Route("ReadFile")]
        [HttpPost]
        public async Task<ActionResult<List<AnalyticsModal>>> AddAnalytics([FromForm] UploadExcelFileRequest request)
        {
            // UploadExcelFileResponse response = new UploadExcelFileResponse();
            string Path = "UploadFileFolder/" + request.File.FileName;
            try
            {
                using (FileStream stream = new FileStream(Path, FileMode.Append))
                {
                    await request.File.CopyToAsync(stream);
                }

                var response = _uploadFileService.UploadExcelFile(request, Path);
                await _context.Analytics.AddRangeAsync(response);
                await _context.SaveChangesAsync();
                return Ok(await _context.Analytics.ToListAsync());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal List<AnalyticsModal> SaveAnalytics(AnalyticsModal analytics)
        {
            throw new NotImplementedException();
        }

        internal List<AnalyticsModal> SaveAnalytics(List<AnalyticsModal> analytics)
        {
            throw new NotImplementedException();
        }

        internal List<AnalyticsModal> GetAnalytics()
        {
            throw new NotImplementedException();
        }
    }
}
