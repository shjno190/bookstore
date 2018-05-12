using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bookstore.Models;

namespace bookstore.Controllers
{
    public class GiohangController : Controller
    {
        //tạo đói tượng data chứa cơ sở dữ liệu từ dbBANSACH đã tạo
        dbQLBansachDataContext data = new dbQLBansachDataContext();
        // GET: Giohang
        public List<Giohang> Laygiohang()
        {
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang==null)
                {
                //Nếu giỏ hàng chưa tồn tại thì khởi tạo listGiohang
                lstGiohang = new List<Giohang>();
                Session["Giohang"]=lstGiohang;
            }
            return lstGiohang;
        }
       //thêm vào giỏ hàng
       public ActionResult ThemGiohang(int iMasach,string strURL)
        {
            //lấy sesion giỏ sách
            List<Giohang> lstGiohang = Laygiohang();
            //kiểm tra
            Giohang sanpham=lstGiohang.Find(n => n.iMasach == iMasach);
            if(sanpham==null)
            {
                sanpham = new Giohang(iMasach);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }
        }
        //Tổng số lượng
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List < Giohang > lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
                {
                iTongSoLuong = lstGiohang.Sum(n => n.iSoluong);
            }
            return iTongSoLuong;
        }
        //Tổng tiền
        private double TongTien ()
        {
            double iTongtien = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if (lstGiohang!=null)
            {
                iTongtien=lstGiohang.Sum(n => n.dThanhtien);
            }
            return iTongtien;
        }
        // xây dựng giỏ hàng
        public ActionResult GioHang()
        {
            List<Giohang>lstGiohang = Laygiohang();
            if(lstGiohang.Count==0)
                {
                return RedirectToAction("Index", "Bookstore");  
                }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGiohang);
        }
        //Tạo partial để hiện thị thông tin giỏ hàng
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        //xóa giỏ hàng
        public ActionResult XoaGiohang(int iMaSP)
        {
            //lấy giỏ hàng từ sêsion
            List<Giohang> lstGiohang = Laygiohang();
            //Kiếm tra sách đã có trong seession chưa
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iMasach == iMaSP);
            //Nếu tồn tjai rồi thì cho sửa số lượng
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.iMasach == iMaSP);
                return RedirectToAction("Giohang");
            }
            if (lstGiohang.Count==0)
            {
                return RedirectToAction("Index", "Bookstore");
            }
            return RedirectToAction("GioHang");
        }
        //cập nhật giỏ hàng
        public ActionResult CapnhatGiohang(int iMaSP, FormCollection f)
        {
            //lấy giỏ hàng từ sêsion
            List<Giohang> lstGiohang = Laygiohang();
            //Kiếm tra sách đã có trong seession chưa
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iMasach == iMaSP);
            //Nếu tồn tjai rồi thì cho sửa số lượng
            if (sanpham != null)
            {
                sanpham.iSoluong = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("Giohang");
        }
        //xóa tát cả giở hàng
        public ActionResult XoatatcaGiohang()
        {
            //lấy giỏ hàng từ sêsion
            List<Giohang> lstGiohang = Laygiohang();
           lstGiohang.Clear();
            return RedirectToAction("Index", "Bookstore");

        }
    }
}