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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.ComponentModel;

namespace Dubovskiy_TelephoneGuide
{
    /// <summary>
    /// Логика взаимодействия для show_tab.xaml
    /// </summary>
    public partial class show_tab : Window
    {
        Городской_Телефонный_СправочникEntities data;
        string choice;

        SqlConnection con;
        SqlDataAdapter sda;
        DataTable dt;
        SqlCommandBuilder scb;
        DataSet ds;

        public show_tab(string choice)
        {
            InitializeComponent();
            data = Городской_Телефонный_СправочникEntities.GetDatabase1();
            this.choice = choice;


            this.LegalFind.Visibility = Visibility.Hidden; // строка ввода для поиска организаций
            this.NaturalFind.Visibility = Visibility.Hidden; // строка ввода для поиска абонентов
            this.LegalSearch.Visibility = Visibility.Hidden; // кнопка поиск орг
            this.NaturalSearch.Visibility = Visibility.Hidden;//кнопка поиск абон
            this.CityBox.Visibility = Visibility.Hidden; // строка для поиска абонентов по городам/сёлам
            this.OrganizationBox.Visibility = Visibility.Hidden; // строка для поиска организ. по городам/сёлам
            this.CityButton.Visibility = Visibility.Hidden; //кнопка для поиска абон по городам/сёлам
            this.OrganizationButton.Visibility = Visibility.Hidden; //кнопка для поиска орг по городам/сёлам

            this.LabelLegal.Visibility = Visibility.Hidden; //поиск организаций
            this.LabelNatural.Visibility = Visibility.Hidden; //поиск абонента
            this.RadioCity.Visibility = Visibility.Hidden; //радиобатон абонента в городе
            this.RadioVillage.Visibility = Visibility.Hidden; //радиобатон абонента в селе
            this.RadioCity_forOrganization.Visibility = Visibility.Hidden; //радиобатон организации в городе
            this.RadioVillage_forOrganization.Visibility = Visibility.Hidden; //радиобатон организации в селе

            this.LabelCity.Visibility = Visibility.Hidden; //поиск по населённому пункту
            
            
        }

        private void Show_Natural_method(object sender, RoutedEventArgs e)
        {
            
            this.NaturalFind.Visibility = Visibility.Visible;
            this.NaturalSearch.Visibility = Visibility.Visible;
            this.LabelNatural.Visibility = Visibility.Visible;
            this.LabelLegal.Visibility = Visibility.Hidden;

            this.CityBox.Visibility = Visibility.Visible;
            this.CityButton.Visibility = Visibility.Visible;

            this.RadioCity.Visibility = Visibility.Visible;
            this.RadioVillage.Visibility = Visibility.Visible;

            this.LabelCity.Visibility = Visibility.Visible;

            this.OrganizationBox.Visibility = Visibility.Hidden;
            this.OrganizationButton.Visibility = Visibility.Hidden;

            database.ItemsSource = data.Abonents.ToList();


        }
        private void Show_Legal_method(object sender, RoutedEventArgs e)
        {
            this.LegalFind.Visibility = Visibility.Visible;
            this.LegalSearch.Visibility = Visibility.Visible;
            this.LabelLegal.Visibility = Visibility.Visible;
            this.LabelNatural.Visibility = Visibility.Hidden;

            this.LabelCity.Visibility = Visibility.Visible;
            this.CityBox.Visibility = Visibility.Hidden;
            this.CityButton.Visibility = Visibility.Hidden;
            this.OrganizationBox.Visibility = Visibility.Visible;
            this.OrganizationButton.Visibility = Visibility.Visible;

            this.RadioCity.Visibility = Visibility.Hidden;
            this.RadioVillage.Visibility = Visibility.Hidden;
            this.RadioCity_forOrganization.Visibility = Visibility.Visible;
            this.RadioVillage_forOrganization.Visibility = Visibility.Visible;

            database.ItemsSource = data.Organizations.ToList();

        }

        private void NaturalSearch_Click(object sender, RoutedEventArgs e)
        {


            string connect = "data source=DESKTOP-HNJ3VPT;initial catalog=Городской Телефонный Справочник;integrated security=True;";
            SqlConnection mycon = new SqlConnection(connect);
            try
            {
                mycon.Open();
                string cmd = $"SELECT * FROM Abonents WHERE Telephone LIKE '{NaturalFind.Text}%'"; // Из какой таблицы нужен вывод 
                SqlCommand createCommand = new SqlCommand(cmd, mycon);


                SqlDataAdapter dataAdp = new SqlDataAdapter(createCommand);
                DataTable dt = new DataTable("Abonents"); // В скобках указываем название таблицы
                dataAdp.Fill(dt);
                database.ItemsSource = dt.DefaultView; // Сам вывод 
                mycon.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка подключения");
            }



        }

        private void LegalSearch_Click(object sender, RoutedEventArgs e)
        {
            string connect = "data source=DESKTOP-HNJ3VPT;initial catalog=Городской Телефонный Справочник;integrated security=True;";
            SqlConnection mycon = new SqlConnection(connect);
            try
            {
                mycon.Open();
                string cmd = $"SELECT * FROM Organization WHERE Telephone LIKE '{LegalFind.Text}%'"; // Из какой таблицы нужен вывод 
                SqlCommand createCommand = new SqlCommand(cmd, mycon);


                SqlDataAdapter dataAdp = new SqlDataAdapter(createCommand);
                DataTable dt = new DataTable("Organization"); // В скобках указываем название таблицы
                dataAdp.Fill(dt);
                database.ItemsSource = dt.DefaultView; // Сам вывод 
                mycon.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка подключения");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) // поиск по городам
        {
            if ((bool)RadioCity.IsChecked)
            {
                

                string connect = "data source=DESKTOP-HNJ3VPT;initial catalog=Городской Телефонный Справочник;integrated security=True;";
                SqlConnection mycon = new SqlConnection(connect);
                try
                {
                    mycon.Open();
                    string cmd = $"SELECT Abonents.Surname, Abonents.Name, Abonents.Telephone, City.CityName AS CIC" +
                        $"         FROM Abonents JOIN Address" +
                        $"         ON Abonents.AddIndex = Address.AddIndex" +
                        $"         JOIN City" +
                        $"         ON Address.CityIndex = City.CityIndex" +
                        $"         WHERE City.CityIndex = '{CityBox.Text}'"; // Из какой таблицы нужен вывод 
                    SqlCommand createCommand = new SqlCommand(cmd, mycon);


                    SqlDataAdapter dataAdp = new SqlDataAdapter(createCommand);
                    DataTable dt = new DataTable("Abonents"); // В скобках указываем название таблицы
                    dataAdp.Fill(dt);
                    database.ItemsSource = dt.DefaultView; // Сам вывод 
                    mycon.Close();
                }
                catch
                {
                    MessageBox.Show("Ошибка подключения");
                }
            }
            if ((bool)RadioVillage.IsChecked)
            {
                string connect = "data source=DESKTOP-HNJ3VPT;initial catalog=Городской Телефонный Справочник;integrated security=True;";
                SqlConnection mycon = new SqlConnection(connect);
                try
                {
                    mycon.Open();
                    string cmd = $"SELECT Abonents.Surname, Abonents.Name, Abonents.Telephone, Village.VillageName AS VIV" +
                        $"         FROM Abonents JOIN Address" +
                        $"         ON Abonents.AddIndex = Address.AddIndex" +
                        $"         JOIN Village" +
                        $"         ON Address.VillageIndex = Village.VillageIndex" +
                        $"         WHERE Village.VillageIndex = '{CityBox.Text}'"; // Из какой таблицы нужен вывод 
                    SqlCommand createCommand = new SqlCommand(cmd, mycon);


                    SqlDataAdapter dataAdp = new SqlDataAdapter(createCommand);
                    DataTable dt = new DataTable("Abonents"); // В скобках указываем название таблицы
                    dataAdp.Fill(dt);
                    database.ItemsSource = dt.DefaultView; // Сам вывод 
                    mycon.Close();
                }
                catch
                {
                    MessageBox.Show("Ошибка подключения");
                }
            }
            
        }

        private void Button_Click2(object sender, RoutedEventArgs e) // поиск организаций по городам
        {
            if ((bool)RadioCity_forOrganization.IsChecked)
            {


                string connect = "data source=DESKTOP-HNJ3VPT;initial catalog=Городской Телефонный Справочник;integrated security=True;";
                SqlConnection mycon = new SqlConnection(connect);
                try
                {
                    mycon.Open();
                    string cmd = $"SELECT Organization.OrganizationName, Organization.Telephone, City.CityName AS CIC" + //!!!Сменить абонентов на организации!!!
                        $"         FROM Organization JOIN Address" +
                        $"         ON Organization.Address = Address.AddIndex" +
                        $"         JOIN City" +
                        $"         ON Address.CityIndex = City.CityIndex" +
                        $"         WHERE City.CityIndex = '{OrganizationBox.Text}'"; // Из какой таблицы нужен вывод 
                    SqlCommand createCommand = new SqlCommand(cmd, mycon);


                    SqlDataAdapter dataAdp = new SqlDataAdapter(createCommand);
                    DataTable dt = new DataTable("Organization"); // В скобках указываем название таблицы
                    dataAdp.Fill(dt);
                    database.ItemsSource = dt.DefaultView; // Сам вывод 
                    mycon.Close();
                }
                catch
                {
                    MessageBox.Show("Ошибка подключения");
                }
            }
            if ((bool)RadioVillage_forOrganization.IsChecked)
            {
                string connect = "data source=DESKTOP-HNJ3VPT;initial catalog=Городской Телефонный Справочник;integrated security=True;";
                SqlConnection mycon = new SqlConnection(connect);
                try
                {
                    mycon.Open();
                    string cmd = $"SELECT Organization.OrganizationName, Organization.Telephone, Village.VillageName AS VIV" +//!!!Сменить абонентов на организации!!!
                        $"         FROM Organization JOIN Address" +
                        $"         ON Organization.Address = Address.AddIndex" +
                        $"         JOIN Village" +
                        $"         ON Address.VillageIndex = Village.VillageIndex" +
                        $"         WHERE Village.VillageIndex = '{OrganizationBox.Text}'"; // Из какой таблицы нужен вывод 
                    SqlCommand createCommand = new SqlCommand(cmd, mycon);


                    SqlDataAdapter dataAdp = new SqlDataAdapter(createCommand);
                    DataTable dt = new DataTable("Organization"); // В скобках указываем название таблицы
                    dataAdp.Fill(dt);
                    database.ItemsSource = dt.DefaultView; // Сам вывод 
                    mycon.Close();
                }
                catch
                {
                    MessageBox.Show("Ошибка подключения");
                }
            }
        }

       
    }

    
}
