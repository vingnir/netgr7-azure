using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Blocking_NonBlocking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSync_Click(object sender, RoutedEventArgs e)
        {
            tblockResult.Text = "";

            Thread.Sleep(10000);
            tblockResult.Text = "Blocking code done!";
        }

        private async void btnAsync_Click(object sender, RoutedEventArgs e)
        {
            tblockResult.Text = "";

            await Task.Delay(10000);
            tblockResult.Text = "Non-Blocking code done!";
        }
    }
}
