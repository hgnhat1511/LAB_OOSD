using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyKhachSan.Data;

namespace QuanLyKhachSan.Services
{
    public class TraPhongService
    {
        // Lấy danh sách phòng đang có khách
        public DataTable LayPhongDangO()
        {
            return Db.Query("SELECT c.SoPhieuDat, c.SoPhong, k.HoTen, p.NgayNhanThucTe, p.TienCoc FROM ChiTietDatPhong c JOIN PhieuDatPhong p ON c.SoPhieuDat = p.SoPhieuDat JOIN KhachHang k ON p.MaKhach = k.MaKhach WHERE p.TrangThai = N'Đang ở'");
        }

        // Lấy chi tiết dịch vụ đã sử dụng để tính tiền
        public DataTable LayDichVuDaDung(string soPhieuDat, string soPhong)
        {
            return Db.Query("SELECT d.TenDV, c.SoLuong, c.DonGia, c.ThanhTien FROM PhieuSuDungDV p JOIN ChiTietPhieuSuDungDV c ON p.SoPhieuSDDV = c.SoPhieuSDDV JOIN DichVu d ON c.MaDV = d.MaDV WHERE p.SoPhieuDat = @s AND p.SoPhong = @p", new SqlParameter("@s", soPhieuDat), new SqlParameter("@p", soPhong));
        }

        // Tính tiền phòng dựa trên số ngày thực tế
        public decimal TinhTienPhong(string soPhieuDat, string soPhong, out int soNgay)
        {
            soNgay = 0;
            var dt = Db.Query("SELECT p.NgayNhanThucTe, ph.DonGiaNgay FROM PhieuDatPhong p JOIN ChiTietDatPhong c ON p.SoPhieuDat = c.SoPhieuDat JOIN Phong ph ON c.SoPhong = ph.SoPhong WHERE p.SoPhieuDat = @s AND c.SoPhong = @p", new SqlParameter("@s", soPhieuDat), new SqlParameter("@p", soPhong));
            if (dt.Rows.Count > 0)
            {
                DateTime ngayNhan = Convert.ToDateTime(dt.Rows[0]["NgayNhanThucTe"]);
                decimal donGia = Convert.ToDecimal(dt.Rows[0]["DonGiaNgay"]);

                // Tính số ngày (nếu trả trong ngày thì tính 1 ngày)
                TimeSpan ts = DateTime.Now.Date - ngayNhan.Date;
                soNgay = ts.Days > 0 ? ts.Days : 1;
                return soNgay * donGia;
            }
            return 0;
        }

        // Thực hiện thanh toán: Tạo hóa đơn, đổi trạng thái phiếu và trạng thái phòng
        public KetQuaXuLy ThanhToan(string soHoaDon, string soPhieuDat, string maNV, int soNgay, decimal tienPhong, decimal tienDV, decimal tienCoc)
        {
            if (string.IsNullOrWhiteSpace(soHoaDon)) return KetQuaXuLy.Fail("Vui lòng nhập số hóa đơn.");
            using (var cn = Db.OpenConnection())
            using (var tx = cn.BeginTransaction())
            {
                try
                {
                    // 1. Tạo hóa đơn
                    var cmdHD = new SqlCommand("INSERT INTO HoaDon(SoHoaDon, SoPhieuDat, NgayLap, MaNV, SoNgayTinhTien, TienPhong, TienDichVu, TrangThai) VALUES(@shd, @spd, @n, @nv, @sn, @tp, @tdv, N'Đã thanh toán')", cn, tx);
                    cmdHD.Parameters.AddWithValue("@shd", soHoaDon);
                    cmdHD.Parameters.AddWithValue("@spd", soPhieuDat);
                    cmdHD.Parameters.AddWithValue("@n", DateTime.Now);
                    cmdHD.Parameters.AddWithValue("@nv", maNV);
                    cmdHD.Parameters.AddWithValue("@sn", soNgay);
                    cmdHD.Parameters.AddWithValue("@tp", tienPhong);
                    cmdHD.Parameters.AddWithValue("@tdv", tienDV);
                    cmdHD.ExecuteNonQuery();

                    // 2. Cập nhật phiếu đặt -> Đã trả
                    var cmdPD = new SqlCommand("UPDATE PhieuDatPhong SET TrangThai = N'Đã trả', NgayTraThucTe = @n WHERE SoPhieuDat = @spd", cn, tx);
                    cmdPD.Parameters.AddWithValue("@n", DateTime.Now);
                    cmdPD.Parameters.AddWithValue("@spd", soPhieuDat);
                    cmdPD.ExecuteNonQuery();

                    // 3. Cập nhật phòng -> Trống
                    var cmdPhong = new SqlCommand("UPDATE Phong SET TrangThai = N'Trống' WHERE SoPhong IN (SELECT SoPhong FROM ChiTietDatPhong WHERE SoPhieuDat = @spd)", cn, tx);
                    cmdPhong.Parameters.AddWithValue("@spd", soPhieuDat);
                    cmdPhong.ExecuteNonQuery();

                    tx.Commit();
                    return KetQuaXuLy.Ok("Thanh toán và trả phòng thành công!");
                }
                catch (Exception ex)
                {
                    try { tx.Rollback(); } catch { }
                    return KetQuaXuLy.Fail("Lỗi: " + ex.Message);
                }
            }
        }
    }
}