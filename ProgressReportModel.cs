using System.Collections.Generic;

namespace Await_Async {
    public class ProgressReportModel {

        public int PercentageComplete { get; set; }
        public List<Form1.WebSiteDataModel> SiteDownloaded { get; set; } = new List<Form1.WebSiteDataModel> ();

        public ProgressReportModel () {

        }
    }
}