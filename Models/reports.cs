using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon2._0.Models
{
    internal class reports
    {
        public int Id { get; set; }
        public int InformerId { get; set; }
        public int ReportedId { get; set; }
        public string ReportText { get; set; }
        public int ReportLength { get; set; }
        public DateTime ReportDate { get; set; }

        public reports(int InformerId, int ReportedId, string ReportText, int ReportLength, DateTime ReportDate)
        {
            this.InformerId = InformerId;
            this.ReportedId = ReportedId;
            this.ReportText = ReportText;
            this.ReportLength = ReportLength;
            this.ReportDate = ReportDate;
        }
    }
}
