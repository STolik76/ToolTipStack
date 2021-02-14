using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Excel = Microsoft.Office.Interop.Excel;
//using System.Diagnostics;
using System.Timers;
using System.Windows.Threading;
namespace Assistant{
    public class CFileChecker: DispatcherObject {
        public ObservableCollection<CDir> Dirs { get; set; }
        public CToolTipStack ToolTipStack { get; }
        private Timer fTimer;
        public CFileChecker(string aConfPath, CToolTipStack aToolTipStack, int aPollingInterval = 180) {
            int tc = 0;
            ToolTipStack = aToolTipStack;
            Dirs = new ObservableCollection<CDir>();
            Excel.Application Exl = new Excel.Application();
            Exl.Workbooks.Open(aConfPath);
            Excel.Worksheet w = Exl.ActiveWorkbook.Sheets["Файлы"];
            int r = 3;
            DateTime dt = DateTime.Today + DateTime.FromOADate(w.Cells[3, 5].Value).TimeOfDay;
            //Debug.WriteLine(dt.ToString());
            CDir d = null;
            fTimer = new Timer(aPollingInterval * 1000);
            fTimer.Elapsed += TimerElapsed;
            fTimer.Start();
            while (w.Cells[r, 2].Value != null) {
                if (w.Cells[r, 1].Value != null) {
                    string s = w.Cells[r, 1].Value;
                    //Debug.WriteLine("Add DIR " + s);
                    d = new CDir(s);
                    //d.Owner = this;
                    Dirs.Add(d);
                }
                string p = w.Cells[r, 2].Value;
                int sd = Convert.ToInt32(w.Cells[r, 3].Value);
                int ud = Convert.ToInt32(w.Cells[r, 4].Value);
                DateTime? t = null;
                int? ed = null;
                if (w.Cells[r, 5].Value != null)
                    t = DateTime.Today + DateTime.FromOADate(w.Cells[r, 5].Value).TimeOfDay;
                if (w.Cells[r, 6].Value != null)
                    ed = Convert.ToInt32(w.Cells[r, 6].Value);
                CFile f = new CFile(p, sd, ud, t, ed, this, tc);
                d.Add(f);
                tc++;
                r = r + 1;
            }
            Exl.ActiveWorkbook.Close(false);
            CheckAll();
        }
        public void FileStatusChanged(CFile aFile)  {
           // Debug.WriteLine($"{aPath} - {aStatus}");
            switch (aFile.Status) {
                case "Expired":
                    if (aFile.ExpiredDelay != null)
                        ToolTipStack.Push($"Файл {aFile.PathFile} отсутсвует!", aFile.Tag, "Внимание", 
                            (int)aFile.ExpiredDelay);
                    break;
                case "Exist":
                    ToolTipStack.Remove(aFile.Tag);
                    ToolTipStack.Push($"Появился файл {aFile.PathFile}", aFile.Tag, "Информация",  
                        aFile.ShowDelay);
                    break;
                case "Update":
                    ToolTipStack.Push($"Файл {aFile.PathFile} обнавлен", aFile.Tag, "Информация",  
                        aFile.UpdateDelay);
                    break;
                default:
                    break;
            }
        }
        public void Stop() {
        }
        private void TimerElapsed(object sender, ElapsedEventArgs e) {
            CheckAll();
        }   
        private void CheckAll() {
            foreach (CDir d in Dirs)
                d.CheckAllFiles();

        }
    }
}
