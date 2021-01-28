using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistant {
    public class CFile {
        public string File { get; set; }
        public DateTime DateUpdate { get; set; }
        public CFile() {
            DateUpdate = DateTime.Now;
            File = "File-" + DateTime.Now.ToString();
        }
    }
}
