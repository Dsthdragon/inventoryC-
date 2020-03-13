using System;
using System.Drawing;
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
using TurboInventory.Pages;
using TurboInventory.Utils;

namespace TurboInventory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            GoTo(new Home());
        }

        void GoTo(Page page)
        {
            PageFrame.Content = page;
        }

        private void homeButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GoTo(new Home());
        }

        private void contactButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GoTo(new Contacts());
        }

        private void transactionsButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GoTo(new Transactions());
        }

        private void itemButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GoTo(new Items());
        }

        private void reportButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GoTo(new Reports());
        }

        private void exitButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            YesOrNoDialog exitDialog = new YesOrNoDialog(this, "Exit App", "Are You Sure you want to exit the app?", "../Resources/exit.png");
            bool? dialogResult = exitDialog.ShowDialog();
            if (dialogResult == true)
            {
                Application.Current.Shutdown();
            }
            //MessageBoxResult messageBoxResult = MessageBox.Show("Are You Sure?", "Exit Confirmation", MessageBoxButton.YesNo);
            //if(messageBoxResult == MessageBoxResult.Yes)
            //{
            //    Application.Current.Shutdown();
            //}
        }
    }
}
