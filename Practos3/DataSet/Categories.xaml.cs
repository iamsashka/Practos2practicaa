using Practos3.DataSet.MagazinCosmetikiPractos3DataSetTableAdapters;
using Practos3.Entity;
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
    /// Логика взаимодействия для Categories.xaml
    /// </summary>
    public partial class Categories : Page
    {
        CategoriesTableAdapter categories = new CategoriesTableAdapter();
        public Categories()
        {
            InitializeComponent();
            CategoriesGrid.ItemsSource = categories.GetData();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            categories.InsertQuery(CategoriiNameTbx.Text);
            CategoriesGrid.ItemsSource = categories.GetData();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (CategoriesGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a row to delete.", "No Row Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DataRowView selectedRow = (DataRowView)CategoriesGrid.SelectedItem;
            int Original_CategoriiID = (int)selectedRow.Row["CategoriiID"];
            string Original_CategoriiName = (string)selectedRow.Row["CategoriiName"];

            try
            {
                categories.DeleteQuery(Original_CategoriiID, Original_CategoriiName);
                CategoriesGrid.ItemsSource = categories.GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
             if(CategoriesGrid.SelectedItem != null)
            {
                object id = (CategoriesGrid.SelectedItem as DataRowView).Row[0];
                categories.UpdateQuery(CategoriiNameTbx.Text, Convert.ToInt32(id));
                CategoriesGrid.ItemsSource = categories.GetData();
            }
        }
        private void CategoriesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoriesGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)CategoriesGrid.SelectedItem;
                CategoriiNameTbx.Text = selectedRow["CategoriiName"].ToString();
            }
        }
    }
}
   
