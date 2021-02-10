using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Timers;
using System.Windows.Threading;
namespace Assistant{
    public class CFileChecker: DispatcherObject {
        public ObservableCollection<CDir> Dirs { get; set; }
        public CToolTipStack ToolTipStack { get; }
        //private int fPollingInterval;
        private Timer fTimer;
        public CFileChecker(string aConfPath, CToolTipStack aToolTipStack, int aPollingInterval = 180) {
            ToolTipStack = aToolTipStack;
            Dirs = new ObservableCollection<CDir>();
            Excel.Application Exl = new Excel.Application();
            Exl.Workbooks.Open(aConfPath);
            Excel.Worksheet w = Exl.ActiveWorkbook.Sheets["Файлы"];
            int r = 3;
            DateTime dt = DateTime.Today + DateTime.FromOADate(w.Cells[3, 5].Value).TimeOfDay;
            Debug.WriteLine(dt.ToString());
            CDir d = null;
            fTimer = new Timer(aPollingInterval * 1000);
            fTimer.Elapsed += TimerElapsed;
            fTimer.Start();
            while (w.Cells[r, 2].Value != null) {
                if (w.Cells[r, 1].Value != null) {
                    string s = w.Cells[r, 1].Value;
                    Debug.WriteLine("Add DIR " + s);
                    d = new CDir(s);
                    d.Owner = this;
                    //d.FileStatusChanged += OnFileStatusChanged;
                    Dirs.Add(d);
                }
                string f = w.Cells[r, 2].Value;
                DateTime? t = null;
                if (w.Cells[r, 5].Value != null)
                    t = DateTime.Today + DateTime.FromOADate(w.Cells[r, 5].Value).TimeOfDay;
                d.Add(f, t);
                r = r + 1;
            }
            Exl.ActiveWorkbook.Close(false);
        }
        public void /*On*/FileStatusChanged(string aPath, string aStatus)  {
            Debug.WriteLine($"{aPath} - {aStatus}");
            switch (aStatus) {
                case "Expired":
                    ToolTipStack.Push($"Файл {aPath} отсутсвует!", "Внимание", 65000);
                    break;
                case "Exist":
                    ToolTipStack.Push($"Появился файл {aPath}", "Информация");
                    break;
                case "Update":
                    ToolTipStack.Push($"Файл {aPath} обнавлен", "Информация");
                    break;
                default:
                    break;
            }
        }
        public void Stop() {
        }
        private void TimerElapsed(object sender, ElapsedEventArgs e) {
            foreach (CDir d in Dirs)
                d.CheckAllFiles();
        }
        

    }
}
