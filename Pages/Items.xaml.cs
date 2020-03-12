using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for Items.xaml
    /// </summary>
    public partial class Items : Page, INotifyPropertyChanged
    {

        private ObservableCollection<Item> _items { get; set; }
        public ObservableCollection<Item> items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged("items");
            }
        }
        public Items()
        {
            InitializeComponent();
            itemGrid.DataContext = this;
            this.GetItems();
        }

        void GetItems()
        {
            using (var db = new ApplicationDBContext())
            {
                items = new ObservableCollection<Item>(db.Items.ToList());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void editItem_Click(object sender, RoutedEventArgs e)
        {
            Item item = (Item)itemGrid.SelectedItem;
            itemGrid.UnselectAll();
            ItemDialog itemDialog = new ItemDialog(item);
            bool? response = itemDialog.ShowDialog();
            if (response == true)
            {
                this.GetItems();
            }
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            ItemDialog itemDialog = new ItemDialog();
            bool? response = itemDialog.ShowDialog();
            if (response == true)
            {
                this.GetItems();
            }
        }
    }
}
