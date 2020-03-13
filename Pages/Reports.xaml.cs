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
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Page, INotifyPropertyChanged
    {


        private ObservableCollection<Report> _reports { get; set; }
        public ObservableCollection<Report> reports
        {
            get { return _reports; }
            set
            {
                _reports = value;
                RaisePropertyChanged("reports");
            }
        }

        public Reports()
        {
            InitializeComponent();
            reportGrid.DataContext = this;
            this.GetReports();
        }

        void GetReports()
        {
            using (var db = new ApplicationDBContext())
            {
                reports = new ObservableCollection<Report>(db.Reports.ToList());
            }
        }
        private void AddReport_Click(object sender, RoutedEventArgs e)
        {
            ReportDialog reportDialog = new ReportDialog();
            bool? response = reportDialog.ShowDialog();
            //if (response == true)
            //{
            //    this.GetItems();
            //}
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void viewReport_Click(object sender, RoutedEventArgs e)
        {

            Report item = (Report)reportGrid.SelectedItem;
            reportGrid.UnselectAll();
            ReportPage itemPage = new ReportPage(item);
            this.NavigationService.Navigate(itemPage);
        }
    }
}
