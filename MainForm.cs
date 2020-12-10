using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;
using System.Linq;

namespace FileDownloader {
    class MainForm : Form {
        private TextBox URLBox;
        private Label URLLabel;
        private Button DownloadButton;

        public MainForm() {
            this.Width = 500;
            this.Height = 200;
            this.Text = "File Downloader";
            this.StartPosition = FormStartPosition.CenterScreen;

            this.URLLabel = new Label();
            this.URLLabel.Text = "Type URL Here!";
            this.URLLabel.Location = new Point(40, 30);
            this.URLLabel.AutoSize = true;
            this.Controls.Add(this.URLLabel);

            this.URLBox = new TextBox();
            this.URLBox.Location = new Point(40, 50);
            this.URLBox.Size = new Size(400, 50);
            this.URLBox.Text = string.Empty;
            this.Controls.Add(this.URLBox);

            this.DownloadButton = new Button();
            this.DownloadButton.Location = new Point(40, 100);
            this.DownloadButton.Size = new Size(170, 30);
            this.DownloadButton.Text = "Download";
            this.DownloadButton.Click += new EventHandler(this.DownloadButtonClick);
            this.Controls.Add(this.DownloadButton);
        }

        private void DownloadButtonClick(object sender, EventArgs e) {
            var dlpath = KnownFolders.GetPath(KnownFolder.Downloads);
            var uri = this.URLBox.Text;
            if (uri == string.Empty) {
                MessageBox.Show("URLを指定してください。", "Warning");
                return;
            }
            try {
                var path = this.Download(uri, dlpath);
                MessageBox.Show($"{path}へのダウンロードが成功しました。", "Success");
                this.URLBox.Text = string.Empty;
            }
            catch(Exception ex) {
                MessageBox.Show($"Downloadに失敗しました。{ex.Message}", "Error");
                return;
            }
        }

        private string Download(string sUri, string dlpath) {
            var uri = new Uri(sUri);
            var dlfullpath = this.MakeDownloadFullPath(sUri, dlpath);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            var client = new WebClient();
            client.DownloadFile(uri, dlfullpath);
            return dlfullpath;
        }

        private string MakeDownloadFullPath(string uri, string dlpath) {
            var fileName = uri.Split('/').ToList().Last();
            if (fileName == string.Empty) fileName = "NewFile.txt";
            if (dlpath[dlpath.Length - 1] == '/') return dlpath + fileName;
            return dlpath + '/' + fileName;
        }
    }
}