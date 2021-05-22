using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace Assistant {
    public class CConf {
        public ObservableCollection<Screen> Monitors { get; }
        public CConf() {
            Monitors = new ObservableCollection<Screen>();
            Screen[] sc = Screen.AllScreens;
            foreach (Screen s in sc) {
                Monitors.Add(s);
            }
        }
    }
}
