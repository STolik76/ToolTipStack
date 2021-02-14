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

namespace Assistant {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public CFileChecker FlCh;
        public CToolTipStack ToolTipStack = new CToolTipStack();
        public ObservableCollection<CDir> Dirs = new ObservableCollection<CDir>();
        public MainWindow() {
            InitializeComponent();
            ToolTipStack.Show();
            FlCh = new CFileChecker(@"d:\PTTestConf.xlsx", ToolTipStack, 30);
            icFiles.ItemsSource = FlCh.Dirs;
        }

        private void button1_Click(object sender, RoutedEventArgs e) {
            //int i = Convert.ToInt32(textBox.Text) * 1000;
            //ToolTipStack.Push($"text Delay {i} ms dfgfdgf dfgfdg gfdgfdg dfgfdgfd dfgfdg dfgfdgw dgfgdfg werewrew xds", $"title {Count}", i);
            //Count++;
        }

        private void Window_Closed(object sender, EventArgs e) {
            ToolTipStack.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) {
            //CDir d = new CDir("Temp");
            //d.Add(@"d:\Temp\1\Q.xml", new DateTime(2021, 1, 29, 21, 10, 0));
            //Dirs.Add(d);
            //icFiles.ItemsSource = Dirs;
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e) {
            foreach (CDir d in Dirs)
                d.CheckAllFiles();
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            FlCh = new CFileChecker(@"d:\PTTestConf.xlsx", ToolTipStack, 30);
            icFiles.ItemsSource = FlCh.Dirs;
        }
    }
}
