using BookShopManagement.BLL_ST;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShopManagement
{
   public class Login_DAL
    {
        Connect dc;
        SqlDataAdapter da,da_;
        private  DataTable dt = new DataTable();
        private static Login_DAL _Instance;
        public string ID_Pos = "";
        public string Pass = "";
        public string Acc = "";
        public string ID_USER;
        SqlCommand cmd;
        public static Login_DAL Instance
        {
            get
            {
                if (_Instance == null) _Instance = new Login_DAL();
                 return _Instance;
            }
            private set
            {

            }   
        }
        public Login_DAL()
        {
            dc = new Connect();
        }
        public bool ChangePass(string _Pass)
        {
            string SQL = "UPDATE Account SET Password = @Pass WHERE UserName= '"+Acc+"'";
            SqlConnection Con = dc.getConnect();
            try
            {
                cmd = new SqlCommand(SQL, Con);
                Con.Open();
                cmd.Parameters.Add("@Pass", SqlDbType.VarChar).Value = CSDL_USER.EncodePass(_Pass);
                cmd.ExecuteNonQuery();
                Con.Close();
            }
            catch (Exception e)
            {
                return false;
            };
            return true;
        }
        public bool adddiary(int masach, int soluong, DateTime ngaynhap, int ID_User)
        {
            string SQL = "INSERT INTO NhatKiNhapSach VALUES(@masach,@soluong,@ngaynhap,@ID_USER)";
            SqlConnection Con = dc.getConnect();
            try
            {
                cmd = new SqlCommand(SQL, Con);
                Con.Open();
                cmd.Parameters.Add("@Masach", SqlDbType.Int).Value = masach;
                cmd.Parameters.Add("@soluong", SqlDbType.Int).Value = soluong;
                cmd.Parameters.Add("@ngaynhap", SqlDbType.DateTime).Value = ngaynhap;
                cmd.Parameters.Add("@ID_USER", SqlDbType.Int).Value = ID_User;
                cmd.ExecuteNonQuery();
                Con.Close();
            }
            catch (Exception e)
            {
                return false;
            };
            return true;
        }
        public DataTable Total(int masach)
        {
            string SQL = "SELECT TongSoLuong FROM Kho WHERE MaSach = " +masach ;
            SqlConnection Con = dc.getConnect();
            da = new SqlDataAdapter(SQL, Con);
            Con.Open();
            DataTable d = new DataTable();
            da.Fill(d);
            Con.Close();
            return d;
        }
        public DataTable SoLuongCon(int masach)
        {
            string SQL = "SELECT SoLuongCon FROM Kho WHERE MaSach = " + masach;
            SqlConnection Con = dc.getConnect();
            da = new SqlDataAdapter(SQL, Con);
            Con.Open();
            DataTable d = new DataTable();
            da.Fill(d);
            Con.Close();
            return d;
        }
        public bool updatesoluongcon(int total , int masach)
        {
            string SQL = "UPDATE Kho SET SoLuongCon = @soluong WHERE MaSach = @masach ";
            SqlConnection Con = dc.getConnect();
            int sachcon = 0;
            foreach (DataRow i in SoLuongCon(masach).Rows)
            {
                sachcon = Convert.ToInt32(i["SoLuongCon"]);
            }
            try
            {
                cmd = new SqlCommand(SQL, Con);
                Con.Open();
                cmd.Parameters.Add("@soluong", SqlDbType.Int).Value = sachcon + total;
                cmd.Parameters.Add("@Masach", SqlDbType.Int).Value = masach;
                cmd.ExecuteNonQuery();
                Con.Close();
            }
            catch (Exception e)
            {
                return false;
            };
            return true;
        }
        public bool updatesoluong(int soluong , int masach)
        {
            string SQL = "UPDATE Kho SET Tongsoluong = @soluong WHERE MaSach= @masach";
            int total = 0; 
            foreach( DataRow i in Total(masach).Rows)
            {
                total = Convert.ToInt32(i["TongSoLuong"]);
            }
            MessageBox.Show(""+total);
            SqlConnection Con = dc.getConnect();
            try
            {
                cmd = new SqlCommand(SQL, Con);
                Con.Open();
                cmd.Parameters.Add("@soluong", SqlDbType.Int).Value = soluong+total;
                cmd.Parameters.Add("@Masach", SqlDbType.Int).Value = masach ;
                cmd.ExecuteNonQuery();
                Con.Close();
            }
            catch (Exception e)
            {
                return false;
            };
            updatesoluongcon(soluong, masach);
            return true;
        }

        public DataTable Staff_Infor(string Account  , string Password )
        {
            
            Pass = CSDL_USER.EncodePass(Password);//CSDL_USER.Instance.EncodePass(Password);
            Acc = Account;
            string SQL = "SELECT ID_User,ID_Position FROM Account WHERE UserName='" + Account + "'AND Password = '" + CSDL_USER.EncodePass(Password) + "'";
            //MessageBox.Show(CSDL_USER.MD5Hash(CSDL_USER.Base64Encode("hatien123"))); 
            //MessageBox.Show(CSDL_USER.EncodePass("hatien123"));
            SqlConnection Con = dc.getConnect();
            da = new SqlDataAdapter(SQL, Con);
            Con.Open();
            dt = new DataTable();
            da.Fill(dt);
            Con.Close();
            return dt;
        }
        public DataTable Get_Infor_Staff()
        {
            ID_USER = "";
            foreach (DataRow i in dt.Rows)
            {
                ID_USER = Convert.ToString(i["ID_User"]);
                ID_Pos = Convert.ToString(i["ID_Position"]);
            }
            string Staff_In4 ="SELECT * FROM Staff WHERE ID_User = " + ID_USER;
            SqlConnection Con = dc.getConnect();
            da_ = new SqlDataAdapter(Staff_In4, Con);
            Con.Open();
            DataTable TB_Staff = new DataTable();
            da_.Fill(TB_Staff);
            if(TB_Staff.Rows.Count == 0) MessageBox.Show("Invalid Login please check" + ID_USER + " username and password");
            Con.Close();
            return TB_Staff;
        }
        public DataTable Get_BookStore()
        {
            String SQL = "SELECT Kho.MaSach , TenSach , TongSoLuong,SoLuongCon from dbo.Kho,dbo.Sach where Sach.Masach = Kho.MaSach";
            SqlConnection Con = dc.getConnect();
            da_ = new SqlDataAdapter(SQL, Con);
            Con.Open();
            DataTable TB_Staff = new DataTable();
            da_.Fill(TB_Staff); 
            Con.Close();
            return TB_Staff;
        }
        public DataTable Get_Diary()
        {
            string SQL = "SELECT *from NhatKiNhapSach";
            SqlConnection Con = dc.getConnect();
            da_ = new SqlDataAdapter(SQL, Con);
            DataTable TB_Staff = new DataTable();
            da_.Fill(TB_Staff);
            Con.Close();
            return TB_Staff;
        }
        public void clear()
        {
            CSDL_USER.Instance.Staff_Infor.Rows.Clear();
        }
    }
}
