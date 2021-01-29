using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Assistant {
    public class CDir {
        public string DirName { get; set; }
        public ObservableCollection<CFile> Files { get; set; }
        public CDir(string aDirName) {
            DirName = aDirName;
            Files = new ObservableCollection<CFile>();
        }
        public void Add(string aPath, DateTime aDedTime) {
            Files.Add(new CFile(aPath, aDedTime));
        }
        public void CheckAllFiles()        {
            foreach (CFile f in Files)
                f.Check();
        }
    }
}
