using ShortUrlService.Data.Infrastructure;
using ShortUrlService.Data.Repositories;
using ShortUrlService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortUrlService.Service
{
    public interface IHistoryRecordService
    {
        IEnumerable<HistoryRecord> GetHistoryRecords();
        HistoryRecord GetHistoryRecord(long id);
        void CreateHistoryRecord(HistoryRecord historyRecord);
        void UpdateHistoryRecord(HistoryRecord historyRecord);
        void SaveHistoryRecord();
    }

    public class HistoryRecordService : IHistoryRecordService
    {
        private readonly IHistoryRecordRepository historyRecordsRepository;
        private readonly IUnitOfWork unitOfWork;

        public HistoryRecordService(IHistoryRecordRepository historyRecordsRepository, IUnitOfWork unitOfWork)
        {
            this.historyRecordsRepository = historyRecordsRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IHistoryRecordService Members

        public IEnumerable<HistoryRecord> GetHistoryRecords()
        {
            var historyRecords = historyRecordsRepository.GetAll();
            return historyRecords;
        }

        public HistoryRecord GetHistoryRecord(long id)
        {
            var historyRecord = historyRecordsRepository.GetById(id);
            return historyRecord;
        }

        public void CreateHistoryRecord(HistoryRecord historyRecord)
        {
            historyRecordsRepository.Add(historyRecord);
        }

        public void SaveHistoryRecord()
        {
            unitOfWork.Commit();
        }

        public void UpdateHistoryRecord(HistoryRecord historyRecord)
        {
            historyRecordsRepository.Update(historyRecord);
        }

        #endregion

    }
}
