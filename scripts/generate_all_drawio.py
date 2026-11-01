import os
import sys

sys.path.insert(0, os.path.dirname(__file__))
from drawio_builder import DrawIOBuilder

sys.stdout.reconfigure(encoding='utf-8')

def make_file(path, xml):
    os.makedirs(os.path.dirname(path), exist_ok=True)
    with open(path, 'w', encoding='utf-8') as f:
        f.write(xml)
    print(f"Generated: {path} ({len(xml)} bytes)")

# ==============================================================================
# LAB 1: THƯ VIỆN TRỰC TUYẾN & CỬA HÀNG NƯỚC GIẢI KHÁT
# ==============================================================================
def gen_lab1():
    # 1. Bai 1: Use Case Model Thư viện online
    b = DrawIOBuilder("LAB1_Bai1_UseCase_Model")
    b.add_boundary("HỆ THỐNG THƯ VIỆN TRỰC TUYẾN (ONLINE LIBRARY)", 200, 40, 700, 680)
    a_reader = b.add_actor("Bạn đọc\n(Độc giả)", 60, 240)
    a_lib = b.add_actor("Thủ thư", 960, 180)
    a_admin = b.add_actor("Quản trị viên\n(Admin)", 960, 420)
    
    uc1 = b.add_usecase("UC01: Tìm kiếm tài liệu", 260, 90)
    uc2 = b.add_usecase("UC02: Đọc sách trực tuyến", 260, 170)
    uc3 = b.add_usecase("UC03: Tải tài liệu điện tử", 260, 250)
    uc4 = b.add_usecase("UC04: Đăng ký mượn sách", 260, 330)
    uc5 = b.add_usecase("UC05: Đăng ký tài khoản", 260, 410)
    uc6 = b.add_usecase("UC06: Đăng nhập", 260, 490)
    uc7 = b.add_usecase("UC07: Yêu cầu đặt mua sách", 260, 570)
    
    uc8 = b.add_usecase("UC08: Quản lý bạn đọc", 650, 120)
    uc9 = b.add_usecase("UC09: Quản lý tài liệu / sách", 650, 200)
    uc10 = b.add_usecase("UC10: Quản lý mượn trả", 650, 280)
    uc11 = b.add_usecase("UC11: Xử lý yêu cầu mua", 650, 360)
    uc12 = b.add_usecase("UC12: Tạo báo cáo thống kê", 650, 450)
    uc13 = b.add_usecase("UC13: Gửi email nhắc nhở", 650, 540)
    
    for uc in [uc1, uc2, uc3, uc4, uc5, uc6, uc7]:
        b.add_relation(a_reader, uc)
    for uc in [uc8, uc9, uc10, uc11, uc12, uc13]:
        b.add_relation(a_lib, uc)
    for uc in [uc8, uc12]:
        b.add_relation(a_admin, uc)
    b.add_relation(uc3, uc6, "yêu cầu thẻ", "include")
    b.add_relation(uc4, uc6, "yêu cầu thẻ", "include")
    make_file(r"IMAGES\LAB1\LAB1_Bai1_UseCase_Model.drawio", b.build_xml())

    # 2. Bai 2: Use Case tổng quát Cửa hàng nước giải khát
    b = DrawIOBuilder("LAB1_Bai2_UseCase_TongQuat")
    b.add_boundary("HỆ THỐNG QUẢN LÝ CỬA HÀNG NƯỚC GIẢI KHÁT", 220, 40, 680, 560)
    a_kh = b.add_actor("Khách hàng", 60, 160)
    a_kq = b.add_actor("Khách quen", 60, 320)
    a_kv = b.add_actor("Khách vãng lai", 60, 460)
    b.add_relation(a_kq, a_kh, "", "inherit")
    b.add_relation(a_kv, a_kh, "", "inherit")
    
    a_nv = b.add_actor("Nhân viên\ncửa hàng", 960, 220)
    a_ncu = b.add_actor("Nhà cung ứng", 960, 440)
    
    uc_order = b.add_usecase("Đặt mua nước giải khát", 280, 100)
    uc_sell = b.add_usecase("Bán hàng & Giao ngay", 280, 200)
    uc_hen = b.add_usecase("Hẹn giao hàng theo đợt", 280, 300)
    uc_no = b.add_usecase("Lập phiếu ghi nợ", 280, 400)
    uc_nhap = b.add_usecase("Quản lý Đơn đặt hàng nhập", 660, 340)
    uc_nhan = b.add_usecase("Tiếp nhận Phiếu giao hàng", 660, 440)
    uc_stat = b.add_usecase("Tra cứu & Thống kê tồn kho", 660, 180)
    
    b.add_relation(a_kh, uc_order)
    b.add_relation(a_kh, uc_sell)
    b.add_relation(a_kq, uc_hen)
    b.add_relation(a_kq, uc_no)
    b.add_relation(a_nv, uc_sell)
    b.add_relation(a_nv, uc_hen)
    b.add_relation(a_nv, uc_no)
    b.add_relation(a_nv, uc_nhap)
    b.add_relation(a_nv, uc_nhan)
    b.add_relation(a_nv, uc_stat)
    b.add_relation(a_ncu, uc_nhap)
    b.add_relation(a_ncu, uc_nhan)
    make_file(r"IMAGES\LAB1\LAB1_Bai2_UseCase_TongQuat.drawio", b.build_xml())

    # 3. Bai 2: Class Diagram Cửa hàng nước giải khát
    b = DrawIOBuilder("LAB1_Bai2_Class_Diagram", 1800, 1200)
    c_nh = b.add_class("NhanHieu", ["- maNhanHieu: String", "- tenNhanHieu: String", "- quocGia: String"], ["+ themNhanHieu()", "+ suaNhanHieu()"], 40, 60, 200)
    c_ngk = b.add_class("NuocGiaiKhat", ["- maNGK: String", "- tenNGK: String", "- donViTinh: String", "- donGiaHienTai: double", "- soLuongTon: int"], ["+ capNhatGia()", "+ kiemTraTon()", "+ nhapKho()"], 300, 60, 230)
    c_ncu = b.add_class("NhaCungUng", ["- maNCU: String", "- tenNCU: String", "- diaChi: String", "- dienThoai: String"], ["+ themNCU()", "+ capNhatNCU()"], 600, 60, 210)
    c_nv = b.add_class("NhanVien", ["- maNV: String", "- hoTen: String", "- chucVu: String", "- soDienThoai: String"], ["+ lapDonHang()", "+ lapHoaDon()"], 880, 60, 200)
    
    c_ddh = b.add_class("DonDatHang", ["- soDDH: String", "- ngayDat: Date", "- tongTienDat: double"], ["+ lapDDH()", "+ guiDDH()"], 600, 320, 210)
    c_ctddh = b.add_class("ChiTietDDH", ["- soLuongDat: int", "- donGiaDat: double"], ["+ tinhThanhTien()"], 320, 320, 190)
    c_pgh = b.add_class("PhieuGiaoHang", ["- soPGH: String", "- ngayGiao: Date", "- dotGiao: int", "- tongTienGiao: double"], ["+ lapPGH()", "+ xacNhanGiao()"], 600, 550, 210)
    c_ctpgh = b.add_class("ChiTietPGH", ["- soLuongGiao: int", "- donGiaGiao: double"], ["+ tinhTienGiao()"], 320, 550, 190)
    
    c_kh = b.add_class("KhachHang", ["- maKH: String", "- tenKH: String", "- diaChi: String", "- soDT: String", "- laKhachQuen: boolean"], ["+ capNhatKH()", "+ kiemTraLoai()"], 880, 320, 200)
    c_hd = b.add_class("HoaDon", ["- soHD: String", "- ngayLap: Date", "- tongTien: double", "- trangThai: String"], ["+ lapHoaDon()", "+ thanhToan()"], 1150, 320, 200)
    c_cthd = b.add_class("ChiTietHD", ["- soLuongMua: int", "- donGiaBan: double"], ["+ thanhTien()"], 1420, 320, 180)
    c_ph = b.add_class("PhieuHen", ["- soPH: String", "- ngayHenGiao: Date", "- dotHen: int"], ["+ lapPhieuHen()"], 1150, 550, 200)
    c_no = b.add_class("PhieuGhiNo", ["- soPGN: String", "- ngayNo: Date", "- soTienNo: double", "- ngayHenTra: Date", "- lyDoNo: String"], ["+ lapPhieuNo()", "+ thanhToanNo()"], 1420, 550, 210)
    
    b.add_relation(c_nh, c_ngk, "1..*")
    b.add_relation(c_ncu, c_ngk, "1..*")
    b.add_relation(c_ncu, c_ddh, "1..*")
    b.add_relation(c_ddh, c_ctddh, "1..*")
    b.add_relation(c_ctddh, c_ngk, "*..1")
    b.add_relation(c_ddh, c_pgh, "1..3")
    b.add_relation(c_pgh, c_ctpgh, "1..*")
    b.add_relation(c_kh, c_hd, "1..*")
    b.add_relation(c_hd, c_cthd, "1..*")
    b.add_relation(c_hd, c_ph, "1..*")
    b.add_relation(c_hd, c_no, "0..1")
    b.add_relation(c_nv, c_hd, "1..*")
    make_file(r"IMAGES\LAB1\LAB1_Bai2_Class_Diagram.drawio", b.build_xml())

    # 4. Bai 2: Activity Diagram Bán hàng & Hẹn giao
    b = DrawIOBuilder("LAB1_Bai2_Activity_BanHang")
    n_start = b.add_initial_node(380, 30)
    act1 = b.add_activity("Khách hàng đặt mua NGK", 290, 80)
    act2 = b.add_activity("Kiểm tra tồn kho số lượng yêu cầu", 270, 160)
    dec1 = b.add_decision("Đủ hàng?", 340, 240)
    act_giao_ngay = b.add_activity("Lập Hóa Đơn & Giao hàng ngay", 160, 330)
    act_hen = b.add_activity("Kiểm tra khách quen & Lập Phiếu Hẹn", 440, 330)
    dec_pay = b.add_decision("Trả ngay / Nợ?", 190, 420)
    act_cash = b.add_activity("Thanh toán tiền mặt/chuyển khoản", 80, 500)
    act_no = b.add_activity("Lập Phiếu Ghi Nợ (Khách quen)", 260, 500)
    act_deliver_later = b.add_activity("Nhập hàng từ NCU & Giao theo đợt hẹn", 440, 420)
    n_end = b.add_final_node(370, 600)
    
    b.add_relation(n_start, act1, "", "arrow")
    b.add_relation(act1, act2, "", "arrow")
    b.add_relation(act2, dec1, "", "arrow")
    b.add_relation(dec1, act_giao_ngay, "[Có đủ]", "arrow")
    b.add_relation(dec1, act_hen, "[Thiếu hàng]", "arrow")
    b.add_relation(act_giao_ngay, dec_pay, "", "arrow")
    b.add_relation(dec_pay, act_cash, "[Trả ngay]", "arrow")
    b.add_relation(dec_pay, act_no, "[Khách nợ]", "arrow")
    b.add_relation(act_hen, act_deliver_later, "", "arrow")
    b.add_relation(act_cash, n_end, "", "arrow")
    b.add_relation(act_no, n_end, "", "arrow")
    b.add_relation(act_deliver_later, n_end, "", "arrow")
    make_file(r"IMAGES\LAB1\LAB1_Bai2_Activity_Diagram.drawio", b.build_xml())

    # 5. Bai 2: Sequence Diagram Lập phiếu ghi nợ
    b = DrawIOBuilder("LAB1_Bai2_Sequence_LapPhieuNo", 1200, 700)
    l_kh = b.add_lifeline("Khách quen\n:KhachHang", 100, 40)
    l_nv = b.add_lifeline("Nhân viên\n:NhanVien", 350, 40)
    l_form = b.add_lifeline("Giao diện\n:FrmBanHang", 600, 40)
    l_db = b.add_lifeline("Cơ sở dữ liệu\n:Database", 850, 40)
    
    b.add_relation(l_kh, l_nv, "1: Yêu cầu nợ tiền hàng", "arrow")
    b.add_relation(l_nv, l_form, "2: Nhập mã khách & kiểm tra hạn mức", "arrow")
    b.add_relation(l_form, l_db, "3: SELECT ThongTinNo WHERE MaKH", "arrow")
    b.add_relation(l_db, l_form, "4: Trả về kết quả: Hợp lệ", "return")
    b.add_relation(l_form, l_nv, "5: Hiển thị form Lập Phiếu Ghi Nợ", "arrow")
    b.add_relation(l_nv, l_form, "6: Nhập số tiền nợ, ngày hẹn trả, lý do", "arrow")
    b.add_relation(l_form, l_db, "7: INSERT INTO PhieuGhiNo, UPDATE CongNo", "arrow")
    b.add_relation(l_db, l_form, "8: Lưu thành công (Success)", "return")
    b.add_relation(l_form, l_nv, "9: In phiếu ghi nợ & cập nhật hóa đơn", "arrow")
    b.add_relation(l_nv, l_kh, "10: Giao phiếu nợ & xuất hàng cho khách", "arrow")
    make_file(r"IMAGES\LAB1\LAB1_Bai2_Sequence_Diagram.drawio", b.build_xml())

# ==============================================================================
# LAB 2: HỆ THỐNG QUẢN LÝ THƯ VIỆN
# ==============================================================================
def gen_lab2():
    # 1. Use Case tổng quát
    b = DrawIOBuilder("LAB2_UseCase_TongQuat")
    b.add_boundary("HỆ THỐNG QUẢN LÝ THƯ VIỆN", 220, 40, 660, 520)
    a_dg = b.add_actor("Độc giả", 60, 180)
    a_tt = b.add_actor("Thủ thư", 940, 180)
    a_ql = b.add_actor("Quản lý", 940, 380)
    
    uc1 = b.add_usecase("Đăng nhập hệ thống", 260, 80)
    uc2 = b.add_usecase("Tra cứu thông tin sách", 260, 170)
    uc3 = b.add_usecase("Đăng ký làm thẻ thư viện", 260, 260)
    uc4 = b.add_usecase("Mượn sách thư viện", 260, 350)
    uc5 = b.add_usecase("Trả sách & Nộp phạt", 260, 440)
    
    uc6 = b.add_usecase("Quản lý danh mục sách", 620, 120)
    uc7 = b.add_usecase("Lập phiếu mượn sách", 620, 200)
    uc8 = b.add_usecase("Xử lý nhận trả & Viết phiếu phạt", 620, 280)
    uc9 = b.add_usecase("Quản lý hồ sơ độc giả & Cấp thẻ", 620, 360)
    uc10 = b.add_usecase("Báo cáo thống kê mượn trả", 620, 450)
    
    b.add_relation(a_dg, uc1)
    b.add_relation(a_dg, uc2)
    b.add_relation(a_dg, uc3)
    b.add_relation(a_dg, uc4)
    b.add_relation(a_dg, uc5)
    b.add_relation(a_tt, uc1)
    b.add_relation(a_tt, uc6)
    b.add_relation(a_tt, uc7)
    b.add_relation(a_tt, uc8)
    b.add_relation(a_tt, uc9)
    b.add_relation(a_ql, uc10)
    make_file(r"IMAGES\LAB2\LAB2_UseCase_TongQuat.drawio", b.build_xml())

    # 2. Class Diagram
    b = DrawIOBuilder("LAB2_Class_Diagram", 1600, 1000)
    c_tl = b.add_class("TheLoai", ["- maTheLoai: String", "- tenTheLoai: String"], ["+ themTheLoai()", "+ suaTheLoai()"], 60, 60, 190)
    c_ds = b.add_class("DauSach", ["- maDauSach: String", "- tenSach: String", "- tacGia: String", "- nhaXuatBan: String", "- namXB: int"], ["+ themDauSach()", "+ timKiem()"], 340, 60, 210)
    c_s = b.add_class("CuonSach", ["- maCuonSach: String", "- tinhTrang: String", "- viTri: String"], ["+ capNhatTinhTrang()", "+ kiemTraSanSang()"], 640, 60, 200)
    
    c_dg = b.add_class("DocGia", ["- maDocGia: String", "- hoTen: String", "- ngaySinh: Date", "- diaChi: String", "- sdt: String"], ["+ capNhatThongTin()", "+ kiemTraThe()"], 60, 320, 200)
    c_the = b.add_class("TheThuVien", ["- maThe: String", "- ngayCap: Date", "- ngayHetHan: Date", "- trangThai: String"], ["+ giaHanThe()", "+ khoaThe()"], 340, 320, 190)
    c_nv = b.add_class("NhanVien", ["- maNV: String", "- hoTen: String", "- quyenHan: String"], ["+ lapPhieuMuon()", "+ nhanTra()"], 950, 320, 190)
    
    c_pm = b.add_class("PhieuMuon", ["- maPhieuMuon: String", "- ngayMuon: Date", "- hanTra: Date", "- tongSoLuong: int"], ["+ lapPhieu()", "+ kiemTraHan()"], 640, 320, 200)
    c_ctm = b.add_class("ChiTietMuon", ["- ghiChu: String", "- daTra: boolean"], ["+ xacNhanTra()"], 640, 560, 180)
    c_pt = b.add_class("PhieuTra", ["- maPhieuTra: String", "- ngayTraThucTe: Date", "- tienPhat: double"], ["+ lapPhieuTra()", "+ tinhTienPhat()"], 340, 560, 200)
    
    b.add_relation(c_tl, c_ds, "1..*")
    b.add_relation(c_ds, c_s, "1..*")
    b.add_relation(c_dg, c_the, "1..1")
    b.add_relation(c_the, c_pm, "1..*")
    b.add_relation(c_pm, c_ctm, "1..*")
    b.add_relation(c_ctm, c_s, "*..1")
    b.add_relation(c_nv, c_pm, "1..*")
    b.add_relation(c_ctm, c_pt, "0..1")
    make_file(r"IMAGES\LAB2\LAB2_Class_Diagram.drawio", b.build_xml())

    # 3. ERD Thư viện
    b = DrawIOBuilder("LAB2_ERD_QuanLyThuVien", 1500, 900)
    t_dg = b.add_class("DOC_GIA", ["PK  MaDocGia: VARCHAR(20)", "    HoTen: NVARCHAR(100)", "    NgaySinh: DATE", "    DiaChi: NVARCHAR(200)", "    SoDT: VARCHAR(15)"], [], 60, 80, 210, fill="#fff2cc", stroke="#d6b656")
    t_the = b.add_class("THE_THU_VIEN", ["PK  MaThe: VARCHAR(20)", "FK  MaDocGia: VARCHAR(20)", "    NgayCap: DATE", "    NgayHetHan: DATE", "    TrangThai: NVARCHAR(50)"], [], 350, 80, 210, fill="#fff2cc", stroke="#d6b656")
    t_pm = b.add_class("PHIEU_MUON", ["PK  MaPhieuMuon: VARCHAR(20)", "FK  MaThe: VARCHAR(20)", "FK  MaNV: VARCHAR(20)", "    NgayMuon: DATETIME", "    HanTra: DATETIME"], [], 650, 80, 220, fill="#fff2cc", stroke="#d6b656")
    t_nv = b.add_class("NHAN_VIEN", ["PK  MaNV: VARCHAR(20)", "    HoTen: NVARCHAR(100)", "    ChucVu: NVARCHAR(50)", "    MatKhau: VARCHAR(100)"], [], 970, 80, 200, fill="#fff2cc", stroke="#d6b656")
    
    t_ctm = b.add_class("CHI_TIET_MUON", ["PK,FK MaPhieuMuon: VARCHAR(20)", "PK,FK MaCuonSach: VARCHAR(20)", "      DaTra: BIT", "      GhiChu: NVARCHAR(200)"], [], 650, 360, 230, fill="#fff2cc", stroke="#d6b656")
    t_cs = b.add_class("CUON_SACH", ["PK  MaCuonSach: VARCHAR(20)", "FK  MaDauSach: VARCHAR(20)", "    TinhTrang: NVARCHAR(50)", "    ViTri: NVARCHAR(50)"], [], 350, 360, 220, fill="#fff2cc", stroke="#d6b656")
    t_ds = b.add_class("DAU_SACH", ["PK  MaDauSach: VARCHAR(20)", "FK  MaTheLoai: VARCHAR(20)", "    TenSach: NVARCHAR(200)", "    TacGia: NVARCHAR(100)", "    NhaXB: NVARCHAR(100)"], [], 60, 360, 230, fill="#fff2cc", stroke="#d6b656")
    t_tl = b.add_class("THE_LOAI", ["PK  MaTheLoai: VARCHAR(20)", "    TenTheLoai: NVARCHAR(100)"], [], 60, 620, 210, fill="#fff2cc", stroke="#d6b656")
    
    b.add_relation(t_dg, t_the, "1 - 1")
    b.add_relation(t_the, t_pm, "1 - n")
    b.add_relation(t_nv, t_pm, "1 - n")
    b.add_relation(t_pm, t_ctm, "1 - n")
    b.add_relation(t_cs, t_ctm, "1 - n")
    b.add_relation(t_ds, t_cs, "1 - n")
    b.add_relation(t_tl, t_ds, "1 - n")
    make_file(r"IMAGES\LAB2\LAB2_ERD_QuanLyThuVien.drawio", b.build_xml())

    # 4. Activity Diagram Mượn - Trả sách
    b = DrawIOBuilder("LAB2_Activity_MuonTraSach")
    st = b.add_initial_node(380, 20)
    a1 = b.add_activity("Độc giả xuất trình Thẻ & Sách cần mượn", 260, 70)
    a2 = b.add_activity("Thủ thư quét mã thẻ & kiểm tra hạn sử dụng", 250, 150)
    d1 = b.add_decision("Thẻ hợp lệ?", 340, 230)
    a_fail = b.add_activity("Thông báo thẻ hết hạn / bị khóa", 120, 310, fill="#f8cecc", stroke="#b85450")
    a3 = b.add_activity("Kiểm tra số lượng sách đang mượn & nợ phạt", 430, 310)
    d2 = b.add_decision("Đủ điều kiện?", 460, 390)
    a_deny = b.add_activity("Từ chối mượn & yêu cầu trả sách cũ / nộp phạt", 260, 470, fill="#f8cecc", stroke="#b85450")
    a4 = b.add_activity("Lập Phiếu Mượn & Ghi nhận hạn trả (14 ngày)", 560, 470)
    a5 = b.add_activity("Cập nhật trạng thái sách: ĐANG MƯỢN", 560, 560)
    en = b.add_final_node(380, 650)
    
    b.add_relation(st, a1, "", "arrow")
    b.add_relation(a1, a2, "", "arrow")
    b.add_relation(a2, d1, "", "arrow")
    b.add_relation(d1, a_fail, "[Không hợp lệ]", "arrow")
    b.add_relation(d1, a3, "[Hợp lệ]", "arrow")
    b.add_relation(a3, d2, "", "arrow")
    b.add_relation(d2, a_deny, "[Quá hạn / nợ]", "arrow")
    b.add_relation(d2, a4, "[Đủ điều kiện]", "arrow")
    b.add_relation(a4, a5, "", "arrow")
    b.add_relation(a_fail, en, "", "arrow")
    b.add_relation(a_deny, en, "", "arrow")
    b.add_relation(a5, en, "", "arrow")
    make_file(r"IMAGES\LAB2\LAB2_Activity_MuonTraSach.drawio", b.build_xml())

    # 5. Sequence Diagram Lập phiếu mượn sách
    b = DrawIOBuilder("LAB2_Sequence_LapPhieuMuon", 1200, 700)
    l_dg = b.add_lifeline("Độc giả\n:DocGia", 100)
    l_tt = b.add_lifeline("Thủ thư\n:ThuThu", 350)
    l_ui = b.add_lifeline("Giao diện\n:FrmMuonTra", 600)
    l_db = b.add_lifeline("Database\n:SQLServer", 850)
    
    b.add_relation(l_dg, l_tt, "1: Gửi mã thẻ & sách muốn mượn", "arrow")
    b.add_relation(l_tt, l_ui, "2: Nhập MaThe & MaCuonSach", "arrow")
    b.add_relation(l_ui, l_db, "3: KiemTraTheHopLe(maThe)", "arrow")
    b.add_relation(l_db, l_ui, "4: Trả về kết quả: Hợp lệ", "return")
    b.add_relation(l_ui, l_db, "5: KiemTraSachSanSang(maSach)", "arrow")
    b.add_relation(l_db, l_ui, "6: Trả về kết quả: Sẵn sàng", "return")
    b.add_relation(l_ui, l_db, "7: INSERT PhieuMuon, ChiTietMuon; UPDATE CuonSach", "arrow")
    b.add_relation(l_db, l_ui, "8: Ghi dữ liệu thành công", "return")
    b.add_relation(l_ui, l_tt, "9: Hiển thị thông báo mượn thành công & in phiếu", "arrow")
    b.add_relation(l_tt, l_dg, "10: Trao sách và phiếu mượn hẹn ngày trả", "arrow")
    make_file(r"IMAGES\LAB2\LAB2_Sequence_LapPhieuMuon.drawio", b.build_xml())

# ==============================================================================
# LAB 3: HỆ THỐNG QUẢN LÝ KHÁCH SẠN
# ==============================================================================
def gen_lab3():
    # 1. Use Case Diagram Khách sạn
    b = DrawIOBuilder("LAB3_UseCase_QuanLyKhachSan")
    b.add_boundary("HỆ THỐNG QUẢN LÝ KHÁCH SẠN", 220, 40, 680, 560)
    a_lt = b.add_actor("Nhân viên\nLễ tân", 60, 200)
    a_ql = b.add_actor("Quản lý\nkhách sạn", 960, 200)
    a_kh = b.add_actor("Khách thuê", 60, 420)
    
    uc1 = b.add_usecase("Đăng nhập hệ thống", 270, 70)
    uc2 = b.add_usecase("Quản lý danh mục Phòng", 270, 150)
    uc3 = b.add_usecase("Quản lý Tiện nghi phòng", 270, 230)
    uc4 = b.add_usecase("Đặt phòng & Nhận phòng", 270, 310)
    uc5 = b.add_usecase("Gọi dịch vụ gia tăng", 270, 390)
    uc6 = b.add_usecase("Trả phòng & Lập hóa đơn", 270, 470)
    
    uc7 = b.add_usecase("Quản lý Bảng giá & Loại phòng", 650, 150)
    uc8 = b.add_usecase("Thống kê doanh thu & Tần suất", 650, 270)
    uc9 = b.add_usecase("Quản lý tài khoản nhân viên", 650, 390)
    
    for uc in [uc1, uc2, uc3, uc4, uc5, uc6]:
        b.add_relation(a_lt, uc)
    b.add_relation(a_kh, uc4)
    b.add_relation(a_kh, uc5)
    for uc in [uc1, uc7, uc8, uc9]:
        b.add_relation(a_ql, uc)
    make_file(r"IMAGES\LAB3\LAB3_UseCase_QuanLyKhachSan.drawio", b.build_xml())

    # 2. ERD Khách sạn
    b = DrawIOBuilder("LAB3_ERD_QuanLyKhachSan", 1500, 900)
    t_lp = b.add_class("LOAI_PHONG", ["PK  MaLoaiPhong: INT", "    TenLoaiPhong: NVARCHAR(50)", "    DonGiaNgay: DECIMAL(18,2)", "    SoNguoiToiDa: INT"], [], 60, 60, 210, fill="#dae8fc", stroke="#6c8ebf")
    t_p = b.add_class("PHONG", ["PK  MaPhong: INT", "FK  MaLoaiPhong: INT", "    SoPhong: VARCHAR(10)", "    Tang: INT", "    TrangThai: NVARCHAR(30)"], [], 350, 60, 210, fill="#dae8fc", stroke="#6c8ebf")
    t_tn = b.add_class("TIEN_NGHI", ["PK  MaTienNghi: INT", "    TenTienNghi: NVARCHAR(100)", "    GiaTri: DECIMAL(18,2)"], [], 60, 300, 210, fill="#dae8fc", stroke="#6c8ebf")
    t_ptn = b.add_class("PHONG_TIENNGHI", ["PK,FK MaPhong: INT", "PK,FK MaTienNghi: INT", "      SoLuong: INT"], [], 350, 300, 210, fill="#dae8fc", stroke="#6c8ebf")
    
    t_kh = b.add_class("KHACH_HANG", ["PK  MaKhachHang: INT", "    HoTen: NVARCHAR(100)", "    CCCD: VARCHAR(20)", "    SoDienThoai: VARCHAR(15)", "    DiaChi: NVARCHAR(200)"], [], 650, 60, 220, fill="#dae8fc", stroke="#6c8ebf")
    t_pdp = b.add_class("PHIEU_DAT_PHONG", ["PK  MaPhieuDat: INT", "FK  MaKhachHang: INT", "FK  MaPhong: INT", "    NgayDat: DATETIME", "    NgayNhanDuKien: DATETIME", "    NgayTraDuKien: DATETIME", "    TienDatCoc: DECIMAL(18,2)", "    TrangThai: NVARCHAR(30)"], [], 650, 300, 230, fill="#dae8fc", stroke="#6c8ebf")
    t_dv = b.add_class("DICH_VU", ["PK  MaDichVu: INT", "    TenDichVu: NVARCHAR(100)", "    DonGia: DECIMAL(18,2)", "    DonViTinh: NVARCHAR(20)"], [], 970, 60, 210, fill="#dae8fc", stroke="#6c8ebf")
    t_ctdv = b.add_class("CHI_TIET_DICH_VU", ["PK  MaChiTietDV: INT", "FK  MaPhieuDat: INT", "FK  MaDichVu: INT", "    NgaySuDung: DATETIME", "    SoLuong: INT", "    ThanhTien: DECIMAL(18,2)"], [], 970, 300, 220, fill="#dae8fc", stroke="#6c8ebf")
    t_hd = b.add_class("HOA_DON", ["PK  MaHoaDon: INT", "FK  MaPhieuDat: INT", "    NgayThanhToan: DATETIME", "    TienPhong: DECIMAL(18,2)", "    TienDichVu: DECIMAL(18,2)", "    TienGiamGia: DECIMAL(18,2)", "    TongTien: DECIMAL(18,2)"], [], 650, 580, 230, fill="#dae8fc", stroke="#6c8ebf")
    
    b.add_relation(t_lp, t_p, "1 - n")
    b.add_relation(t_p, t_ptn, "1 - n")
    b.add_relation(t_tn, t_ptn, "1 - n")
    b.add_relation(t_kh, t_pdp, "1 - n")
    b.add_relation(t_p, t_pdp, "1 - n")
    b.add_relation(t_pdp, t_ctdv, "1 - n")
    b.add_relation(t_dv, t_ctdv, "1 - n")
    b.add_relation(t_pdp, t_hd, "1 - 1")
    make_file(r"IMAGES\LAB3\LAB3_ERD_QuanLyKhachSan.drawio", b.build_xml())

# ==============================================================================
# LAB 4: HỆ THỐNG e-SHOPPING
# ==============================================================================
def gen_lab4():
    # 1. Use Case tổng quát e-Shopping
    b = DrawIOBuilder("LAB4_UseCase_TongQuat")
    b.add_boundary("HỆ THỐNG e-SHOPPING ABC", 200, 40, 720, 620)
    a_cus = b.add_actor("Khách hàng\n(Customer)", 60, 220)
    a_adm = b.add_actor("Quản trị viên\n(Admin)", 980, 300)
    
    uc1 = b.add_usecase("Xem danh mục & Tìm sản phẩm", 260, 70)
    uc2 = b.add_usecase("Quản lý giỏ hàng", 260, 150)
    uc3 = b.add_usecase("Đặt hàng & Checkout", 260, 230)
    uc4 = b.add_usecase("Thanh toán thẻ tín dụng", 260, 310)
    uc5 = b.add_usecase("Theo dõi trạng thái đơn hàng", 260, 390)
    uc6 = b.add_usecase("Đăng ký & Đăng nhập", 260, 470)
    uc7 = b.add_usecase("Đánh giá sản phẩm", 260, 550)
    
    uc8 = b.add_usecase("Quản lý sản phẩm & Tồn kho", 660, 150)
    uc9 = b.add_usecase("Xử lý & Cập nhật đơn hàng", 660, 270)
    uc10 = b.add_usecase("Báo cáo doanh số bán hàng", 660, 390)
    uc11 = b.add_usecase("Quản lý chính sách khuyến mãi", 660, 510)
    
    for uc in [uc1, uc2, uc3, uc4, uc5, uc6, uc7]:
        b.add_relation(a_cus, uc)
    for uc in [uc8, uc9, uc10, uc11]:
        b.add_relation(a_adm, uc)
    b.add_relation(uc3, uc4, "", "include")
    b.add_relation(uc3, uc2, "", "include")
    make_file(r"IMAGES\LAB4\LAB4_UseCase_TongQuat.drawio", b.build_xml())

    # 2. Activity Diagram Luồng tính phí vận chuyển và checkout
    b = DrawIOBuilder("LAB4_Activity_Checkout_Shipping")
    st = b.add_initial_node(380, 20)
    a1 = b.add_activity("Khách bấm Thanh toán (Checkout) từ giỏ hàng", 240, 70)
    a2 = b.add_activity("Kiểm tra tồn kho từng sản phẩm trong giỏ", 250, 150)
    d1 = b.add_decision("Đủ hàng?", 340, 230)
    a_out = b.add_activity("Báo hết hàng & đề xuất chỉnh sửa giỏ hàng", 100, 310, fill="#f8cecc", stroke="#b85450")
    a3 = b.add_activity("Nhập thông tin giao hàng & chọn phương thức", 420, 310)
    d2 = b.add_decision("Tổng tiền >= $50?", 450, 390)
    a_free = b.add_activity("Áp dụng Miễn phí giao hàng ($0)", 320, 470, fill="#d5e8d4", stroke="#82b366")
    a_fee = b.add_activity("Cộng phí giao hàng tiêu chuẩn ($5)", 560, 470)
    a4 = b.add_activity("Xác nhận thanh toán thẻ tín dụng qua Payment Gateway", 380, 560)
    d3 = b.add_decision("Thẻ hợp lệ?", 450, 640)
    a_fail = b.add_activity("Báo lỗi thanh toán & yêu cầu thử lại", 200, 710, fill="#f8cecc", stroke="#b85450")
    a5 = b.add_activity("Tạo đơn hàng, trừ tồn kho & gửi email xác nhận", 480, 710)
    en = b.add_final_node(400, 810)
    
    b.add_relation(st, a1, "", "arrow")
    b.add_relation(a1, a2, "", "arrow")
    b.add_relation(a2, d1, "", "arrow")
    b.add_relation(d1, a_out, "[Hết hàng]", "arrow")
    b.add_relation(d1, a3, "[Còn hàng]", "arrow")
    b.add_relation(a3, d2, "", "arrow")
    b.add_relation(d2, a_free, "[Đạt chuẩn]", "arrow")
    b.add_relation(d2, a_fee, "[Dưới $50]", "arrow")
    b.add_relation(a_free, a4, "", "arrow")
    b.add_relation(a_fee, a4, "", "arrow")
    b.add_relation(a4, d3, "", "arrow")
    b.add_relation(d3, a_fail, "[Thất bại]", "arrow")
    b.add_relation(d3, a5, "[Thành công]", "arrow")
    b.add_relation(a_out, en, "", "arrow")
    b.add_relation(a_fail, en, "", "arrow")
    b.add_relation(a5, en, "", "arrow")
    make_file(r"IMAGES\LAB4\LAB4_Activity_Checkout.drawio", b.build_xml())

    # 3. Class Diagram Analysis
    b = DrawIOBuilder("LAB4_Analysis_Class_Diagram", 1600, 950)
    c_cat = b.add_class("NhomSanPham", ["- maNhom: String", "- tenNhom: String", "- moTa: String"], ["+ themNhom()", "+ suaNhom()"], 60, 60, 200)
    c_prod = b.add_class("SanPham", ["- maSP: String", "- tenSP: String", "- giaBan: double", "- soLuongTon: int", "- hinhAnh: String"], ["+ giamTon()", "+ tangTon()", "+ xemChiTiet()"], 350, 60, 220)
    c_cart = b.add_class("GioHang", ["- maGioHang: String", "- ngayTao: Date"], ["+ themVaoGio()", "+ capNhatSoLuong()", "+ xoaKhoiGio()", "+ tinhTongTien()"], 670, 60, 210)
    c_cus = b.add_class("KhachHang", ["- maKH: String", "- hoTen: String", "- email: String", "- diaChi: String", "- soDT: String"], ["+ dangKy()", "+ dangNhap()", "+ capNhatHoSo()"], 980, 60, 210)
    
    c_order = b.add_class("DonHang", ["- maDonHang: String", "- ngayDat: Date", "- tongTien: double", "- phiVanChuyen: double", "- trangThai: String"], ["+ taoDonHang()", "+ huyDonHang()", "+ capNhatTrangThai()"], 670, 360, 220)
    c_item = b.add_class("ChiTietDonHang", ["- soLuong: int", "- donGia: double"], ["+ tinhThanhTien()"], 350, 360, 190)
    c_pay = b.add_class("ThanhToan", ["- maGiaoDich: String", "- phuongThuc: String", "- soTien: double", "- ngayThanhToan: Date", "- trangThai: String"], ["+ xuLyThanhToan()", "+ hoanTien()"], 980, 360, 220)
    c_ship = b.add_class("GiaoHang", ["- maVanDon: String", "- donViVanChuyen: String", "- ngayGiaoDuKien: Date", "- trangThaiGiao: String"], ["+ taoVanDon()", "+ capNhatViTri()"], 670, 620, 220)
    
    b.add_relation(c_cat, c_prod, "1..*")
    b.add_relation(c_cus, c_cart, "1..1")
    b.add_relation(c_cart, c_order, "1..*")
    b.add_relation(c_order, c_item, "1..*")
    b.add_relation(c_item, c_prod, "*..1")
    b.add_relation(c_order, c_pay, "1..1")
    b.add_relation(c_order, c_ship, "1..1")
    make_file(r"IMAGES\LAB4\LAB4_Analysis_Class_Diagram.drawio", b.build_xml())

# ==============================================================================
# LAB 5: HỆ THỐNG QUẢN LÝ CÔNG TY DU LỊCH
# ==============================================================================
def gen_lab5():
    # 1. Use Case tổng quát Du lịch
    b = DrawIOBuilder("LAB5_UseCase_QuanLyDuLich")
    b.add_boundary("HỆ THỐNG QUẢN LÝ CÔNG TY DU LỊCH (VĂN HÓA VIỆT)", 220, 40, 680, 580)
    a_kh = b.add_actor("Khách hàng / Đại diện đoàn", 60, 200)
    a_nv = b.add_actor("Nhân viên điều hành", 960, 220)
    a_hdv = b.add_actor("Hướng dẫn viên", 960, 450)
    
    uc1 = b.add_usecase("Tra cứu thông tin Tour", 270, 70)
    uc2 = b.add_usecase("Đăng ký Tour theo đoàn", 270, 160)
    uc3 = b.add_usecase("Đăng ký Tour theo chuyến lẻ", 270, 250)
    uc4 = b.add_usecase("Thanh toán tiền Tour (Đặt cọc/đủ)", 270, 340)
    uc5 = b.add_usecase("Gửi đánh giá & Khảo sát", 270, 430)
    
    uc6 = b.add_usecase("Quản lý Danh mục Tour & Lịch trình", 650, 120)
    uc7 = b.add_usecase("Tổ chức chuyến đi & Phân công HDV", 650, 220)
    uc8 = b.add_usecase("Quản lý Thu - Chi & Công nợ Tour", 650, 320)
    uc9 = b.add_usecase("Tiếp nhận phản hồi khảo sát", 650, 420)
    uc10 = b.add_usecase("Nhận lịch dẫn tour & Báo cáo", 650, 510)
    
    for uc in [uc1, uc2, uc3, uc4, uc5]:
        b.add_relation(a_kh, uc)
    for uc in [uc6, uc7, uc8, uc9]:
        b.add_relation(a_nv, uc)
    b.add_relation(a_hdv, uc10)
    make_file(r"IMAGES\LAB5\LAB5_UseCase_QuanLyDuLich.drawio", b.build_xml())

    # 2. Activity Diagram Đăng ký tour
    b = DrawIOBuilder("LAB5_Activity_DangKyTour")
    st = b.add_initial_node(380, 20)
    a1 = b.add_activity("Khách hàng chọn Tour & ngày khởi hành", 250, 70)
    a2 = b.add_activity("Nhân viên kiểm tra số chỗ còn trống", 260, 150)
    d1 = b.add_decision("Còn chỗ?", 340, 230)
    a_full = b.add_activity("Thông báo hết chỗ & tư vấn tour khác", 120, 310, fill="#f8cecc", stroke="#b85450")
    a3 = b.add_activity("Lập Phiếu Đăng Ký (Ghi nhận danh sách đoàn)", 420, 310)
    a4 = b.add_activity("Tính toán tổng chi phí & yêu cầu đặt cọc (>= 30%)", 390, 400)
    a5 = b.add_activity("Khách hàng thanh toán đặt cọc", 420, 480)
    a6 = b.add_activity("Hệ thống xuất biên nhận & chốt giữ chỗ", 410, 560)
    en = b.add_final_node(380, 650)
    
    b.add_relation(st, a1, "", "arrow")
    b.add_relation(a1, a2, "", "arrow")
    b.add_relation(a2, d1, "", "arrow")
    b.add_relation(d1, a_full, "[Hết chỗ]", "arrow")
    b.add_relation(d1, a3, "[Còn chỗ]", "arrow")
    b.add_relation(a3, a4, "", "arrow")
    b.add_relation(a4, a5, "", "arrow")
    b.add_relation(a5, a6, "", "arrow")
    b.add_relation(a_full, en, "", "arrow")
    b.add_relation(a6, en, "", "arrow")
    make_file(r"IMAGES\LAB5\LAB5_Activity_DangKyTour.drawio", b.build_xml())

# ==============================================================================
# LAB 7: HỆ THỐNG QUẢN LÝ NHÂN SỰ ĐÔNG Á
# ==============================================================================
def gen_lab7():
    # 1. Use Case tổng quát
    b = DrawIOBuilder("LAB7_UseCase_QuanLyNhanSu")
    b.add_boundary("HỆ THỐNG QUẢN LÝ NHÂN SỰ CÔNG TY SƠN ĐÔNG Á", 220, 40, 680, 600)
    a_cbns = b.add_actor("Cán bộ\nnhân sự", 60, 200)
    a_gd = b.add_actor("Giám đốc", 960, 180)
    a_kt = b.add_actor("Kế toán", 960, 420)
    a_bcc = b.add_actor("Ban chấm công", 60, 440)
    
    uc1 = b.add_usecase("Quản lý hồ sơ nhân viên", 260, 70)
    uc2 = b.add_usecase("Lập hợp đồng thử việc & chính thức", 260, 150)
    uc3 = b.add_usecase("Lập quyết định bổ nhiệm / điều chuyển", 260, 230)
    uc4 = b.add_usecase("Ghi nhận chấm công & phụ trội", 260, 320)
    uc5 = b.add_usecase("Tính và lập Bảng lương hàng tháng", 260, 410)
    uc6 = b.add_usecase("Đề xuất Khen thưởng & Kỷ luật", 260, 500)
    
    uc7 = b.add_usecase("Ký duyệt quyết định khen thưởng / kỷ luật", 650, 150)
    uc8 = b.add_usecase("Ký duyệt hợp đồng lao động", 650, 240)
    uc9 = b.add_usecase("Lập phiếu chi khen thưởng & Lương", 650, 380)
    uc10 = b.add_usecase("Quản lý chế độ bảo hiểm xã hội", 650, 490)
    
    for uc in [uc1, uc2, uc3, uc6]:
        b.add_relation(a_cbns, uc)
    b.add_relation(a_bcc, uc4)
    b.add_relation(a_gd, uc7)
    b.add_relation(a_gd, uc8)
    for uc in [uc5, uc9, uc10]:
        b.add_relation(a_kt, uc)
    make_file(r"IMAGES\LAB7\LAB7_UseCase_QuanLyNhanSu.drawio", b.build_xml())

    # 2. State Diagram Trạng thái Nhân viên
    b = DrawIOBuilder("LAB7_State_NhanVien")
    st = b.add_initial_node(100, 160)
    s1 = b.add_state("Ứng viên trúng tuyển", 160, 145)
    s2 = b.add_state("Đang thử việc", 360, 145)
    s3 = b.add_state("Nhân viên chính thức", 580, 145)
    s4 = b.add_state("Tạm hoãn / Nghỉ thai sản", 580, 280)
    s5 = b.add_state("Đã thôi việc / Chấm dứt HĐ", 840, 145, fill="#f8cecc", stroke="#b85450")
    en = b.add_final_node(1050, 160)
    
    b.add_relation(st, s1, "", "arrow")
    b.add_relation(s1, s2, "Ký HĐ thử việc", "arrow")
    b.add_relation(s2, s3, "Đạt đánh giá thử việc & Ký HĐ chính thức", "arrow")
    b.add_relation(s2, s5, "Không đạt thử việc", "arrow")
    b.add_relation(s3, s4, "Nghỉ phép dài hạn", "arrow")
    b.add_relation(s4, s3, "Hết hạn nghỉ phép", "arrow")
    b.add_relation(s3, s5, "Hết hạn HĐ / Kỷ luật sa thải / Xin nghỉ", "arrow")
    b.add_relation(s5, en, "", "arrow")
    make_file(r"IMAGES\LAB7\LAB7_State_NhanVien.drawio", b.build_xml())

# ==============================================================================
# LAB 8: HỆ THỐNG CỬA HÀNG NƯỚC GIẢI KHÁT (STARUML SPEC)
# ==============================================================================
def gen_lab8():
    # 1. Use Case tổng quát
    b = DrawIOBuilder("LAB8_Hinh2_1_UseCase_TongQuat")
    b.add_boundary("CỬA HÀNG NƯỚC GIẢI KHÁT", 220, 40, 680, 560)
    a_kh = b.add_actor("Khách hàng", 60, 160)
    a_kq = b.add_actor("Khách quen", 60, 310)
    a_kv = b.add_actor("Khách vãng lai", 60, 440)
    b.add_relation(a_kq, a_kh, "", "inherit")
    b.add_relation(a_kv, a_kh, "", "inherit")
    
    a_nv = b.add_actor("Nhân viên", 960, 220)
    a_ncu = b.add_actor("Nhà cung ứng", 960, 420)
    
    uc1 = b.add_usecase("Đặt mua nước giải khát", 270, 90)
    uc2 = b.add_usecase("Bán hàng giao ngay", 270, 180)
    uc3 = b.add_usecase("Hẹn giao hàng nhiều đợt", 270, 280)
    uc4 = b.add_usecase("Ghi nợ tiền hàng", 270, 380)
    
    uc5 = b.add_usecase("Quản lý Đơn đặt hàng (nhập)", 650, 200)
    uc6 = b.add_usecase("Tiếp nhận Phiếu giao hàng", 650, 320)
    uc7 = b.add_usecase("Tra cứu & Thống kê", 650, 440)
    
    b.add_relation(a_kh, uc1)
    b.add_relation(a_kh, uc2)
    b.add_relation(a_kq, uc3)
    b.add_relation(a_kq, uc4)
    b.add_relation(a_nv, uc2)
    b.add_relation(a_nv, uc3)
    b.add_relation(a_nv, uc4)
    b.add_relation(a_nv, uc5)
    b.add_relation(a_nv, uc6)
    b.add_relation(a_nv, uc7)
    b.add_relation(a_ncu, uc5)
    b.add_relation(a_ncu, uc6)
    make_file(r"IMAGES\LAB8\LAB8_Hinh2_1_UseCase_TongQuat.drawio", b.build_xml())

    # 2. State Diagram Hóa đơn
    b = DrawIOBuilder("LAB8_Hinh2_6_State_HoaDon")
    st = b.add_initial_node(80, 120)
    s1 = b.add_state("Khởi tạo / Chờ xuất hàng", 140, 105)
    s2 = b.add_state("Đã giao hàng đủ", 360, 105)
    s3 = b.add_state("Giao một phần (Có hẹn)", 360, 240)
    s4 = b.add_state("Đã thanh toán đủ", 620, 105, fill="#d5e8d4", stroke="#82b366")
    s5 = b.add_state("Ghi nợ (Chờ trả)", 620, 240, fill="#fff2cc", stroke="#d6b656")
    s6 = b.add_state("Đã hủy hóa đơn", 360, 360, fill="#f8cecc", stroke="#b85450")
    en = b.add_final_node(860, 120)
    
    b.add_relation(st, s1, "", "arrow")
    b.add_relation(s1, s2, "Đủ hàng kho", "arrow")
    b.add_relation(s1, s3, "Thiếu hàng -> Lập phiếu hẹn", "arrow")
    b.add_relation(s2, s4, "Thu tiền ngay", "arrow")
    b.add_relation(s2, s5, "Lập phiếu ghi nợ", "arrow")
    b.add_relation(s3, s2, "Giao đủ các đợt hẹn", "arrow")
    b.add_relation(s5, s4, "Khách trả hết nợ", "arrow")
    b.add_relation(s1, s6, "Khách hủy mua", "arrow")
    b.add_relation(s4, en, "", "arrow")
    make_file(r"IMAGES\LAB8\LAB8_Hinh2_6_State_HoaDon.drawio", b.build_xml())

    # 3. ERD Cửa hàng nước giải khát
    b = DrawIOBuilder("LAB8_Hinh3_3_ERD", 1600, 1000)
    t_nh = b.add_class("NHAN_HIEU", ["PK  MaNhanHieu: VARCHAR(10)", "    TenNhanHieu: NVARCHAR(100)", "    XuatXu: NVARCHAR(50)"], [], 60, 60, 210, fill="#dae8fc", stroke="#6c8ebf")
    t_ngk = b.add_class("NUOC_GIAI_KHAT", ["PK  MaNGK: VARCHAR(10)", "FK  MaNhanHieu: VARCHAR(10)", "FK  MaNCU: VARCHAR(10)", "    TenNGK: NVARCHAR(100)", "    DVT: NVARCHAR(20)", "    SoLuongTon: INT", "    DonGiaBan: DECIMAL(18,2)"], [], 350, 60, 230, fill="#dae8fc", stroke="#6c8ebf")
    t_ncu = b.add_class("NHA_CUNG_UNG", ["PK  MaNCU: VARCHAR(10)", "    TenNCU: NVARCHAR(100)", "    DiaChi: NVARCHAR(200)", "    SoDT: VARCHAR(15)"], [], 670, 60, 210, fill="#dae8fc", stroke="#6c8ebf")
    t_nv = b.add_class("NHAN_VIEN", ["PK  MaNV: VARCHAR(10)", "    HoTen: NVARCHAR(100)", "    ChucVu: NVARCHAR(50)"], [], 970, 60, 190, fill="#dae8fc", stroke="#6c8ebf")
    
    t_ddh = b.add_class("DON_DAT_HANG", ["PK  SoDDH: VARCHAR(15)", "FK  MaNCU: VARCHAR(10)", "FK  MaNV: VARCHAR(10)", "    NgayDat: DATE", "    NgayNhan: DATE"], [], 670, 360, 210, fill="#dae8fc", stroke="#6c8ebf")
    t_ctddh = b.add_class("CHI_TIET_DDH", ["PK,FK SoDDH: VARCHAR(15)", "PK,FK MaNGK: VARCHAR(10)", "      SoLuongDat: INT", "      DonGiaDat: DECIMAL(18,2)"], [], 350, 360, 220, fill="#dae8fc", stroke="#6c8ebf")
    t_pgh = b.add_class("PHIEU_GIAO_HANG", ["PK  SoPGH: VARCHAR(15)", "FK  SoDDH: VARCHAR(15)", "    NgayGiao: DATE", "    DotGiao: INT", "    TongTienGiao: DECIMAL(18,2)"], [], 670, 640, 220, fill="#dae8fc", stroke="#6c8ebf")
    
    t_kh = b.add_class("KHACH_HANG", ["PK  MaKH: VARCHAR(10)", "    TenKH: NVARCHAR(100)", "    DiaChi: NVARCHAR(200)", "    SoDT: VARCHAR(15)", "    LaKhachQuen: BIT"], [], 970, 360, 200, fill="#dae8fc", stroke="#6c8ebf")
    t_hd = b.add_class("HOA_DON", ["PK  SoHD: VARCHAR(15)", "FK  MaKH: VARCHAR(10)", "FK  MaNV: VARCHAR(10)", "    NgayLap: DATE", "    TongTien: DECIMAL(18,2)"], [], 1230, 360, 200, fill="#dae8fc", stroke="#6c8ebf")
    t_cthd = b.add_class("CHI_TIET_HD", ["PK,FK SoHD: VARCHAR(15)", "PK,FK MaNGK: VARCHAR(10)", "      SoLuongMua: INT", "      DonGiaBan: DECIMAL(18,2)"], [], 1230, 100, 210, fill="#dae8fc", stroke="#6c8ebf")
    t_ph = b.add_class("PHIEU_HEN", ["PK  SoPH: VARCHAR(15)", "FK  SoHD: VARCHAR(15)", "    NgayHenGiao: DATE", "    DotHen: INT"], [], 1230, 640, 200, fill="#dae8fc", stroke="#6c8ebf")
    t_no = b.add_class("PHIEU_GHI_NO", ["PK  SoPGN: VARCHAR(15)", "FK  SoHD: VARCHAR(15)", "FK  MaNV: VARCHAR(10)", "    NgayNo: DATE", "    SoTienNo: DECIMAL(18,2)", "    NgayHenTra: DATE", "    LyDoNo: NVARCHAR(200)"], [], 970, 640, 220, fill="#dae8fc", stroke="#6c8ebf")
    
    b.add_relation(t_nh, t_ngk, "1 - n")
    b.add_relation(t_ncu, t_ngk, "1 - n")
    b.add_relation(t_ncu, t_ddh, "1 - n")
    b.add_relation(t_ddh, t_ctddh, "1 - n")
    b.add_relation(t_ngk, t_ctddh, "1 - n")
    b.add_relation(t_ddh, t_pgh, "1 - n (max 3)")
    b.add_relation(t_kh, t_hd, "1 - n")
    b.add_relation(t_nv, t_hd, "1 - n")
    b.add_relation(t_hd, t_cthd, "1 - n")
    b.add_relation(t_ngk, t_cthd, "1 - n")
    b.add_relation(t_hd, t_ph, "1 - n")
    b.add_relation(t_hd, t_no, "1 - 1")
    make_file(r"IMAGES\LAB8\LAB8_Hinh3_3_ERD.drawio", b.build_xml())

if __name__ == '__main__':
    gen_lab1()
    gen_lab2()
    gen_lab3()
    gen_lab4()
    gen_lab5()
    gen_lab7()
    gen_lab8()
    print("ALL DRAW.IO DIAGRAMS GENERATED SUCCESSFULLY!")
