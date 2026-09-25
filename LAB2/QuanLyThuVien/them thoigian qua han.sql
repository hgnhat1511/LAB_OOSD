-- 1. Vá bảng PhieuMuon
IF COL_LENGTH('dbo.PhieuMuon', 'NgayHenTra') IS NULL
    ALTER TABLE dbo.PhieuMuon ADD NgayHenTra DATE NULL;

-- 2. Vá bảng ChiTietPhieuMuon
IF COL_LENGTH('dbo.ChiTietPhieuMuon', 'MaChiTiet') IS NULL
    ALTER TABLE dbo.ChiTietPhieuMuon ADD MaChiTiet VARCHAR(50) NULL;
IF COL_LENGTH('dbo.ChiTietPhieuMuon', 'NgayTraThucTe') IS NULL
    ALTER TABLE dbo.ChiTietPhieuMuon ADD NgayTraThucTe DATE NULL;
IF COL_LENGTH('dbo.ChiTietPhieuMuon', 'TinhTrangTra') IS NULL
    ALTER TABLE dbo.ChiTietPhieuMuon ADD TinhTrangTra NVARCHAR(50) NULL;

-- 3. Vá bảng PhieuPhat
IF COL_LENGTH('dbo.PhieuPhat', 'MaChiTiet') IS NULL
    ALTER TABLE dbo.PhieuPhat ADD MaChiTiet VARCHAR(50) NULL;
IF COL_LENGTH('dbo.PhieuPhat', 'MaNhanVien') IS NULL
    ALTER TABLE dbo.PhieuPhat ADD MaNhanVien VARCHAR(50) NULL;
IF COL_LENGTH('dbo.PhieuPhat', 'NgayPhat') IS NULL
    ALTER TABLE dbo.PhieuPhat ADD NgayPhat DATE NULL;
IF COL_LENGTH('dbo.PhieuPhat', 'PhiPhat') IS NULL
    ALTER TABLE dbo.PhieuPhat ADD PhiPhat DECIMAL(18,0) NULL;
IF COL_LENGTH('dbo.PhieuPhat', 'LyDo') IS NULL
    ALTER TABLE dbo.PhieuPhat ADD LyDo NVARCHAR(255) NULL;