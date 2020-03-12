using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TurboInventory.Utils
{
    /// <summary>
    /// Interaction logic for YesOrNoDialog.xaml
    /// </summary>
    public partial class YesOrNoDialog : Window
    {
        public YesOrNoDialog()
        {
            InitializeComponent();
        }
        public YesOrNoDialog(Window parent, string title, string caption)
        {
            Owner = parent;
            Title = title;
            InitializeComponent();
            dialogText.Text = caption;
        }
        public YesOrNoDialog(Window parent, string title, string caption, string image)
        {
            Owner = parent;
            Title = title;
            InitializeComponent();
            dialogText.Text = caption;
            dialogImage.Source = new BitmapImage(new Uri(image, UriKind.Relative));
        }


        void ClosedDialog(bool result)
        {
            this.DialogResult = result;
            this.Close();
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            this.ClosedDialog(true);
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            this.ClosedDialog(false);
        }
    }
}
