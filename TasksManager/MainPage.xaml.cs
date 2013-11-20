using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.DomainServices.Client;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using TasksManager.Web;

namespace TasksManager
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void taskDomainDataSource_LoadedData(object sender, System.Windows.Controls.LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime lowerDateVal;
            DateTime upperDateVal;
            GetDates(out lowerDateVal, out upperDateVal);

            TasksDomainContext context = new TasksDomainContext();
            taskDataGrid.ItemsSource = context.Tasks;
            EntityQuery<Task> query = context.GetTasksByStartDateQuery(lowerDateVal, upperDateVal);
            LoadOperation<Task> loadOp = context.Load(query);
        }

        private void GetDates(out DateTime lowerDateVal, out DateTime upperDateVal)
        {
            lowerDateVal = DateTime.MinValue;
            upperDateVal = DateTime.MaxValue;
            if (!string.IsNullOrWhiteSpace(lowerDate.Text))
            {
                lowerDateVal = DateTime.Parse(lowerDate.Text);
            }
            if (!string.IsNullOrWhiteSpace(upperDate.Text))
            {
                upperDateVal = DateTime.Parse(upperDate.Text);
            }
        }

    }
}
