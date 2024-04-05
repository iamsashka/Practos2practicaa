
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using Practos3.Entity;

namespace Practos3
{
    /// <summary>
    /// Логика взаимодействия для Country2E.xaml
    /// </summary>
    public partial class Country2E : Page
    {
        private MagazinCosmetikiPractos3Entities contextCoun = new MagazinCosmetikiPractos3Entities();
        public Country2E()
        {
            InitializeComponent();
            CountryGrd.ItemsSource = contextCoun.Country.ToList();

        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Practos3.Entity.Country c = new Practos3.Entity.Country();
            // Add new country to the database
            using (var context = new MagazinCosmetikiPractos3Entities())
            {
                c.CountryName = tbCountryName.Text;
                context.Country.Add(c);
                context.SaveChanges();
            }
            // Update the CountryGrd data grid
            CountryGrd.ItemsSource = contextCoun.Country.ToList();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (CountryGrd.SelectedItem == null)
            {
                MessageBox.Show("Please select a country to delete.", "No Country Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedCountry = CountryGrd.SelectedItem as Practos3.Entity.Country;
            if (selectedCountry == null)
            {
                MessageBox.Show("Invalid selection.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var context = new MagazinCosmetikiPractos3Entities())
                {
                    context.Country.Attach(selectedCountry);
                    context.Country.Remove(selectedCountry);
                    context.SaveChanges();
                }

                CountryGrd.ItemsSource = contextCoun.Country.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (CountryGrd.SelectedItem == null)
            {
                MessageBox.Show("Please select a country to update.", "No Country Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedCountry = CountryGrd.SelectedItem as Practos3.Entity.Country;
            if (selectedCountry == null)
            {
                MessageBox.Show("Invalid selection.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var context = new MagazinCosmetikiPractos3Entities())
                {
                    context.Country.Attach(selectedCountry);
                    selectedCountry.CountryName = tbCountryName.Text;
                    context.SaveChanges();
                }

                CountryGrd.ItemsSource = contextCoun.Country.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Update Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void CountryGrd_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (CountryGrd.SelectedItem != null)
            {
                Practos3.Entity.Country selectedCountry = CountryGrd.SelectedItem as Practos3.Entity.Country;
                if (selectedCountry != null)
                {
                    tbCountryName.Text = selectedCountry.CountryName;
                }
            }
        }
    }
}

