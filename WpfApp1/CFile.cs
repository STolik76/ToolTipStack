using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
namespace Assistant {
    public class CFile : INotifyPropertyChanged  {
        private DateTime fDateUpdate;
        private DateTime fDedLine;
        private string fPath;
        private string fFileName;
        private string fStatus;
        private string fDateUpdateStr;
        public event PropertyChangedEventHandler PropertyChanged;
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
            }
        }
        
        public CFile(string aPath, DateTime aDedLine) {
            fPath = aPath;
            fDedLine = aDedLine;
            DateUpdate = "-";
            Status = "None";
            FileName = Path.GetFileName(fPath);
            fDateUpdate = DateTime.Now;
        }
        public string Check() {
            if (File.Exists(fPath) == true) {
                DateTime t = File.GetLastWriteTime(fPath);
                if ((Status == "None") || (Status == "Expired")) {
                    //Exist
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
                if (DateTime.Now > fDedLine)
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
