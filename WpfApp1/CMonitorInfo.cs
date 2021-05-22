using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assistant {
    public class CMonitorInfo {
        public string Name { get; set; }
        public bool Primary { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int WorkingAreaX { get; set; }
        public int WorkingAreaY { get; set; }
        public int WorkingAreaWidth { get; set; }
        public int WorkingAreaHeight { get; set; }
        CMonitorInfo(Screen aScreen) {
            Name = aScreen.DeviceName;
            Primary = aScreen.Primary;

        }
    }
}
