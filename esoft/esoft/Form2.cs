using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Common;

namespace esoft
{
    public partial class Form2 : Form
    {
        SqlConnection con = new SqlConnection("Data Source = DESKTOP-K6659U1\\SQL_LAPTOP; Initial Catalog = esoft_zhk; Integrated Security = true;");
        DataTable DataTable = new DataTable();
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            var source = new AutoCompleteStringCollection();
            source.AddRange(new string[]
            {
                "Москва","Санкт-Петербург","Уфа","Казань","Новосибирск"
            });
            textBox2.AutoCompleteCustomSource = source;
            comboBox1.SelectedIndex =  0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand command = new SqlCommand($"INSERT INTO dbo.houses_in_complexes([Название_ЖК],[Город],[Статус_строительства_ЖК],[Добавочная_стоимость_ЖК],[Затраты_на_строительство_ЖК]) VALUES('{textBox1.Text}','{textBox2.Text}','{comboBox1.Text}','{textBox3.Text}','{textBox4.Text}')", con);
            command.ExecuteNonQuery();
            con.Close();
        }
    }
}
