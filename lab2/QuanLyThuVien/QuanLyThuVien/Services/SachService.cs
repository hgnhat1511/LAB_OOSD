using QuanLyThuVien.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyThuVien.Services
{
    public class SachService
    {
        public DataTable LayTatCa()
        {
            return Db.Query("SELECT MaDauSach, TenSach, NamXuatBan, SoLuongHienCo, MaTheLoai, MaNhaXuatBan FROM DauSach");
        }

        public DataTable TimKiem(string tuKhoa)
        {
            return Db.Query("SELECT * FROM DauSach WHERE MaDauSach LIKE @kw OR TenSach LIKE @kw",
                            new SqlParameter("@kw", "%" + tuKhoa + "%"));
        }

        public KetQuaXuLy LuuSach(string ma, string ten, int nam, int sl, string maTL, string maNXB, bool capNhat)
        {
            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(ten))
                return KetQuaXuLy.Loi("Mã và tên sách không được để trống.");
            try
            {
                string sql = capNhat
                    ? "UPDATE DauSach SET TenSach=@Ten, NamXuatBan=@Nam, SoLuongHienCo=@SL, MaTheLoai=@MaTL, MaNhaXuatBan=@MaNXB WHERE MaDauSach=@Ma"
                    : "INSERT INTO DauSach(MaDauSach, TenSach, NamXuatBan, SoLuongHienCo, MaTheLoai, MaNhaXuatBan) VALUES(@Ma, @Ten, @Nam, @SL, @MaTL, @MaNXB)";

                int n = Db.Execute(sql,
                    new SqlParameter("@Ma", ma.Trim()),
                    new SqlParameter("@Ten", ten.Trim()),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@SL", sl),
                    new SqlParameter("@MaTL", string.IsNullOrEmpty(maTL) ? (object)DBNull.Value : maTL),
                    new SqlParameter("@MaNXB", string.IsNullOrEmpty(maNXB) ? (object)DBNull.Value : maNXB));

                return n > 0 ? KetQuaXuLy.Ok("Lưu sách thành công.") : KetQuaXuLy.Loi("Không có dữ liệu thay đổi.");
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601) return KetQuaXuLy.Loi("Mã sách đã tồn tại.");
                return KetQuaXuLy.Loi("Lỗi cơ sở dữ liệu: " + ex.Message);
            }
        }

        public KetQuaXuLy XoaSach(string ma)
        {
            try
            {
                int n = Db.Execute("DELETE FROM DauSach WHERE MaDauSach=@Ma", new SqlParameter("@Ma", ma));
                return n > 0 ? KetQuaXuLy.Ok("Xóa thành công.") : KetQuaXuLy.Loi("Không tìm thấy dữ liệu.");
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) return KetQuaXuLy.Loi("Sách đang được mượn hoặc có phiếu phạt, không thể xóa.");
                return KetQuaXuLy.Loi("Lỗi cơ sở dữ liệu: " + ex.Message);
            }
        }

        public DataTable LayDSTheLoai() { return Db.Query("SELECT MaTheLoai, TenTheLoai FROM TheLoai"); }
        public DataTable LayDSNhaXuatBan() { return Db.Query("SELECT MaNhaXuatBan FROM NhaXuatBan"); }
    }
}