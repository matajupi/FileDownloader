using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace FileDownloader {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            Application.Run(new MainForm());
        }
    }
}