using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// Подключение библиотек DB
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace sql_windows_learning
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null; // Создаём подключение класса sql
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) // Обработчик события
        {
            // Подключим новый SQL, передадим путь к БД через созданный нами словарь в app.config (настройках)
            // Мы создавали словрь  ConnectionStrings, по ключу [TestDB] берём наш путь к БД. Этот путь передаётся в новую SQL
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString);

            // Откроём подключение к баззе данных
            sqlConnection.Open();

            // Проверим состояние нашей SQL. Если она открыта - то выведется информационное окно
            if (sqlConnection.State == ConnectionState.Open)
                MessageBox.Show("Подключение установлено");


        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SqlCommand command = new SqlCommand(
            //    $"INSERT INTO [Students] (Name, Surname, Birthday) VALUES (N'{textBox1.Text}', N'{textBox2.Text}', '{textBox3.Text}')",
            //    sqlConnection);
            //MessageBox.Show(command.ExecuteNonQuery().ToString());
            SqlCommand command = new SqlCommand(
                "INSERT INTO [Students] (Name, Surname, Birthday, HomePlace, Phone, Email) VALUES (@Name, @Surname, @Birthday, @HomePlace, @Phone, @Email)",
                sqlConnection);

            DateTime date = DateTime.Parse(textBox3.Text);

            command.Parameters.AddWithValue("Name", textBox1.Text);
            command.Parameters.AddWithValue("Surname", textBox2.Text);
            command.Parameters.AddWithValue("Birthday", $"{date.Month}/{date.Day}/{date.Year}");
            command.Parameters.AddWithValue("HomePlace", textBox4.Text);
            command.Parameters.AddWithValue("Phone", textBox5.Text);
            command.Parameters.AddWithValue("Email", textBox6.Text);

            MessageBox.Show(command.ExecuteNonQuery().ToString());
        }
    }
}
