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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CakeShopWPF
{
    /// <summary>
    /// Interaction logic for DetailProductPage.xaml
    /// </summary>
    public partial class DetailProductPage : Page
    {
        public int CakeId { get; set; }
        public DetailProductPage(int cakeId)
        {
            InitializeComponent();
            CakeId = cakeId;
        }

        private void DetailProductPage_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
