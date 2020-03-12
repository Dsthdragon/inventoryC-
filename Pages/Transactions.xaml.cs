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
    /// Interaction logic for Transactions.xaml
    /// </summary>
    public partial class Transactions : Page, INotifyPropertyChanged
    {

        private ObservableCollection<Transaction> _transactions { get; set; }
        public ObservableCollection<Transaction> transactions
        {
            get { return _transactions; }
            set
            {
                _transactions = value;
                RaisePropertyChanged("transactions");
            }
        }
        public Transactions()
        {
            InitializeComponent();
            transactionGrid.DataContext = this;
            this.GetTransactions();
        }

        void GetTransactions()
        {
            using (var db = new ApplicationDBContext())
            {
                transactions = new ObservableCollection<Transaction>(db.Transactions.ToList());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void editTransaction_Click(object sender, RoutedEventArgs e)
        {
            Transaction transaction = (Transaction)transactionGrid.SelectedItem;
            transactionGrid.UnselectAll();
            TransactionDialog transactionDialog = new TransactionDialog(transaction);
            bool? response = transactionDialog.ShowDialog();
            if (response == true)
            {
                this.GetTransactions();
            }
        }

        private void AddTransaction_Click(object sender, RoutedEventArgs e)
        {
            TransactionDialog transactionDialog = new TransactionDialog();
            bool? response = transactionDialog.ShowDialog();
            if (response == true)
            {
                this.GetTransactions();
            }
        }
    }
}
