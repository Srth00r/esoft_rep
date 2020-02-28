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
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source = 303-3\\SQLEXPRESS; Initial Catalog = esoft_zhk; Integrated Security = true;");
        
        public Form1()
        {
            InitializeComponent();
        }
        
        private void LoadData()
        {
            con.Open();
            string query = @"select distinct [Название_ЖК], [Статус_строительства_ЖК], (select COUNT([Название_ЖК]) from [dbo].[houses_in_complexes] as h2 where h1.Название_ЖК = h2.Название_ЖК) as [Кол-во домов], [Город]
from[dbo].[houses_in_complexes] as h1";
            SqlCommand command = new SqlCommand(query, con);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                dataGridView1.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString());
            reader.Close();
            con.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            button3.Visible = true;
            button4.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand delete = new SqlCommand($"DELETE FROM dbo.houses_in_complexes WHERE [ИД_ЖК] = '{dataGridView1.CurrentRow.Index + 1}'", con);
            delete.ExecuteNonQuery();
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 fm = new Form2();
            fm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            button4.Visible = false;
            dataGridView1.EndEdit();
            con.Open();
            SqlCommand command = new SqlCommand($"UPDATE dbo.houses_in_complexes SET [Название_ЖК] = '{dataGridView1.CurrentRow.Cells[0].Value.ToString()}', [Статус_строительства_ЖК] = '{dataGridView1.CurrentRow.Cells[1].Value.ToString()}', [Город] = '{dataGridView1.CurrentRow.Cells[3].Value.ToString()}' WHERE [ИД_ЖК] = '{dataGridView1.CurrentRow.Index + 1}'", con);
            command.ExecuteNonQuery();
            con.Close();
        }
    }
}
