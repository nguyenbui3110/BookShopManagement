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

namespace BookShopManagement.UserControls
{
    public partial class UC_Home : UserControl
    {
        public UC_Home()
        {
            InitializeComponent();
            setLabel();
           
        }
        void setLabel()
        {
            foreach (DataRow i in Login_DAL.Instance.Get_Infor_Staff().Rows)
            {
                label10.Text = Convert.ToString(i["ID_Staff"]);
                label9.Text = Convert.ToString(i["Name_Staff"]);
                if (Convert.ToBoolean(i["Gender"])) label11.Text = "Male"; else label11.Text = "Female";
                label12.Text = Convert.ToString(i["DateOfBirth"]);
                label13.Text = Convert.ToString(Convert.ToString(i["Address"]));
                label14.Text = Convert.ToString(Convert.ToString(i["PhoneNumber"]));
                label16.Text = Convert.ToString(Convert.ToString(i["Email"]));
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangePass f = new ChangePass();
            f.Show();
            
        }

    }
}
