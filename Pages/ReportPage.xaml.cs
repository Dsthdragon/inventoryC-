using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
using TurboInventory.Models;

namespace TurboInventory.Pages
{
    /// <summary>
    /// Interaction logic for ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        public Report report { get; set; }
        public ICollection<Transaction> transactions { get; set; }

        public ReportPage(Report report)
        {
            this.report = report;
            using (var db = new ApplicationDBContext())
            {
                this.transactions = db.Transactions.Where(t => t.Created.Date >= report.StartDate.Date && t.Created.Date <= report.EndDate.Date)
                    .Include("Receiver").Include("Issuer")
                    .Include("Item").ToList();
            }

            this.DataContext = this;
            InitializeComponent();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        void GenerateExcel()
        {
            transactionGrid.SelectAllCells();
            transactionGrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, transactionGrid);
            String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            String result = (string)Clipboard.GetData(DataFormats.Text);
            transactionGrid.UnselectAllCells();
            System.IO.StreamWriter file1 = new System.IO.StreamWriter("test.xls");
            file1.WriteLine(result.Replace(',', ' '));
            file1.Close();

            MessageBox.Show(" Exporting DataGrid data to Excel file created.xls");
        }

        private void GenerateXLS_Click(object sender, RoutedEventArgs e)
        {
            GenerateExcel();
        }
    }
}
