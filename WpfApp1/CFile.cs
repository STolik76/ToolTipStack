using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Windows.Threading;
namespace Assistant {
    public delegate void DFileStatusChanged(CFile aFile);
    public class CFile : INotifyPropertyChanged  {
        private DateTime fDateUpdate;//Время обновления файла
        private DateTime? fDedLine;//Предельное время появления файла
        public string PathFile { get; }//Путь к файлу
        private string fFileName;//Имя файла
        private string fStatus;//Статус файла
        private string fDateUpdateStr;//Строковое обозначение времени обновления файла
        public event PropertyChangedEventHandler PropertyChanged;
        public  CFileChecker Owner { get; }
        public string FileName {
            get {
                return fFileName;
            }
            set {
                fFileName = value;
                OnPropertyChanged("FileName");
            }
        }
        public string DateUpdate {
            get {
                return fDateUpdateStr;
            }
            set {
                fDateUpdateStr = value;
                OnPropertyChanged("DateUpdate");
            }
        }
        public string Status {
            get {
                return fStatus;
            }
            set {
                fStatus = value;
                OnPropertyChanged("Status");
                if (Owner != null) {
                    Owner.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new DFileStatusChanged(Owner.FileStatusChanged), this);
                }                    
            }
        }
        public int ShowDelay { get; }
        public int UpdateDelay { get; }
        public int? ExpiredDelay { get; }
        public int Tag { get;  }
        public CFile(string aPath, int aShowDelay, int aUpdateDelay, DateTime? aDedLine, int? aExpiredDelay, 
            CFileChecker aOwner, int aTag) {
            PathFile = aPath;
            fDedLine = aDedLine;
            //!!!!!
            ShowDelay = aShowDelay;
            UpdateDelay = aUpdateDelay;
            ExpiredDelay = aExpiredDelay;
            Owner = aOwner;
            ///!!!!
            DateUpdate = "-";
            Status = "None";
            FileName = Path.GetFileName(PathFile);
            fDateUpdate = DateTime.Now;
            Tag = aTag;
        }
        public string Check() {
            if (File.Exists(PathFile) == true) {
                DateTime t = File.GetLastWriteTime(PathFile);
                if ((Status == "None") || (Status == "Expired")) {
                    fDateUpdate = t;
                    DateUpdate = fDateUpdate.ToString("g");
                    Status = "Exist";
                } else {
                    if (t > fDateUpdate)                    {
                        fDateUpdate = t;
                        DateUpdate = fDateUpdate.ToString("g");
                        Status = "Update";
                    }
                }
            } else {
                //None or Epired
                Status = "None";
                DateUpdate = "-";
                if ((fDedLine !=null) && (DateTime.Now > fDedLine))
                    Status = "Expired";
            }
            return Status;
        }
        public void OnPropertyChanged(string aPropertyName) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(aPropertyName));           
        }

    }
}
