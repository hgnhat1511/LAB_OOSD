using QuanLyThuVien.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyThuVien.Services
{
    public class DocGiaService
    {
        public DataTable LayTatCa()
        {
            return Db.Query("SELECT MaDocGia, HoTen, NgaySinh, Phai, SoDienThoai FROM DocGia");
        }

        public DataTable TimKiem(string tuKhoa)
        {
            return Db.Query("SELECT * FROM DocGia WHERE MaDocGia LIKE @kw OR HoTen LIKE @kw",
                            new SqlParameter("@kw", "%" + tuKhoa + "%"));
        }

        public KetQuaXuLy LuuDocGia(string ma, string ten, DateTime ngaySinh, string phai, string sdt, bool capNhat)
        {
            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(ten))
                return KetQuaXuLy.Loi("Mã và tên độc giả không được để trống.");
            try
            {
                string sql = capNhat
                    ? "UPDATE DocGia SET HoTen=@Ten, NgaySinh=@NgaySinh, Phai=@Phai, SoDienThoai=@SDT WHERE MaDocGia=@Ma"
                    : "INSERT INTO DocGia(MaDocGia, HoTen, NgaySinh, Phai, SoDienThoai) VALUES(@Ma, @Ten, @NgaySinh, @Phai, @SDT)";

                int n = Db.Execute(sql,
                    new SqlParameter("@Ma", ma.Trim()),
                    new SqlParameter("@Ten", ten.Trim()),
                    new SqlParameter("@NgaySinh", ngaySinh),
                    new SqlParameter("@Phai", string.IsNullOrEmpty(phai) ? (object)DBNull.Value : phai),
                    new SqlParameter("@SDT", string.IsNullOrEmpty(sdt) ? (object)DBNull.Value : sdt));

                return n > 0 ? KetQuaXuLy.Ok("Lưu độc giả thành công.") : KetQuaXuLy.Loi("Không có dữ liệu thay đổi.");
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601) return KetQuaXuLy.Loi("Mã độc giả đã tồn tại.");
                return KetQuaXuLy.Loi("Lỗi cơ sở dữ liệu: " + ex.Message);
            }
        }

        public KetQuaXuLy XoaDocGia(string ma)
        {
            try
            {
                int n = Db.Execute("DELETE FROM DocGia WHERE MaDocGia=@Ma", new SqlParameter("@Ma", ma));
                return n > 0 ? KetQuaXuLy.Ok("Xóa thành công.") : KetQuaXuLy.Loi("Không tìm thấy dữ liệu.");
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) return KetQuaXuLy.Loi("Độc giả này đang có phiếu mượn/phạt, không thể xóa.");
                return KetQuaXuLy.Loi("Lỗi cơ sở dữ liệu: " + ex.Message);
            }
        }
        public DataTable LayComboDocGia()
        {
            // Đã sửa thành gọi trực tiếp cột HoTen thay vì ghép (Ho + Ten)
            return Db.Query("SELECT MaDocGia, HoTen FROM DocGia ORDER BY HoTen");
        }
    }
}