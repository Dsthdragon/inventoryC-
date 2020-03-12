using Microsoft.EntityFrameworkCore;
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
using TurboInventory.Models;
using TurboInventory.Utils;

namespace TurboInventory.Dialogs
{
    /// <summary>
    /// Interaction logic for ItemDialog.xaml
    /// </summary>
    public partial class ItemDialog : Window
    {
        Item item;
        public ItemDialog()
        {
            Owner = Application.Current.MainWindow;
            item = new Item();
            this.SetDataContext();
            InitializeComponent();
            this.itemDialogTitle.Text = "Add New Item";
        }

        public ItemDialog(Item value)
        {
            Owner = Application.Current.MainWindow;
            item = value;
            this.SetDataContext();
            InitializeComponent();
            this.itemDialogTitle.Text = "Edit Item Details";
        }

        public void SetDataContext()
        {
            this.DataContext = item;
        }

        private void saveItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (item.Name == null)
            {
                DefaultDialog defaultDialog = new DefaultDialog(this, "", "Item Name is required!");
                defaultDialog.ShowDialog();
                return;
            }
            try
            {
                using (var db = new ApplicationDBContext())
                {
                    if (item.Id != 0)
                    {
                        db.Items.Update(item);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Items.Add(item);
                        db.SaveChanges();
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }


            this.DialogResult = true;
            this.Close();
        }

        private void closeItemDialog_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
