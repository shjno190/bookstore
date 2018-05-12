using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bookstore.Models;

namespace bookstore.Models
{
    
    public class Giohang
    {
        //Tạo đối tượng dataa chứa dữa liệu từ model dbBANSACH đã tạo
        dbQLBansachDataContext data = new dbQLBansachDataContext();
        public int iMasach { set; get; }
        public string sTensach { set; get; }
        public string sAnhbia { set; get; }
        public Double dDongia { set; get; }
        public int iSoluong { set; get; }
        public Double dThanhtien
        {
            get { return iSoluong * dDongia; }
        }
        //Khởi tạo giò hàng theo Masach được truyền vào mặc định là 1
        public Giohang(int Masach)
        {
            iMasach = Masach;
            SACH sach = data.SACHes.Single(n => n.Masach == iMasach);
            sTensach = sach.Tensach;
            sAnhbia = sach.Hinhminhhoa;
            dDongia = double.Parse(sach.Dongia.ToString());
            iSoluong = 1;
        }
    }
}