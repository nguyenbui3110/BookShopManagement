using BookShopManagement.BLL_ST;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShopManagement.UserControls
{
    public partial class UC_ImporBook : UserControl
    {
        public UC_ImporBook()
        {
            InitializeComponent();
            SetCBB();
            Da = new List<Diary>();
        }
        private static List<Diary> Da { get; set; }
        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        void SetCBB()
        {
            foreach (DataRow i in CSDL_USER.Instance.Books.Rows)
            { 
                comboBox2.Items.Add(new CBBItems()
                {
                    Text = Convert.ToString(i["TenSach"]),
                    value = Convert.ToInt32(i["MaSach"])
                });
            }
            comboBox2.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Diary add = new Diary();
            int stt=1;
            add.MaSach = ((CBBItems)comboBox2.SelectedItem).value;
            add.SoLuong = Convert.ToInt32(textBox2.Text);
            add.NgayNhap = Convert.ToDateTime(dateTimePicker2.Value);
            add.ID_Staff = Convert.ToInt32(Login_DAL.Instance.ID_USER);
            add.STT = stt++;
            Da.Add(add);
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = Da;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("xac nhan nhap?");
            foreach(Diary i in Da)
            {
                Login_DAL.Instance.adddiary(i.MaSach, i.SoLuong, i.NgayNhap, i.ID_Staff);
                Login_DAL.Instance.updatesoluong(i.SoLuong, i.MaSach);
            }
            Da.Clear();
            textBox2.Text = "";
        }
    }
}
