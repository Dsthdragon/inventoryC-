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
    /// Interaction logic for ReportDialog.xaml
    /// </summary>
    public partial class ReportDialog : Window
    {
        Report report;
        public ReportDialog()
        {
            Owner = Application.Current.MainWindow;
            report = new Report();
            report.StartDate = DateTime.Now;
            report.EndDate = DateTime.Now;
            this.SetDataContext();
            InitializeComponent();
            this.reportDialogTitle.Text = "Add New Report";
        }

        public void SetDataContext()
        {
            this.DataContext = report;
        }

        private void saveReportButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Start - " + this.report.StartDate);
            Debug.WriteLine("Start - " + this.report.EndDate);
            if(this.report.EndDate.Date > DateTime.Today.Date)
            {
                DefaultDialog defaultDialog = new DefaultDialog(this, "", "End Date Cant be greater than today");
                defaultDialog.ShowDialog();
                return;
            }
            if(this.report.StartDate.Date > this.report.EndDate.Date)
            {
                DefaultDialog defaultDialog = new DefaultDialog(this, "", "Start Date cant be greater than End Date");
                defaultDialog.ShowDialog();
                return;
            }

            try
            {
                using (var db = new ApplicationDBContext())
                {
                    this.report.ReportItems = new List<ReportItem>();
                    List<Transaction> transactions = db.Transactions.Where(t => t.Created.Date >= report.StartDate.Date && t.Created.Date <= report.EndDate.Date).ToList();
                    foreach(Transaction transaction in transactions)
                    {
                        this.report.TotalTransactions += 1;

                        if (transaction.Credit)
                        {
                            report.Received += transaction.Amount;
                        }
                        else
                        {
                            report.Taken += transaction.Amount;
                        }
                    }
                    db.Reports.Add(report);
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

        private void closeReportDialog_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
