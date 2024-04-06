using Practos3.DataSet.MagazinCosmetikiPractos3DataSetTableAdapters;
using Practos3.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
    /// Логика взаимодействия для Products.xaml
    /// </summary>
    public partial class Products : Page
    {
        ProductsTableAdapter products = new ProductsTableAdapter();
        CategoriesTableAdapter categories = new CategoriesTableAdapter();
        CountryTableAdapter country = new CountryTableAdapter();

        public Products()
        {
            InitializeComponent();

            ProductsGrid.ItemsSource = products.GetData();
            CategoriiIDTbx.DisplayMemberPath = "CategoriesName";
            
            CountryIDTbx.Items.Clear();
            CountryIDTbx.ItemsSource = country.GetData();
            CategoriiIDTbx.Items.Clear();
            CategoriiIDTbx.ItemsSource = categories.GetData();
            CountryIDTbx.SelectedValuePath = "CountryID";
            CountryIDTbx.DisplayMemberPath = "CountryName";
            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (CategoriiIDTbx.SelectedItem != null && CountryIDTbx.SelectedItem != null)
            {
                DataRowView selectedCategoriesRow = (DataRowView)CategoriiIDTbx.SelectedItem;
                int CategoriiID = (int)selectedCategoriesRow["CategoriiID"];

                DataRowView selectedCountryRow = (DataRowView)CountryIDTbx.SelectedItem;
                int CountryID = (int)selectedCountryRow["CountryID"];

                products.InsertQuery(ProductsNameTbx.Text, CategoriiID, CountryID, Convert.ToDecimal(PriceTbx.Text));

                ProductsGrid.ItemsSource = products.GetData();
                ClearInputs();
            }
        }
        private void ClearInputs()
        {
            ProductsNameTbx.Clear();
            CategoriiIDTbx.SelectedIndex = -1;
            CountryIDTbx.SelectedIndex = -1;
            PriceTbx.Clear();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem != null)
            {
                DataRowView selectedRow = ProductsGrid.SelectedItem as DataRowView;
                int productID = Convert.ToInt32(selectedRow.Row[0]);

                try
                {
                    products.DeleteQuery(productID);
                    ProductsGrid.ItemsSource = products.GetData();
                    ClearInputs();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItems != null && ProductsGrid.SelectedItems.Count == 1)
            {
                DataRowView selectedRow = ProductsGrid.SelectedItems[0] as DataRowView;
                int ProductID = (int)selectedRow.Row[0];

                DataRowView selectedCategory = CategoriiIDTbx.SelectedItem as DataRowView;
                int CategoriiID = (int)selectedCategory.Row[0];

                DataRowView selectedCountry = CountryIDTbx.SelectedItem as DataRowView;
                int CountryID = (int)selectedCountry.Row[0];

                products.UpdateQuery(ProductsNameTbx.Text, CategoriiID, CountryID, decimal.Parse(PriceTbx.Text), ProductID);
                ProductsGrid.ItemsSource = products.GetData();
            }
        }

        private void CategoriiIDTbx_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            object cell = (CategoriiIDTbx.SelectedItem as DataRowView).Row[1];
            MessageBox.Show(cell.ToString());
        }

        private void CountryIDTbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object cell = (CountryIDTbx.SelectedItem as DataRowView).Row[1];
            MessageBox.Show(cell.ToString());
        }
    }

}
