using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO;
using System.Diagnostics;

namespace Assistant {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public CFileChecker FlCh;
        public CToolTipStack ToolTipStack = new CToolTipStack();
        public ObservableCollection<CDir> Dirs = new ObservableCollection<CDir>();
        public CConf Conf = new CConf();
        public MainWindow() {
            InitializeComponent();
            ToolTipStack.Show();
            string p = Directory.GetCurrentDirectory() + "\\PTConf.xlsx";
            Debug.WriteLine(p);
            FlCh = new CFileChecker(p, ToolTipStack, 30);
            icFiles.ItemsSource = FlCh.Dirs;
            cbScreensList.ItemsSource = Conf.Monitors;
        }
        private void Window_Closed(object sender, EventArgs e) {
            ToolTipStack.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            
        }



        //private void CheckButton_Click(object sender, RoutedEventArgs e) {
        //    foreach (CDir d in Dirs)
        //        d.CheckAllFiles();
        //}

        //private void button_Click(object sender, RoutedEventArgs e) {
        //    FlCh = new CFileChecker(@"d:\PTTestConf.xlsx", ToolTipStack, 30);
        //    icFiles.ItemsSource = FlCh.Dirs;
        //}
    }
}
