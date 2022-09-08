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

namespace Dubovskiy_TelephoneGuide
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonPerson_Click(object sender, RoutedEventArgs e)
        {
            Showbd("Открыть");
        }

        private void Showbd(string choice)
        {
            show_tab fir = new show_tab(choice);
            fir.Show();
            Close();
        }

        
    }
}
