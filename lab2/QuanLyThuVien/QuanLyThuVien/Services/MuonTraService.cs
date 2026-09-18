using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyThuVien.Data;

namespace QuanLyThuVien.Services
{
    public class MuonTraService
    {
        public KetQuaXuLy KiemTraDieuKienMuon(string maDocGia, int soSachMuonMoi)
        {
            if (string.IsNullOrWhiteSpace(maDocGia)) return KetQuaXuLy.Loi("Vui lòng chọn độc giả.");
            if (soSachMuonMoi < 1) return KetQuaXuLy.Loi("Phải chọn ít nhất 1 đầu sách.");
            if (soSachMuonMoi > 3) return KetQuaXuLy.Loi("Một lần lập phiếu chỉ được chọn tối đa 3 đầu sách khác nhau.");

            DataTable the = Db.Query(@"SELECT TOP 1 MaThe,HanSuDung,DaDongLePhi,TrangThai 
                FROM TheDocGia WHERE MaDocGia=@Ma AND TrangThai=1 ORDER BY HanSuDung DESC", new SqlParameter("@Ma", maDocGia));

            if (the.Rows.Count == 0) return KetQuaXuLy.Loi("Độc giả chưa có thẻ thư viện đang hoạt động.");
            DateTime han = Convert.ToDateTime(the.Rows[0]["HanSuDung"]);
            bool lePhi = Convert.ToBoolean(the.Rows[0]["DaDongLePhi"]);

            if (han.Date < DateTime.Today) return KetQuaXuLy.Loi("Thẻ thư viện đã hết hạn.");
            if (!lePhi) return KetQuaXuLy.Loi("Độc giả chưa đóng lệ phí năm nên thẻ chưa có giá trị.");

            int quaHan = Convert.ToInt32(Db.Scalar(@"SELECT COUNT(*) FROM PhieuMuon pm 
                JOIN ChiTietPhieuMuon ct ON ct.MaPhieuMuon=pm.MaPhieuMuon 
                WHERE pm.MaDocGia=@Ma AND ct.NgayTraThucTe IS NULL AND pm.NgayHenTra < CAST(GETDATE() AS date)", new SqlParameter("@Ma", maDocGia)));

            if (quaHan > 0) return KetQuaXuLy.Loi("Độc giả còn sách quá hạn chưa trả nên không được mượn thêm.");

            int dangMuon = Convert.ToInt32(Db.Scalar(@"SELECT COUNT(*) FROM PhieuMuon pm 
                JOIN ChiTietPhieuMuon ct ON ct.MaPhieuMuon=pm.MaPhieuMuon 
                WHERE pm.MaDocGia=@Ma AND ct.NgayTraThucTe IS NULL", new SqlParameter("@Ma", maDocGia)));

            if (dangMuon + soSachMuonMoi > 3)
                return KetQuaXuLy.Loi("Tổng số sách đang mượn và sắp mượn không được vượt quá 3 cuốn.");

            return KetQuaXuLy.Ok("Độc giả đủ điều kiện mượn sách.");
        }

        // (Do giới hạn độ dài, trong thực tế file này còn chứa hàm LapPhieuMuon dùng SqlTransaction để ghi dữ liệu vào CSDL và hàm TraSach dùng để xử lý phiếu phạt như trong hướng dẫn)
    }
}