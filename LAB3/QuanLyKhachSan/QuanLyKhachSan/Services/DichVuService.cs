using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyKhachSan.Data;

namespace QuanLyKhachSan.Services
{
    public class DichVuService
    {
        // Chỉ lấy những phòng đang có khách ở (TrangThai = 'Đang ở')
        public DataTable LayPhongDangO()
        {
            return Db.Query("SELECT c.SoPhieuDat, c.SoPhong, k.HoTen FROM ChiTietDatPhong c JOIN PhieuDatPhong p ON c.SoPhieuDat = p.SoPhieuDat JOIN KhachHang k ON p.MaKhach = k.MaKhach WHERE p.TrangThai = N'Đang ở'");
        }

        public DataTable LayDanhSachDichVu()
        {
            return Db.Query("SELECT * FROM DichVu ORDER BY TenDV");
        }

        public DataTable LayPhieuSDDV(string soPhieuDat, string soPhong)
        {
            return Db.Query("SELECT * FROM PhieuSuDungDV WHERE SoPhieuDat=@s AND SoPhong=@p", new SqlParameter("@s", soPhieuDat), new SqlParameter("@p", soPhong));
        }

        public DataTable LayChiTietSDDV(string soPhieuSDDV)
        {
            return Db.Query("SELECT c.*, d.TenDV, d.DonViTinh FROM ChiTietPhieuSuDungDV c JOIN DichVu d ON c.MaDV = d.MaDV WHERE c.SoPhieuSDDV=@s", new SqlParameter("@s", soPhieuSDDV));
        }

        public KetQuaXuLy ThemDichVu(string soPhieuSDDV, string soPhieuDat, string soPhong, DateTime ngay, string maNV, string maDV, int soLuong, decimal donGia)
        {
            if (string.IsNullOrWhiteSpace(soPhieuSDDV) || string.IsNullOrWhiteSpace(soPhieuDat) || string.IsNullOrWhiteSpace(soPhong) || string.IsNullOrWhiteSpace(maDV) || soLuong <= 0)
                return KetQuaXuLy.Fail("Thông tin sử dụng dịch vụ không hợp lệ (kiểm tra lại mã, số lượng).");

            using (var cn = Db.OpenConnection())
            using (var tx = cn.BeginTransaction())
            {
                try
                {
                    // Kiểm tra xem phòng này trong ngày hôm nay đã có phiếu SDDV chưa, chưa có thì tạo mới
                    var cmdCheck = new SqlCommand("SELECT SoPhieuSDDV FROM PhieuSuDungDV WHERE SoPhieuSDDV=@ps", cn, tx);
                    cmdCheck.Parameters.AddWithValue("@ps", soPhieuSDDV);
                    var exists = cmdCheck.ExecuteScalar();

                    if (exists == null)
                    {
                        var cmdInsertPhieu = new SqlCommand("INSERT INTO PhieuSuDungDV(SoPhieuSDDV, SoPhieuDat, SoPhong, NgaySuDung, MaNV) VALUES(@ps, @pd, @sp, @n, @nv)", cn, tx);
                        cmdInsertPhieu.Parameters.AddWithValue("@ps", soPhieuSDDV);
                        cmdInsertPhieu.Parameters.AddWithValue("@pd", soPhieuDat);
                        cmdInsertPhieu.Parameters.AddWithValue("@sp", soPhong);
                        cmdInsertPhieu.Parameters.AddWithValue("@n", ngay.Date);
                        cmdInsertPhieu.Parameters.AddWithValue("@nv", maNV);
                        cmdInsertPhieu.ExecuteNonQuery();
                    }

                    // Thêm chi tiết dịch vụ (nếu trùng dịch vụ thì cộng dồn số lượng)
                    var cmdCheckCT = new SqlCommand("SELECT SoLuong FROM ChiTietPhieuSuDungDV WHERE SoPhieuSDDV=@ps AND MaDV=@mdv", cn, tx);
                    cmdCheckCT.Parameters.AddWithValue("@ps", soPhieuSDDV);
                    cmdCheckCT.Parameters.AddWithValue("@mdv", maDV);
                    var sl = cmdCheckCT.ExecuteScalar();

                    if (sl != null)
                    {
                        var cmdUpdateCT = new SqlCommand("UPDATE ChiTietPhieuSuDungDV SET SoLuong = SoLuong + @sl WHERE SoPhieuSDDV=@ps AND MaDV=@mdv", cn, tx);
                        cmdUpdateCT.Parameters.AddWithValue("@sl", soLuong);
                        cmdUpdateCT.Parameters.AddWithValue("@ps", soPhieuSDDV);
                        cmdUpdateCT.Parameters.AddWithValue("@mdv", maDV);
                        cmdUpdateCT.ExecuteNonQuery();
                    }
                    else
                    {
                        var cmdInsertCT = new SqlCommand("INSERT INTO ChiTietPhieuSuDungDV(SoPhieuSDDV, MaDV, SoLuong, DonGia) VALUES(@ps, @mdv, @sl, @g)", cn, tx);
                        cmdInsertCT.Parameters.AddWithValue("@ps", soPhieuSDDV);
                        cmdInsertCT.Parameters.AddWithValue("@mdv", maDV);
                        cmdInsertCT.Parameters.AddWithValue("@sl", soLuong);
                        cmdInsertCT.Parameters.AddWithValue("@g", donGia);
                        cmdInsertCT.ExecuteNonQuery();
                    }

                    tx.Commit();
                    return KetQuaXuLy.Ok("Đã thêm dịch vụ thành công.");
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