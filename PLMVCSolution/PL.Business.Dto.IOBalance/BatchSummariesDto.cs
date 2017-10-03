using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Business.Dto.IOBalance
{
    public class BatchSummariesDto
    {
        public long BatchNo { get; set; }

        [StringLength(1000)]
        public string FileName { get; set; }

        [StringLength(3000)]
        public string FilePath { get; set; }

        public int TotalRecords { get; set; }

        public int Successful { get; set; }

        public int Failed { get; set; }

        [StringLength(50)]
        public string UploadStatus { get; set; }

        public bool IsDownload { get; set; }

        [StringLength(1000)]
        public string ResultFileName { get; set; }

        [StringLength(3000)]
        public string ResultFilePath { get; set; }

        public int? UploadedBy { get; set; }

        public string UploadedByUserName { get; set; }

        public DateTime StartUpload { get; set; }

        public DateTime? EndUpload { get; set; }
    }
}
