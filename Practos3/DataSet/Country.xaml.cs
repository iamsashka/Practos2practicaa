using Practos3.DataSet.MagazinCosmetikiPractos3DataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Practos3
{
    /// <summary>
    /// Логика взаимодействия для Country.xaml
    /// </summary>
    public partial class Country : Page
    {
        CountryTableAdapter country = new CountryTableAdapter();
        public Country()
        {
            InitializeComponent();
            CountryGrid.ItemsSource = country.GetData();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            country.InsertQuery(CountryNameTbx.Text);
            CountryGrid.ItemsSource = country.GetData();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (CountryGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a row to delete.", "No Row Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DataRowView selectedRow = (DataRowView)CountryGrid.SelectedItem;
            int Original_CountryID = (int)selectedRow.Row["CountryID"];
            string Original_CountryName = (string)selectedRow.Row["CountryName"];

            try
            {
                country.DeleteQuery(Original_CountryID, Original_CountryName);
                CountryGrid.ItemsSource = country.GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (CountryGrid.SelectedItem != null)
            {
                object id = (CountryGrid.SelectedItem as DataRowView).Row[0];
                country.UpdateQuery(CountryNameTbx.Text, Convert.ToInt32(id));
                CountryGrid.ItemsSource = country.GetData();
            }
        }
        private void CountryGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CountryGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)CountryGrid.SelectedItem;
                CountryNameTbx.Text = selectedRow["CountryName"].ToString();
            }
        }
    }
}
