using AutoMapper;
using ShortUrlService.Model;
using ShortUrlService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ShortUrlServiceWeb.Controllers.Utils;
using ShortUrlServiceWeb.ViewModels;

namespace ShortUrlServiceWeb.Controllers
{
    public class RecordsController : ApiController
    {
        private readonly IHistoryRecordService historyRecordService;

        public RecordsController(IHistoryRecordService historyRecordService)
        {
            this.historyRecordService = historyRecordService;
        }

          // GET api/records
        public IEnumerable<HistoryRecordViewModel> Get()
        {
            IEnumerable<HistoryRecordViewModel> viewModelHistoryRecords;
            IEnumerable<HistoryRecord> historyRecords;

            historyRecords = historyRecordService.GetHistoryRecords().ToList();
            viewModelHistoryRecords = Mapper.Map<IEnumerable<HistoryRecord>, IEnumerable<HistoryRecordViewModel>>(historyRecords);

            return viewModelHistoryRecords;
        }

        // GET api/records/5
        public IHttpActionResult Get(string str)
        {
            if (String.IsNullOrEmpty(str))
            {

                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }

            ConverterUrl converter = new ConverterUrl(10);
            long recordId = converter.Decode(str);

            HistoryRecord historyRecord = historyRecordService.GetHistoryRecord(recordId);

            if (historyRecord == null)
            {

                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            historyRecord.HitCount++;
            historyRecordService.UpdateHistoryRecord(historyRecord);
            historyRecordService.SaveHistoryRecord();

            //var response = Request.CreateResponse(HttpStatusCode.Moved);
            //response.Headers.Location = new Uri(historyRecord.UrlLong);
            
            return Redirect(historyRecord.UrlLong);
        }


        // POST api/records
        public HistoryRecordViewModel Post([FromBody]HistoryRecordViewModel viewModelHistoryRecord)
        {
            if (ModelState.IsValid)
            {
                HistoryRecord historyRecord = Mapper.Map<HistoryRecordViewModel, HistoryRecord>(viewModelHistoryRecord);
                try
                {
                    historyRecord.CreateDate = DateTime.Now; //todo настроить в EF автогенерацию
                    historyRecordService.CreateHistoryRecord(historyRecord);
                    historyRecordService.SaveHistoryRecord();
                }
                catch (Exception ex)
                {

                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
                }                

                  
                
                ConverterUrl converter = new ConverterUrl(10); //todo вынести в настройки 10
                string hash = converter.Encode(historyRecord.Id);

                //historyRecord.UrlShort = $"{Request.RequestUri.Authority}api/values/{hash}"; //to do Или проще так?
                historyRecord.UrlShort = $"{Request.RequestUri.Authority}/Home/Record/{hash}";
                historyRecordService.UpdateHistoryRecord(historyRecord);
                historyRecordService.SaveHistoryRecord();
                viewModelHistoryRecord = Mapper.Map<HistoryRecord, HistoryRecordViewModel>(historyRecord);
            }
            return viewModelHistoryRecord;
        }
    }
}
