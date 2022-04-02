using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BookShopManagement.Forms;
using BookShopManagement.BLL_ST;

namespace BookShopManagement.UserControls
{
    public partial class UC_ManageBook : UserControl
    {
        public UC_ManageBook()
        {
            InitializeComponent();
          dataGridView2.DataSource = CSDL_OOP.Instance.GetBookstores("");
            setBookOut();
            setCBB();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string name = "";
            name = textBox1.Text;
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = CSDL_OOP.Instance.GetBookstores(name);
        }
        private void setCBB()
        {
            int count = 0;
            foreach (DataColumn i in CSDL_USER.Instance.Books.Columns)
            {
                comboBox1.Items.Add(new CBBItems()
                {
                    Text = i.ColumnName,
                    value = count
                });
                count++;
            }
            comboBox1.SelectedIndex = -1;
        }
        void setBookOut()
        {
            List<Bookstore> dt = new List<Bookstore>();
            List<Bookstore> DT = new List<Bookstore>();
            dt = CSDL_OOP.Instance.GetBookstores("");
            DT = dt.OrderBy(o => o.SoLuongCon).ToList();
            List<Bookstore> _dt = new List<Bookstore>();
            for(int i = 0; i< 4; i++)
            {
                _dt.Add(DT[i]);
            }
            dataGridView1.DataSource = _dt;
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
            if(comboBox1.SelectedIndex != -1)
            {
                
                List<Bookstore> dt = new List<Bookstore>();
                if (textBox1.Text == "")
                {
                    dt = CSDL_OOP.Instance.GetBookstores("");
                }
                else dt = CSDL_OOP.Instance.GetBookstores(textBox1.Text);
                string sortBy = comboBox1.SelectedItem.ToString();
                switch (sortBy)
                {
                    case "MaSach":
                        dataGridView2.DataSource = dt.OrderBy(o => o.MaSach).ToList();
                        break;
                    case "TenSach":
                        dataGridView2.DataSource = dt.OrderBy(o => o.TenSach).ToList();
                        break;
                    case "TongSoLuong":
                        dataGridView2.DataSource = dt.OrderBy(o => o.TongSoLuong).ToList();
                        break;
                    case "SoLuongCon":
                        dataGridView2.DataSource = dt.OrderBy(o => o.SoLuongCon).ToList();
                        break;
                }
               /* dataGridView2.Columns[0].HeaderText = "Book ID";
                dataGridView2.Columns[1].HeaderText = "Book Title";
                dataGridView2.Columns[2].HeaderText = "Cost Price";
                dataGridView2.Columns[3].HeaderText = "So luong con";*/
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
