using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShopManagement.BLL_ST
{
    class CSDL_USER
    {
        private static CSDL_USER _Instance;
        public static CSDL_USER Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new CSDL_USER();
                return _Instance;
            }
            private set
            {

            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string MD5Hash(string input)
        {

            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();

            ////Tạo MD5 
            //MD5 mh = MD5.Create();
            ////Chuyển kiểu chuổi thành kiểu byte
            //byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes("Chuỗi cần mã hóa");
            ////mã hóa chuỗi đã chuyển
            //byte[] hash = mh.ComputeHash(inputBytes);
            ////tạo đối tượng StringBuilder (làm việc với kiểu dữ liệu lớn)
            //StringBuilder sb = new StringBuilder();

            //for (int i = 0; i < hash.Length; i++)
            //{
            //    sb.Append(hash[i].ToString("X2"));
            //}
            //return sb.ToString();
            //nếu bạn muốn các chữ cái in thường thay vì in hoa thì bạn thay chữ "X" in hoa 
            //trong "X2" thành "x"
        }


        public static string EncodePass(string pass)
        {
            return MD5Hash(Base64Encode(pass));
        }

        public DataTable Staff_Infor { get; set; }
        public DataTable Books { get; set; }
        public DataTable Diary { get; set; }
        private void GetDiary(DataRow DT)
        {
            DataRow Dr = Diary.NewRow();
            Dr["STT"] = DT["STT"];
            Dr["MaSach"] = DT["MaSach"];
            Dr["SoLuong"] = DT["SoLuong"];
            Dr["NgayNhap"] = DT["NgayNhap"];
            Dr["ID_Staff"] = DT["ID_Staff"];
            Diary.Rows.Add(Dr);
        }
        public void ResetBookDB(List<Bookstore> _Book)
        {
            Books.Rows.Clear();
            foreach(Bookstore i in _Book) {
                DataRow Dr = Books.NewRow();
                Dr["MaSach"] = i.MaSach;
                Dr["TenSach"] = i.TenSach;
                Dr["TongSoLuong"] = i.TongSoLuong;
                Dr["SoLuongCon"] = i.SoLuongCon;
                Books.Rows.Add(Dr);
            }
        }
        private void GetCSDL(DataRow ST )
        {
            DataRow Dr = Staff_Infor.NewRow();
            Dr["ID_Staff"] = ST["ID_Staff"];
            Dr["Name_Staff"] = ST["Name_Staff"];
            Dr["Gender"] = ST["Gender"];
            Dr["DateOfBirth"] = ST["DateOfBirth"];
            Dr["Address"] = ST["Address"];
            Dr["Email"] = ST["Email"];
            Dr["PhoneNumber"] = ST["PhoneNumber"];
            Staff_Infor.Rows.Add(Dr);
        }
        private void GetBooks(DataRow DT)
        {
            DataRow Dr = Books.NewRow();
            Dr["MaSach"] = DT["MaSach"];
            Dr["TenSach"] = DT["TenSach"];
            Dr["TongSoLuong"] = DT["TongSoLuong"];
            Dr["SoLuongCon"] = DT["SoLuongCon"];
            Books.Rows.Add(Dr);

        }
        public void refresh(List<Bookstore> _Book)
        {
            Books.Rows.Clear();
            foreach( Bookstore i in _Book)
            {
                DataRow Dr = Books.NewRow();
                Dr["MaSach"] = Convert.ToString(i.MaSach);
                Dr["TenSach"] = Convert.ToString(i.TenSach);
                Dr["TongSoLuong"] = Convert.ToString(i.TongSoLuong);
                Dr["SoLuongCon"] = Convert.ToString(i.SoLuongCon);
                Books.Rows.Add(Dr);
            }
        }
       public CSDL_USER()
        {
            Staff_Infor = new DataTable();
            Staff_Infor.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID_Staff",typeof(int)),
                new DataColumn("Name_Staff",typeof(string)),
                new DataColumn("Gender",typeof(bool)),
                new DataColumn("DateOfBirth",typeof(DateTime)),
                new DataColumn("Address",typeof(string)),
                new DataColumn("PhoneNumber",typeof(string)),
                new DataColumn("Email",typeof(string))
            });
            foreach (DataRow i in Login_DAL.Instance.Get_Infor_Staff().Rows)
            {
                GetCSDL(i);
            }
            Books = new DataTable();
            Books.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("MaSach",typeof(string)),
                new DataColumn("TenSach",typeof(string)),
                new DataColumn("TongSoLuong",typeof(string)),
                new DataColumn("SoLuongCon",typeof(string))
            });
            foreach(DataRow i in Login_DAL.Instance.Get_BookStore().Rows)
            {
                GetBooks(i);
            }
            Diary = new DataTable();
            Diary.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("STT",typeof(int)),
                new DataColumn("MaSach",typeof(int)),
                new DataColumn("SoLuong",typeof(int)),
                new DataColumn("NgayNhap",typeof(DateTime)),
                new DataColumn("ID_Staff",typeof(int))
            }); 
            foreach(DataRow i in Login_DAL.Instance.Get_Diary().Rows)
            {
                GetDiary(i);
            }
        }

    }   
}
