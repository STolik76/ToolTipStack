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
                string p = GetStrByTemplate(w.Cells[r, 2].Value);
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
        public string GetStrByTemplate(string aTemplate) {
            string s;
            DateTime d = DateTime.Today;
            string dd = d.ToString("dd");
            string mm = d.ToString("MM");
            string yy = d.ToString("yy");
            string yyyy = d.ToString("yyyy");
            DateTime d1 = d.AddDays(1);
            string dd1 = d1.ToString("dd");
            string mm1 = d1.ToString("MM");
            string yy1 = d1.ToString("yy");
            string yyyy1 = d1.ToString("yyyy");
            DateTime d2 = d.AddDays(2);
            string dd2 = d2.ToString("dd");
            string mm2 = d2.ToString("MM");
            string yy2 = d2.ToString("yy");
            string yyyy2 = d2.ToString("yyyy");
            s = aTemplate
                .Replace("%ДД%", dd)
                .Replace("%ММ%", mm)
                .Replace("%ГГ%", yy)
                .Replace("%ГГГГ%", yyyy)
                .Replace("%ДД1%", dd1)
                .Replace("%ММ1%", mm1)
                .Replace("%ГГ1%", yy1)
                .Replace("%ГГГГ1%", yyyy1)
                .Replace("%ДД2%", dd2)
                .Replace("%ММ2%", mm2)
                .Replace("%ГГ2%", yy2)
                .Replace("%ГГГГ2%", yyyy2);
            return s;
        }
    }
}
