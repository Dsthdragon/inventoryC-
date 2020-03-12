using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TurboInventory.Dialogs;
using TurboInventory.Models;

namespace TurboInventory.Pages
{
    /// <summary>
    /// Interaction logic for Contacts.xaml
    /// </summary>
    

    public partial class Contacts : Page, INotifyPropertyChanged
    {

        private ObservableCollection<Contact> _contacts { get; set; }
        public ObservableCollection<Contact> contacts { 
            get { return _contacts; } 
            set { 
                _contacts = value;
                RaisePropertyChanged("contacts");
            } 
        }

        public Contacts()
        {
            InitializeComponent();
            contactGrid.DataContext = this;
            this.GetContacts();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        void GetContacts()
        {
            using (var db = new ApplicationDBContext())
            {
                contacts = new ObservableCollection<Contact>(db.Contacts.ToList());
            }
        }

        private void editContact_Click(object sender, RoutedEventArgs e)
        {
            Contact contact = (Contact)contactGrid.SelectedItem;
            contactGrid.UnselectAll();
            ContactDialog contactDialog = new ContactDialog(contact);
            bool? response = contactDialog.ShowDialog();
            if (response == true)
            {
                this.GetContacts();
            }
        }

        private void AddContact_Click(object sender, RoutedEventArgs e)
        {
            ContactDialog contactDialog = new ContactDialog();
            bool? response = contactDialog.ShowDialog();
            if (response == true)
            {
                this.GetContacts();
            }
        }
    }
}
