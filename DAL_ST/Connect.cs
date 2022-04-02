using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace BookShopManagement
{
    class Connect
    {
        string constr; 
        public Connect()
        {
            string setting = ConfigurationManager.ConnectionStrings["BookShopManagement.Properties.Settings.PBL3ConnectionString"].ConnectionString;
            

            System.Windows.Forms.MessageBox.Show(setting);
            
            constr= setting;
        }
        public SqlConnection getConnect()
        {
            return new SqlConnection(constr);
        }
    }
}