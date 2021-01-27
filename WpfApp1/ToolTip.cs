using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;
namespace WpfApp1{
    public delegate void DClosedToolTip(int aNum);
    public class CToolTip : IDisposable {
        public string Message { get; set; }
        public string Title { get; set; }
        public int Num { get; set; }
        private ToolTipStack Owner;
        private bool disposed = false;
        private Timer timer;
        public CToolTip(string aMessage, string aTitle, int aDelay, int aNum, ToolTipStack aOwner) {
            Message = aMessage;
            Title = aTitle;
            Num = aNum;           
            Owner = aOwner;
            timer = new Timer(aDelay);
            timer.Elapsed += TimerElapsed;   
            timer.Start();
        }
        ~CToolTip() {
            Dispose(false);
        }
        private void TimerElapsed(object sender, ElapsedEventArgs e) {
            timer.Stop();
            Owner.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new DClosedToolTip(Owner.Remove), Num);
        }
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool aDisposing) {
            if (!disposed) {
                if (aDisposing) 
                    timer.Stop();
                disposed = true;
            }
        }
    }
}
