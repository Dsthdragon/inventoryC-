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
    /// Interaction logic for DefaultDialog.xaml
    /// </summary>
    public partial class DefaultDialog : Window
    {
        public DefaultDialog()
        {
            InitializeComponent();
        }
        public DefaultDialog(Window parent, string title, string caption)
        {
            Owner = parent;
            Title = title;
            InitializeComponent();
            dialogText.Text = caption;
        }
        public DefaultDialog(Window parent, string title, string caption, string image)
        {
            Owner = parent;
            Title = title;
            InitializeComponent();
            dialogText.Text = caption;
            dialogImage.Source = new BitmapImage(new Uri(image, UriKind.Relative));
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
