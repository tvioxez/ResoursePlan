using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace РаспределениеРесурсов
{
    public partial class Авт : Form
    {
        database db = new database();
        public Авт()
        {
            InitializeComponent();
        }


        private void logins()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable Table = new DataTable();

            string query = $"select * from Admin where Login='{db.login}' and Pass='{db.pass}'";

            SqlCommand command = new SqlCommand(query, db.con);

            adapter.SelectCommand = command;

            adapter.Fill(Table);

            if (Table.Rows.Count == 1)
            {

                MessageBox.Show("Вы успешно вошли!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();

                Добавление с = new Добавление();
                с.ShowDialog();

                db.con.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Close();
                db.con.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин/пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db.login = textBox1.Text;
            db.pass = textBox2.Text;

            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Пожалуйста, введите логин/пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
            logins();
            }
        }
    }
}
