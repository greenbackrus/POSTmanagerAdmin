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
using Microsoft.EntityFrameworkCore;
using POSTmanagerAdmin.Models;

namespace POSTmanagerAdmin.RestViews
{
    /// <summary>
    /// Логика взаимодействия для CreateRest.xaml
    /// </summary>
    public partial class CreateRestWindow : Window
    {
        private PmDbContext _context = new PmDbContext();
        public CreateRestWindow()
        {
            InitializeComponent();
        }

        private void UsersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Create_Button_Click(object sender, RoutedEventArgs e)
        {
            string _name = NameTextBox.Text;
            string _ip = IpTextBox.Text;
            string _port = PortTextBox.Text;
            string _serverName = ServerNameTextBox.Text;

            //TODO: refactor (ValidationContext)
            if (string.IsNullOrEmpty(_name) ||
                string.IsNullOrEmpty(_ip) ||
                string.IsNullOrEmpty(_port) ||
                string.IsNullOrEmpty(_serverName)) 
            {
                ShowWarning("Все поля должны быть заполнены!");
                return;
            }

            Rest _newRest = new Rest { 
                Name = _name,
                Ip = _ip,
                Port = _port,
                ServerName = _serverName,
            };
            _context.Rests.Add(_newRest);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                ShowWarning($"При сохранении ресторана возникла ошибка!" + ex.Message);
                //TODO: log
            }
            //TODO: handling users
        }
        private void CleanWarning()
        {
            WarningLabel.Content = "";
            WarningLabel.Visibility = Visibility.Hidden;
        }

        private void ShowWarning(string text)
        {
            WarningLabel.Content = text;
            WarningLabel.Visibility = Visibility.Visible;
        }

        private void CreateRestForm_Activated(object sender, EventArgs e)
        {
            var _users = _context.Users.Select(u => new { u.Name }).ToList();
            UsersGrid.ItemsSource = _users;
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
