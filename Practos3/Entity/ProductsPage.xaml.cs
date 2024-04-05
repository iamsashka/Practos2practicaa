using Practos3.Entity;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        private MagazinCosmetikiPractos3Entities contextPro = new MagazinCosmetikiPractos3Entities();

        public ProductsPage()
        {
            InitializeComponent();
            ProductsPagedg.ItemsSource = contextPro.Products.ToList();
            LoadCategoriesComboBox();
            LoadCountriesComboBox();
        }

        private void LoadCategoriesComboBox() { 
            NameTbx2.ItemsSource = contextPro.Categories.ToList(); 
            NameTbx2.DisplayMemberPath = "CategoriiName"; 
            NameTbx2.SelectedValuePath = "CategoriiID"; 
        }

        private void LoadCountriesComboBox()
        {
            NameTbx3.ItemsSource = contextPro.Country.ToList();
            NameTbx3.DisplayMemberPath = "CountryName";
            NameTbx3.SelectedValuePath = "CountryID";
        }
        private void ClearFields()
        {
            NameTbx1.Clear();
            NameTbx2.SelectedIndex = -1;
            NameTbx3.SelectedIndex = -1;
            NameTbx4.Clear();
            
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
            var newProduct = new Practos3.Entity.Products();
            newProduct.ProductName = NameTbx1.Text;
            newProduct.CategoriiID = (int)NameTbx2.SelectedValue;
            newProduct.CountryID = (int)NameTbx3.SelectedValue;
            newProduct.Price = decimal.Parse(NameTbx4.Text);
            

            contextPro.Products.Add(newProduct);
            contextPro.SaveChanges();

            ProductsPagedg.ItemsSource = contextPro.Products.ToList();
            ClearFields();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsPagedg.SelectedItem != null)
            {
                // Получаем выбранный продукт
                var selectedProduct = ProductsPagedg.SelectedItem as Practos3.Entity.Products;

                // Обновляем данные выбранного продукта
                selectedProduct.ProductName = NameTbx1.Text;
                selectedProduct.CategoriiID = (int)NameTbx2.SelectedValue;
                selectedProduct.CountryID = (int)NameTbx3.SelectedValue;
                selectedProduct.Price = decimal.Parse(NameTbx4.Text);

                // Сохраняем изменения в базе данных
                contextPro.SaveChanges();

                // Обновляем отображение таблицы с продуктами
                ProductsPagedg.ItemsSource = contextPro.Products.ToList();

                // Очищаем поля ввода
                ClearFields();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsPagedg.SelectedItem != null)
            {
                var selectedProduct = ProductsPagedg.SelectedItem as Practos3.Entity.Products;
                contextPro.Products.Remove(selectedProduct);
                contextPro.SaveChanges();
                ProductsPagedg.ItemsSource = contextPro.Products.ToList();
                ClearFields();
            }
        }


        private void ProductsPagedg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductsPagedg.SelectedItem != null)
            {
                var selectedProduct = ProductsPagedg.SelectedItem as Practos3.Entity.Products;
                NameTbx2.SelectedValue = selectedProduct.CategoriiID;
                NameTbx3.SelectedValue = selectedProduct.CountryID;
            }
        }

        private void NameTbx2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NameTbx2.SelectedItem != null)
            {
                var selectedCategory = NameTbx2.SelectedItem as Practos3.Entity.Categories;
                LoadProductsByCategories(selectedCategory.CategoriiID); // corrected
            }
        }

        private void NameTbx3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NameTbx3.SelectedItem != null)
            {
                var selectedCountry = NameTbx3.SelectedItem as Practos3.Entity.Country;
                LoadProductsByCountry(selectedCountry.CountryID);
            }
        }

        private void LoadProductsByCategories(int categoriiID)
        {
            ProductsPagedg.ItemsSource = contextPro.Products.Where(p => p.CategoriiID == categoriiID).ToList();
        }

        private void LoadProductsByCountry(int countryID)
        {
            ProductsPagedg.ItemsSource = contextPro.Products.Where(p => p.CountryID == countryID).ToList();
        }
    }
}