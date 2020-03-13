using Microsoft.EntityFrameworkCore;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TurboInventory.Models;

namespace TurboInventory.Pages
{
    /// <summary>
    /// Interaction logic for Item.xaml
    /// </summary>
    public partial class ItemPage : Page
    {
        public Item item { get; set; }

        //public IList<DataPoint> Points { get; private set; }
        //public IList<DataPoint> Points1 { get; private set; }
        //public IList<DataPoint> Points2 { get; private set; }

        public List<ColumnItem> Points { get; private set; }
        public List<ColumnItem> Points1 { get; private set; }
        public List<ColumnItem> Points2 { get; private set; }
        public List<ColumnItem> Points3 { get; private set; }

        public List<DateTime> PointDates { get; private set; }

        public double minValue { get; set; }
        public double maxValue { get; set; }
        public ItemPage(Item item)
        {
            Debug.WriteLine(item.Name);
            this.item = item;
            using (var db = new ApplicationDBContext())
            {
                this.item.ItemReports = db.ItemReports.Where(ir => ir.ItemId == this.item.Id)
                    .Include("Item").OrderByDescending(ir => ir.Created).ToList();
                this.item.Transactions = db.Transactions.Where(t => t.ItemId == this.item.Id)
                    .Include("Receiver").Include("Issuer")
                    .Include("Item").OrderByDescending(t => t.Created).ToList();
            }


            minValue = DateTimeAxis.ToDouble(DateTime.Now.AddDays(-3));
            maxValue = DateTimeAxis.ToDouble(DateTime.Today);
            this.DataContext = this;
            this.setUpGraph();
            InitializeComponent();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void setUpGraph()
        {
            this.Points = new List<ColumnItem>();
            this.Points1 = new List<ColumnItem>();
            this.Points2 = new List<ColumnItem>();
            this.Points3 = new List<ColumnItem>();
            this.PointDates = new List<DateTime>();
            foreach (ItemReport itemReport in this.item.ItemReports)
            {
                

                this.Points.Add(new ColumnItem(itemReport.Brought));
                this.Points1.Add(new ColumnItem(itemReport.Taken));
                this.Points2.Add(new ColumnItem(itemReport.Remaining));
                this.Points3.Add(new ColumnItem(itemReport.Transactions));

                this.PointDates.Add(itemReport.Created.Date);

                //this.Points.Add(new DataPoint(DateTimeAxis.ToDouble(itemReport.Created.Date), itemReport.Brought));
                //this.Points1.Add(new DataPoint(DateTimeAxis.ToDouble(itemReport.Created.Date), itemReport.Taken));

                //this.Points2.Add(new DataPoint(DateTimeAxis.ToDouble(itemReport.Created.Date), itemReport.Remaining));

            }

            this.Points.Reverse();
            this.Points1.Reverse();
            this.Points2.Reverse();
            this.Points3.Reverse();

            this.PointDates.Reverse();
        }
    }
}
