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

namespace Turfirm
{
    /// <summary>
    /// Логика взаимодействия для ListOfTour.xaml
    /// </summary>
    public partial class ListOfTour : Page
    {
        public ListOfTour()  
        {
            InitializeComponent();
            listTour.ItemsSource = BD.tBE.Tour.ToList(); 

            List<Tour> BT = BD.tBE.Tour.ToList();


            cmbSort.Items.Add("Все типы");  
            for (int i = 0; i < BT.Count; i++)  
            {
                cmbSort.Items.Add(BT[i].TypeOfTour);
            }

            cmbSort.SelectedIndex = 0;  // выбранное по умолчанию значение в списке с породами котов ("Все туры")

        }

      

    



        void Filter()  // метод для одновременной фильтрации, поиска и сортировки
        {
            List<Tour> ListTour = new List<Tour>();

            ListTour = BD.tBE.Tour.ToList();
          
            if (!string.IsNullOrWhiteSpace(tbSearch.Text))  
            {
                ListTour = ListTour.Where(x => x.Name.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
            }

            

            // выбор элементов только с актуальностью
            if (cbActual.IsChecked == true)
            {
                ListTour = ListTour.Where(x => x.IsActual != false).ToList();
            }

            


            
            switch (cmbSort.SelectedIndex)
            {
                case 0:
                    {
                        ListTour.Sort((x, y) => x.Name.CompareTo(y.Name));
                    }
                    break;
                case 1:
                    {
                        ListTour.Sort((x, y) => x.Name.CompareTo(y.Name));
                        ListTour.Reverse();
                    }
                    break;
            }



            listTour.ItemsSource = ListTour;
            if (ListTour.Count == 0)
            {
                MessageBox.Show("нет записей");
                tbSearch.Text = "";
            }

            decimal b = 0;
            foreach (Tour t in ListTour)
            {
                b = b + t.Price;
            }
            CountTour.Text = "Общая стоимость туров: " + b;

        }

        
        private void cmbBreed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cbPhoto_Checked(object sender, RoutedEventArgs e)
        {
            Filter();
        }
    }
}
