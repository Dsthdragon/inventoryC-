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
    /// Interaction logic for ContactDialog.xaml
    /// </summary>
    public partial class ContactDialog : Window
    {
        Contact contact;
        public ContactDialog()
        {
            Owner = Application.Current.MainWindow;
            contact = new Contact();
            this.SetDataContext();
            InitializeComponent();
            this.contactDialogTitle.Text = "Add New Contact";
        }

        public ContactDialog(Contact value)
        {
            Owner = Application.Current.MainWindow;
            contact = value;
            this.SetDataContext();
            InitializeComponent();
            this.contactDialogTitle.Text = "Edit Contact Details";
        }

        public void SetDataContext()
        {
            this.DataContext = contact;
        }

        private void saveContactButton_Click(object sender, RoutedEventArgs e)
        {
            if (contact.Name == null)
            {
                DefaultDialog defaultDialog = new DefaultDialog(this, "", "Contact Name is required!");
                defaultDialog.ShowDialog();
                return;
            }
            try
            {

                System.Diagnostics.Debug.WriteLine(contact.Id);
                using (var db = new ApplicationDBContext())
                {
                    if (contact.Id != 0)
                    {
                        db.Contacts.Update(contact);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Contacts.Add(contact);
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

        private void closeContactDialog_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
