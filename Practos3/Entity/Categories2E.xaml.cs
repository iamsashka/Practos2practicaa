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
    /// Логика взаимодействия для Categories2E.xaml
    /// </summary>
    public partial class Categories2E : Page
    {
        private MagazinCosmetikiPractos3Entities contextCat = new MagazinCosmetikiPractos3Entities();

        public Categories2E()
        {
            InitializeComponent();
            Categoriesdg.ItemsSource = contextCat.Categories.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new MagazinCosmetikiPractos3Entities())
            {
                var newCategories = new Practos3.Entity.Categories 
                { 
                    CategoriiName = tbCategoriiName1.Text 
                };
                context.Categories.Add(newCategories);
                context.SaveChanges();
            }
            Categoriesdg.ItemsSource = contextCat.Categories.ToList();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (Categoriesdg.SelectedItem == null)
            {
                MessageBox.Show("Please select a row to delete.", "No Row Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedCategories = Categoriesdg.SelectedItem as Practos3.Entity.Categories;
            if (selectedCategories == null)
            {
                MessageBox.Show("Invalid selection.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var context = new MagazinCosmetikiPractos3Entities())
                {
                    context.Categories.Attach(selectedCategories);
                    context.Categories.Remove(selectedCategories);
                    context.SaveChanges();
                }

                Categoriesdg.ItemsSource = contextCat.Categories.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (Categoriesdg.SelectedItem == null)
            {
                MessageBox.Show("Please select a row to update.", "No Row Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedCategories = Categoriesdg.SelectedItem as Practos3.Entity.Categories;
            if (selectedCategories == null)
            {
                MessageBox.Show("Invalid selection.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var context = new MagazinCosmetikiPractos3Entities())
                {
                    context.Categories.Attach(selectedCategories);
                    selectedCategories.CategoriiName = tbCategoriiName1.Text;
                    context.SaveChanges();
                }

                Categoriesdg.ItemsSource = contextCat.Categories.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Update Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void tbCategoriiName_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        private void Categoriesdg_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (Categoriesdg.SelectedItem != null)
            {
                Practos3.Entity.Categories selectedCategorii = Categoriesdg.SelectedItem as Practos3.Entity.Categories;
                if (selectedCategorii != null)
                {
                    tbCategoriiName1.Text = selectedCategorii.CategoriiName;
                }
            }
        }
    }
}
