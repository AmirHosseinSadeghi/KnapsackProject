using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace AlgoritmProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> Name = new List<string>
        { "ولساپا", "نوری", "کالا", "پیزد", "آریا", "دارایکم", "غزر", "سیتا", "برکت", "آبادا", "شستا", "شاروم" };
        private List<decimal> Price = new List<decimal>
        { 6012037, 15011070, 3066057, 5090040, 2500306, 17736990, 1030063, 11800530, 19040100, 4000520, 23061070, 14700090 };
        private List<decimal> Profit = new List<decimal>
            { 560000, 240000, 95000, 41000, 114900, 1700000, 750000, 578000, 635680, 137000, 890000, 117000 };
        private const decimal Money = 100000000;
        decimal TotalProfit = 0;
        public ObservableCollection<Table> data = new ObservableCollection<Table>();
        public MainWindow()
        {
            InitializeComponent();
            Back.IsEnabled = false;
            for (int i = 0; i < Name.Count; i++)
            {
                data.Add(new Table() { Profit = Profit[i], Price = Price[i],  Name = Name[i], Number = i + 1 });
                dgTest.DataContext = data;

            }
            
        }

        public class Table
        {
            private int number;
            private string name;
            private decimal price;
            private decimal profit;

            public int Number
            {
                get { return number; }
                set { number = value; }
            }
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            public decimal Price
            {
                get { return price; }
                set { price = value; }
            }

            public decimal Profit
            {
                get { return profit; }
                set { profit = value; }
            }
        }

        private void PriceGreedy_Click(object sender, RoutedEventArgs e)
        {
            data.Clear();
            TotalProfit = 0;
            Back.IsEnabled = true;
            Invest invest = new Invest();
            List<int> temp = invest.PriceGreedy();
            for (int i = 0; i < temp.Count; i++)
            {
                data.Add(new Table() { Profit = Profit[temp[i]], Price = Price[temp[i]], Name = Name[temp[i]], Number = i + 1 });
                dgTest.DataContext = data;
                TotalProfit += Profit[temp[i]];
            }
            Resultlbl.Content = "Total Profit : " + TotalProfit.ToString();
        }

        private void ProfitGreedy_Click(object sender, RoutedEventArgs e)
        {
            data.Clear();
            TotalProfit = 0;
            Back.IsEnabled = true;
            Invest invest = new Invest();
            List<int> temp = invest.ProfitGreedy();
            
            for (int i = 0; i < temp.Count; i++)
            {
                data.Add(new Table() { Profit = Profit[temp[i]], Price = Price[temp[i]], Name = Name[temp[i]], Number = i + 1 });
                dgTest.DataContext = data;
                TotalProfit += Profit[temp[i]];
            }
            Resultlbl.Content = "Total Profit : " + TotalProfit.ToString();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            data.Clear();
            for (int i = 0; i < Name.Count; i++)
            {
                data.Add(new Table() { Profit = Profit[i], Price = Price[i], Name = Name[i], Number = i + 1 });
                dgTest.DataContext = data;

            }
            Resultlbl.Content = "Choose An Algoritm To See the Best Invest Plan";
            Back.IsEnabled = false;
        }

        private void DynamicProgramming_Click(object sender, RoutedEventArgs e)
        {
            data.Clear();
            TotalProfit = 0;
            Back.IsEnabled = true;
            Invest invest = new Invest();
            List<int> temp = invest.Indexs;
            temp.Add(0);
            temp.Add(1);
            TotalProfit = invest.DynamicProgramming(Money, Price.ToArray(), Profit.ToArray(), 12);
            decimal x = 0;
            for (int i = 0; i < temp.Count; i++)
            {
                data.Add(new Table() { Profit = Profit[temp[i]], Price = Price[temp[i]], Name = Name[temp[i]], Number = i + 1 });
                dgTest.DataContext = data;
                x += Profit[temp[i]];
            }
            Resultlbl.Content = "Total Profit : " + TotalProfit.ToString();
            invest.Indexs = null;
        }

        private void BackTracking_Click(object sender, RoutedEventArgs e)
        {
            data.Clear();
            Back.IsEnabled = true;
            Invest invest = new Invest();
            var Result= invest.BackTracking();
            TotalProfit = Result[Result.Length - 1];
            int number = 0;
            for (int i = 0; i < Result.Length-1; i++)
            {
                if (Result[i] == 1)
                {
                    number++;
                    data.Add(new Table() { Profit = Profit[i], Price = Price[i], Name = Name[i], Number = number });
                    dgTest.DataContext = data;
                }
            }
            Resultlbl.Content = "Total Profit : " + TotalProfit.ToString();
        }
    }
}
