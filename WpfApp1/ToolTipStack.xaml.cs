﻿using System;
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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Diagnostics;


namespace Assistant {
    /// <summary>
    /// Логика взаимодействия для Glass.xaml
    /// </summary>
    public partial class CToolTipStack: Window {
        //private int Count;
        public ObservableCollection<CToolTip> Items { get; set; }
        public CToolTipStack() {
            InitializeComponent();
            Items = new ObservableCollection<CToolTip>();
            lbToolTip.ItemsSource = Items;
       
        }
        private bool IsExist(string aMessage, string aTitle) {
            foreach (CToolTip t in Items)
                if ((t.Message == aMessage) && (t.Title == aTitle))
                    return true;
            return false;
        }
        public void Push(string aMessage, int aNum, string aTitle = "", int aDelay = 3) {
            if (IsExist(aMessage, aTitle) == false) {
                CToolTip t = new CToolTip(aMessage, aTitle, aDelay, aNum, this);
                //Count++;
                Items.Add(t);
            }
        }
        public void Remove(int aNum) {
            //Debug.WriteLine(aNum.ToString());
            try {            
                Items.Remove(Items.Single(x => x.Num == aNum));
            }
            catch (Exception e) {
                Debug.WriteLine("Нет");
            }
         }
        private void Close_Button_Click(object sender, RoutedEventArgs e) {
            Control c = (Control)sender;
            int n = Convert.ToInt32(c.Tag);
            CToolTip tp = Items.Single(x => x.Num == n);
            tp.Dispose();
            Remove(n);
            //Debug.WriteLine(n.ToString());
        }
        public void SetPosition(int aLeft, int aTop, int aWidth, int aHeight) {
            Left = aLeft;
            Top = aTop;
            Width = aWidth;
            Height = aHeight;
        }

    }
}
