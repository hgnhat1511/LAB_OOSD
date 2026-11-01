-- 1. TẠO CƠ SỞ DỮ LIỆU
CREATE DATABASE QL_CH_NuocGiaiKhat;
GO

USE QL_CH_NuocGiaiKhat;
GO

-- 2. TẠO CÁC BẢNG DANH MỤC (Thực thể độc lập tạo trước)
CREATE TABLE NhaCungUng (
    MaNCU CHAR(10) PRIMARY KEY,
    TenNCU NVARCHAR(80) NOT NULL,
    DiaChi NVARCHAR(150),
    SoDienThoai VARCHAR(15) NOT NULL
);

CREATE TABLE NhanVien (
    MaNV CHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(60) NOT NULL,
    SoDienThoai VARCHAR(15),
    DiaChi NVARCHAR(150)
);

CREATE TABLE KhachHang (
    MaKH CHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(60) NOT NULL,
    DiaChi NVARCHAR(150),
    SoDienThoai VARCHAR(15),
    LoaiKhach NVARCHAR(20) CHECK (LoaiKhach IN (N'Quen', N'Vãng lai'))
);

CREATE TABLE LoaiNGK (
    MaLoai CHAR(10) PRIMARY KEY,
    TenLoai NVARCHAR(50) UNIQUE NOT NULL,
    MaNCU CHAR(10) NOT NULL,
    FOREIGN KEY (MaNCU) REFERENCES NhaCungUng(MaNCU)
);

CREATE TABLE NuocGiaiKhat (
    MaNGK CHAR(10) PRIMARY KEY,
    TenNGK NVARCHAR(80) NOT NULL,
    DonViTinh NVARCHAR(20),
    DonGiaBan DECIMAL(18,0) CHECK (DonGiaBan >= 0),
    SoLuongTon INT CHECK (SoLuongTon >= 0),
    MaLoai CHAR(10) NOT NULL,
    FOREIGN KEY (MaLoai) REFERENCES LoaiNGK(MaLoai)
);

-- 3. TẠO CÁC BẢNG NGHIỆP VỤ NHẬP HÀNG
CREATE TABLE DonDatHang (
    SoDDH CHAR(10) PRIMARY KEY,
    NgayDat DATE NOT NULL,
    NgayNhan DATE NOT NULL,
    TrangThai NVARCHAR(20),
    MaNCU CHAR(10) NOT NULL,
    MaNV CHAR(10) NOT NULL,
    CHECK (NgayNhan >= NgayDat),
    FOREIGN KEY (MaNCU) REFERENCES NhaCungUng(MaNCU),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);

CREATE TABLE ChiTietDDH (
    SoDDH CHAR(10),
    MaNGK CHAR(10),
    SoLuongDat INT CHECK (SoLuongDat > 0),
    PRIMARY KEY (SoDDH, MaNGK),
    FOREIGN KEY (SoDDH) REFERENCES DonDatHang(SoDDH),
    FOREIGN KEY (MaNGK) REFERENCES NuocGiaiKhat(MaNGK)
);

CREATE TABLE PhieuGiaoHang (
    SoPGH CHAR(10) PRIMARY KEY,
    SoDDH CHAR(10) NOT NULL,
    DotGiao TINYINT CHECK (DotGiao BETWEEN 1 AND 3),
    NgayGiao DATE NOT NULL,
    TongTien DECIMAL(18,0) CHECK (TongTien >= 0),
    UNIQUE (SoDDH, DotGiao), -- Mỗi ĐĐH không được trùng đợt giao
    FOREIGN KEY (SoDDH) REFERENCES DonDatHang(SoDDH)
);

CREATE TABLE ChiTietPGH (
    SoPGH CHAR(10),
    MaNGK CHAR(10),
    SoLuongGiao INT CHECK (SoLuongGiao > 0),
    DonGiaNhap DECIMAL(18,0) CHECK (DonGiaNhap >= 0),
    PRIMARY KEY (SoPGH, MaNGK),
    FOREIGN KEY (SoPGH) REFERENCES PhieuGiaoHang(SoPGH),
    FOREIGN KEY (MaNGK) REFERENCES NuocGiaiKhat(MaNGK)
);

-- 4. TẠO CÁC BẢNG NGHIỆP VỤ BÁN HÀNG & CÔNG NỢ
CREATE TABLE HoaDon (
    SoHD CHAR(10) PRIMARY KEY,
    NgayLap DATE NOT NULL,
    TongTien DECIMAL(18,0) CHECK (TongTien >= 0) DEFAULT 0,
    TrangThai NVARCHAR(20) CHECK (TrangThai IN (N'Đang giao', N'Đã giao đủ')),
    MaKH CHAR(10) NOT NULL,
    MaNV CHAR(10) NOT NULL,
    UNIQUE (SoHD, MaKH), -- Đích đến cho khóa ngoại ghép của PhieuGhiNo
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);

CREATE TABLE ChiTietHoaDon (
    SoHD CHAR(10),
    MaNGK CHAR(10),
    SoLuong INT CHECK (SoLuong > 0),
    DonGia DECIMAL(18,0) CHECK (DonGia >= 0),
    SoLuongDaGiao INT CHECK (SoLuongDaGiao >= 0),
    PRIMARY KEY (SoHD, MaNGK),
    CHECK (SoLuongDaGiao <= SoLuong), -- Không giao vượt quá số lượng mua
    FOREIGN KEY (SoHD) REFERENCES HoaDon(SoHD),
    FOREIGN KEY (MaNGK) REFERENCES NuocGiaiKhat(MaNGK)
);

CREATE TABLE PhieuHenGiao (
    SoPHG CHAR(10) PRIMARY KEY,
    SoHD CHAR(10) NOT NULL,
    NgayHen DATE NOT NULL,
    TrangThai NVARCHAR(20) CHECK (TrangThai IN (N'Chưa giao', N'Đã giao')),
    FOREIGN KEY (SoHD) REFERENCES HoaDon(SoHD)
);

CREATE TABLE ChiTietPhieuHen (
    SoPHG CHAR(10),
    MaNGK CHAR(10),
    SoLuongHen INT CHECK (SoLuongHen > 0),
    PRIMARY KEY (SoPHG, MaNGK),
    FOREIGN KEY (SoPHG) REFERENCES PhieuHenGiao(SoPHG),
    FOREIGN KEY (MaNGK) REFERENCES NuocGiaiKhat(MaNGK)
);

CREATE TABLE PhieuGhiNo (
    SoPGN CHAR(10) PRIMARY KEY,
    NgayLap DATE NOT NULL,
    SoHD CHAR(10) UNIQUE NOT NULL, -- Mỗi hóa đơn chỉ 1 phiếu nợ
    MaKH CHAR(10) NOT NULL,
    SoTienNo DECIMAL(18,0) CHECK (SoTienNo > 0),
    NgayDuKienTra DATE NOT NULL,
    LyDo NVARCHAR(200),
    MaNV CHAR(10) NOT NULL,
    TrangThai NVARCHAR(20) CHECK (TrangThai IN (N'Chưa trả', N'Quá hạn', N'Đã trả')),
    CHECK (NgayDuKienTra >= NgayLap),
    FOREIGN KEY (SoHD, MaKH) REFERENCES HoaDon(SoHD, MaKH),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);
GO

-- 5. THÊM DỮ LIỆU MẪU (MOCK DATA) ĐỂ TEST TRÊN C#
INSERT INTO NhaCungUng VALUES 
('NCU01', N'Công ty TNHH Suntory PepsiCo VN', N'Quận 1, TP.HCM', '0283123456'),
('NCU02', N'Công ty TNHH Nước Giải Khát Coca-Cola VN', N'Thủ Đức, TP.HCM', '0283654321');

INSERT INTO NhanVien VALUES 
('NV01', N'Nguyễn Văn Hoàng Nhật', '0901234567', N'Tân Bình, TP.HCM'),
('NV02', N'Trần Thị B', '0912345678', N'Bình Thạnh, TP.HCM');

INSERT INTO KhachHang VALUES 
('KH01', N'Lê Minh C', N'Quận 10, TP.HCM', '0923456789', N'Quen'),
('KH02', N'Phạm Văn D', N'Quận 5, TP.HCM', '0934567890', N'Vãng lai');

INSERT INTO LoaiNGK VALUES 
('L01', N'Nước ngọt có gas Pepsi', 'NCU01'),
('L02', N'Nước suối Aquafina', 'NCU01'),
('L03', N'Nước ngọt có gas Coca', 'NCU02');

INSERT INTO NuocGiaiKhat VALUES 
('NGK01', N'Pepsi chai nhựa 390ml', N'Chai', 10000, 150, 'L01'),
('NGK02', N'7Up lon 330ml', N'Lon', 12000, 5, 'L01'), -- Số lượng tồn ít để test xuất phiếu hẹn
('NGK03', N'Nước tinh khiết Aquafina 500ml', N'Chai', 5000, 200, 'L02'),
('NGK04', N'Coca Cola lon 330ml', N'Lon', 12000, 100, 'L03');

INSERT INTO DonDatHang VALUES 
('DDH01', '2026-10-25', '2026-10-28', N'Đang giao', 'NCU01', 'NV01');

INSERT INTO ChiTietDDH VALUES 
('DDH01', 'NGK02', 100);
GO