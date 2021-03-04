using System;
using System.IO;
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
using AmonicAirlines.DB;
using System.Security.Cryptography;

namespace AmonicAirlines
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(hash);
        }
        public MainWindow()
        {
            //InitializeComponent();
            DataBaseEntities db = new DataBaseEntities();
            StreamReader reader = new StreamReader("C:\\Users\\Mikhail\\Desktop\\DigitalSkills2017-авиакомпания\\Сессия 1\\UserData.csv");
            while (!reader.EndOfStream)
            {
                User user = new User();
                var data = reader.ReadLine().Split(',');
                //user.RoleID = db.Roles.FirstOrDefault(i => i.Title == data[0]).ID;
                user.Email = data[1];
                user.Password = GetHash(data[2]);
                user.FirstName = data[3];
                user.LastName = data[4];
                user.OfficeID = db.Offices.FirstOrDefault(i => i.Title == data[5]).ID;
                db.Users.Add(user);
            }
            db.SaveChangesAsync();
        }
    }
}
