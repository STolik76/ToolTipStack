using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;
namespace Assistant {
    public delegate void DClosedToolTip(int aNum);
    public class CToolTip : IDisposable {
        public string Message { get; set; }
        public string Title { get; set; }
        public int Num { get; set; }
        private CToolTipStack fOwner;
        private bool fDisposed = false;
        private Timer fTimer;
        public CToolTip(string aMessage, string aTitle, int aDelay, int aNum, CToolTipStack aOwner) {
            Message = aMessage;
            Title = aTitle;
            Num = aNum;           
            fOwner = aOwner;
            fTimer = new Timer(aDelay * 1000);
            fTimer.Elapsed += TimerElapsed;   
            fTimer.Start();
        }
        ~CToolTip() {
            Dispose(false);
        }
        private void TimerElapsed(object sender, ElapsedEventArgs e) {
            fTimer.Stop();
            fOwner.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new DClosedToolTip(fOwner.Remove), Num);
        }
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool aDisposing) {
            if (!fDisposed) {
                if (aDisposing) 
                    fTimer.Stop();
                fDisposed = true;
            }
        }
    }
}
