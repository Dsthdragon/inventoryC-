using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using TurboInventory.Models;
using TurboInventory.Utils;

namespace TurboInventory.Dialogs
{
    /// <summary>
    /// Interaction logic for TransactionDialog.xaml
    /// </summary>
    public partial class TransactionDialog : Window
    {
        Transaction transaction;
        public TransactionDialog()
        {
            Owner = Application.Current.MainWindow;
            transaction = new Transaction();
            this.SetDataContext();
            InitializeComponent();
            this.transactionDialogTitle.Text = "Add New Transaction";
            this.FillCombo();
        }

        public TransactionDialog(Transaction value)
        {
            Owner = Application.Current.MainWindow;
            transaction = value;
            this.SetDataContext();
            InitializeComponent();
            this.transactionDialogTitle.Text = "Edit Transaction Details";
            this.FillCombo();
        }

        public void SetDataContext()
        {
            this.DataContext = transaction;
        }

        public void FillCombo()
        {
            using (var db = new ApplicationDBContext())
            {
                this.ItemCombo.ItemsSource = db.Items.ToList();
                this.ReceiverCombo.ItemsSource = db.Contacts.ToList();
                this.IssuerCombo.ItemsSource = db.Contacts.ToList();
            }
        }

        private void saveTransactionButton_Click(object sender, RoutedEventArgs e)
        {

            Debug.WriteLine("Transaction" + transaction.ToString());
            if (transaction.IssuerId == 0)
            {
                DefaultDialog defaultDialog = new DefaultDialog(this, "", "Issuer is required!");
                defaultDialog.ShowDialog();
                return;
            }
            if (transaction.ReceiverId == 0)
            {
                DefaultDialog defaultDialog = new DefaultDialog(this, "", "Receiver is required!");
                defaultDialog.ShowDialog();
                return;
            }
            if (transaction.ItemId == 0)
            {
                DefaultDialog defaultDialog = new DefaultDialog(this, "", "Item is required!");
                defaultDialog.ShowDialog();
                return;
            }
            if (transaction.Amount == 0)
            {
                DefaultDialog defaultDialog = new DefaultDialog(this, "", "Amount must be greater than 0!");
                defaultDialog.ShowDialog();
                return;
            }
            if (transaction.IssuerId == transaction.ReceiverId)
            {
                DefaultDialog defaultDialog = new DefaultDialog(this, "", "Can not Issure to your self!");
                defaultDialog.ShowDialog();
                return;
            }
            Item item = (Item)ItemCombo.SelectedItem;
            if (transaction.Amount > item.Stock && !transaction.Credit )
            {
                DefaultDialog defaultDialog = new DefaultDialog(this, "", "Cant issue more more than stock!");
                defaultDialog.ShowDialog();
                return;
            }
            try
            {

                System.Diagnostics.Debug.WriteLine(transaction.Id);
                using (var db = new ApplicationDBContext())
                {
                    if(transaction.Credit)
                    {
                        item.Stock += transaction.Amount;
                    } else
                    {
                        item.Stock += transaction.Amount;
                    }
                    db.Transactions.Add(transaction);
                    db.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }


            this.DialogResult = true;
            this.Close();
        }

        private void closeTransactionDialog_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
