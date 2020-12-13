using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Reflection.Metadata;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Threading.Tasks.Sources;
using System.Windows.Forms;
using System.Xml;

namespace Await_Async {
    public partial class Form1 : Form {
        CancellationTokenSource cts = new CancellationTokenSource ();

        public Form1 () {
            InitializeComponent ();
        }

        public void button1_Click (object sender, EventArgs e) {
            var watch = Stopwatch.StartNew ();

            RunDownloadSync ();
            watch.Stop ();
            var elapsedMs = watch.ElapsedMilliseconds;
            textBox1.Text += $"Total execution time {elapsedMs}";

        }

        public async void button2_Click (object sender, EventArgs e) {
            Progress<ProgressReportModel> progress = new Progress<ProgressReportModel> ();
            progress.ProgressChanged += ReportProgress;

            var watch = Stopwatch.StartNew ();

            try {
                var results = await RunDownloadAsyncList (progress, cts.Token);
                PrintResults (results);
            } catch (Exception ex) {
                textBox1.Text += $"The async download was canceleld {Environment.NewLine}";
            }

            watch.Stop ();
            var elapsedMs = watch.ElapsedMilliseconds;
            textBox1.Text += $"Total execution time {elapsedMs}";
        }

        private void ReportProgress (object sender, ProgressReportModel e) {
            progressBar1.Value = e.PercentageComplete;
            PrintResults (e.SiteDownloaded);

        }

        public async void button3_Click (object sender, EventArgs e) {
              Progress<ProgressReportModel> progress = new Progress<ProgressReportModel> ();
            progress.ProgressChanged += ReportProgress;

            var watch = Stopwatch.StartNew ();
            await RunDownloadParalelAsyncV2 (progress);
            watch.Stop ();
            var elapsedMs = watch.ElapsedMilliseconds;
            textBox1.Text += $"Total execution time {elapsedMs}";
        }

        public void button4_Click (object sender, EventArgs e) {

            cts.Cancel ();
        }
        public void button5_Click (object sender, EventArgs e) {
            var watch = Stopwatch.StartNew ();

            RunDownloadParalelSync ();
            watch.Stop ();
            var elapsedMs = watch.ElapsedMilliseconds;
            textBox1.Text += $"Total execution time {elapsedMs}";

        }

        private void RunDownloadSync () {
            List<string> webSites = PrepData ();
            foreach (string site in webSites) {
                var watch = Stopwatch.StartNew ();
                WebSiteDataModel results = DownloadWebSite (site);
                watch.Stop ();
                var elapsedMs = watch.ElapsedMilliseconds;
                ReportWebsiteInfo (results, elapsedMs);
            }
        }
        private List<WebSiteDataModel> RunDownloadParalelSync () {
            List<string> webSites = PrepData ();
            List<WebSiteDataModel> output = new List<WebSiteDataModel> ();
            Parallel.ForEach<string> (webSites, (site) => {
                WebSiteDataModel results = DownloadWebSite (site);
                output.Add (results);
                ReportWebsiteInfo (results, 0);
            });

            return output;
        }

        private async Task RunDownloadASync () {
            List<string> webSites = PrepData ();
            foreach (string site in webSites) {
                var watch = Stopwatch.StartNew ();
                WebSiteDataModel results = await Task.Run (() => DownloadWebSite (site));
                watch.Stop ();
                var elapsedMs = watch.ElapsedMilliseconds;
                ReportWebsiteInfo (results, elapsedMs);
            }
        }

        private async Task<List<WebSiteDataModel>> RunDownloadAsyncList (IProgress<ProgressReportModel> progress, CancellationToken cts) {
            List<string> webSites = PrepData ();
            List<WebSiteDataModel> output = new List<WebSiteDataModel> ();
            ProgressReportModel report = new ProgressReportModel ();

            foreach (var item in webSites) {
                WebSiteDataModel results = await DownloadWebSiteAsync (item);
                output.Add (results);
                cts.ThrowIfCancellationRequested ();

                report.SiteDownloaded = output;
                report.PercentageComplete = (output.Count * 100) / webSites.Count;
                progress.Report (report);
            }
            return output;
        }

        private async Task RunDownloadParalelAsync () {
            List<string> webSites = PrepData ();
            List<Task<WebSiteDataModel>> tasks = new List<Task<WebSiteDataModel>> ();
            foreach (string site in webSites) {

                // tasks.Add(Task.Run(()=>DownloadWebSite (site)));
                tasks.Add (DownloadWebSiteAsync (site));

            }
            var results = await Task.WhenAll (tasks);
            foreach (var item in results) {
                ReportWebsiteInfo (item, 0);
            }
        }

        public async Task<List<WebSiteDataModel>> RunDownloadParalelAsyncV2 (IProgress<ProgressReportModel> progress) {
            List<string> webSites = PrepData ();
            List<WebSiteDataModel> output = new List<WebSiteDataModel> ();
            ProgressReportModel report = new ProgressReportModel();

            await Task.Run (() => {
                Parallel.ForEach<string> (webSites, (site) => {
                    WebSiteDataModel results = DownloadWebSite (site);
                    output.Add (results);

                    report.SiteDownloaded = output;
                    report.PercentageComplete = (output.Count * 100) / webSites.Count;
                    progress.Report(report);

                });
            });

            return output;
        }

        private async Task<List<WebSiteDataModel>> RunDownloadParalelAsyncList () {
            List<string> webSites = PrepData ();
            List<Task<WebSiteDataModel>> tasks = new List<Task<WebSiteDataModel>> ();
            foreach (string site in webSites) {
                tasks.Add (DownloadWebSiteAsync (site));

            }
            var results = await Task.WhenAll (tasks);
            return new List<WebSiteDataModel> (results);
        }

        private void PrintResults (List<WebSiteDataModel> resultss) {
            textBox1.Text = "";
            foreach (var results in resultss) {
                textBox1.Text += $"{results.WebsiteUrl} downloaded: {results.WebsiteData.Length} characters long ,downloaded  seconds {Environment.NewLine} ";
            }
        }

        private void ReportWebsiteInfo (WebSiteDataModel results, long seconds = 0) {
            textBox1.Text += $"{results.WebsiteUrl} downloaded: {results.WebsiteData.Length} characters long ,downloaded {seconds} seconds {Environment.NewLine} ";
        }

        private List<string> PrepData () {
            List<string> output = new List<string> ();
            textBox1.Clear ();
            output.Add ("https://www.google.com");
            output.Add ("https://www.microsoft.com");
            output.Add ("https://www.cnn.com");
            output.Add ("https://www.codeproject.com");
            output.Add ("https://www.stackoverflow.com");
            output.Add ("https://www.gul-pak.com");
            output.Add ("http://pandas55-001-site1.gtempurl.com/");
            output.Add ("https://www.yahoo.com");
            return output;
        }

        private WebSiteDataModel DownloadWebSite (string WebSiteUrl) {
            WebSiteDataModel output = new WebSiteDataModel ();
            WebClient client = new WebClient ();
            output.WebsiteUrl = WebSiteUrl;
            output.WebsiteData = client.DownloadString (WebSiteUrl);
            return output;
        }

        private static async Task<WebSiteDataModel> DownloadWebSiteAsync (string WebSiteUrl) {
            WebSiteDataModel output = new WebSiteDataModel ();
            WebClient client = new WebClient ();
            output.WebsiteUrl = WebSiteUrl;
            output.WebsiteData = await client.DownloadStringTaskAsync (WebSiteUrl);
            return output;
        }

        public class WebSiteDataModel {
            public string WebsiteUrl { get; set; }
            public string WebsiteData { get; set; }
        }

    }
}