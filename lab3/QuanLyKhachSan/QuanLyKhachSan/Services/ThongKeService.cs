using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyKhachSan.Data;

namespace QuanLyKhachSan.Services
{
    public class ThongKeService
    {
        // Thống kê doanh thu theo từng ngày trong khoảng thời gian
        public DataTable ThongKeDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            string sql = @"
                SELECT 
                    CAST(NgayLap AS DATE) AS Ngay,
                    COUNT(SoHoaDon) AS SoLuongHoaDon,
                    SUM(TienPhong) AS TongTienPhong,
                    SUM(TienDichVu) AS TongTienDichVu,
                    SUM(TienPhong + TienDichVu) AS TongDoanhThu
                FROM HoaDon
                WHERE TrangThai = N'Đã thanh toán' 
                  AND CAST(NgayLap AS DATE) >= @tu AND CAST(NgayLap AS DATE) <= @den
                GROUP BY CAST(NgayLap AS DATE)
                ORDER BY CAST(NgayLap AS DATE) DESC";
            return Db.Query(sql, new SqlParameter("@tu", tuNgay.Date), new SqlParameter("@den", denNgay.Date));
        }

        // Thống kê số lần được thuê của từng phòng
        public DataTable ThongKeTanSuatPhong(DateTime tuNgay, DateTime denNgay)
        {
            string sql = @"
                SELECT 
                    c.SoPhong,
                    k.TenKhuVuc,
                    COUNT(c.SoPhieuDat) AS SoLanThue
                FROM ChiTietDatPhong c
                JOIN PhieuDatPhong pd ON c.SoPhieuDat = pd.SoPhieuDat
                JOIN Phong p ON c.SoPhong = p.SoPhong
                JOIN KhuVuc k ON p.MaKhuVuc = k.MaKhuVuc
                WHERE pd.NgayLap >= @tu AND pd.NgayLap <= @den
                GROUP BY c.SoPhong, k.TenKhuVuc
                ORDER BY SoLanThue DESC";
            return Db.Query(sql, new SqlParameter("@tu", tuNgay.Date), new SqlParameter("@den", denNgay.Date));
        }
    }
}