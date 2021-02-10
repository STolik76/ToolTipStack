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
        public event DFileStatusChanged FileStatusChanged;
        public CFileChecker Owner { get; set; }
        public CDir(string aDirName) {
            DirName = aDirName;
            Files = new ObservableCollection<CFile>();
        }
        public void Add(string aPath, DateTime? aDedTime) {
            CFile f = new CFile(aPath, aDedTime);
            f.Owner = Owner;
            //f.FileStatusChanged += FileStatusChanged;
            Files.Add(f); ;
        }
        public void CheckAllFiles()        {
            foreach (CFile f in Files)
                f.Check();
        }
    }
}
