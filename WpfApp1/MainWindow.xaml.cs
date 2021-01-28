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
        private int Count;
        public CToolTipStack ToolTipStack = new CToolTipStack();
        public ObservableCollection<CDir> Dirs = new ObservableCollection<CDir>();
        public MainWindow() {
            InitializeComponent();
            ToolTipStack.Show();
        }

     
        private void button1_Click(object sender, RoutedEventArgs e) {
            int i = Convert.ToInt32(textBox.Text) * 1000;
            ToolTipStack.Push($"text Delay {i} ms dfgfdgf dfgfdg gfdgfdg dfgfdgfd dfgfdg dfgfdgw dgfgdfg werewrew xds", $"title {Count}", i);
            Count++;
        }

        private void Window_Closed(object sender, EventArgs e) {
            ToolTipStack.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            CDir d1 = new CDir();
            d1.Files.Add(new CFile());
            d1.Files.Add(new CFile());
            d1.Files.Add(new CFile());
            Dirs.Add(d1);
            CDir d2 = new CDir();
            d2.Files.Add(new CFile());
            Dirs.Add(d2);
            CDir d3 = new CDir();
            d3.Files.Add(new CFile());
            d3.Files.Add(new CFile());
            d3.Files.Add(new CFile());
            Dirs.Add(d3);
            CDir d4 = new CDir();
            d4.Files.Add(new CFile());
            d4.Files.Add(new CFile());
            Dirs.Add(d4);
            lbFiles.ItemsSource = Dirs;
        }
    }
}
