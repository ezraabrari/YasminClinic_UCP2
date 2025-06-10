CREATE DATABASE YasminClinic2;

-- 1. TABEL ADMIN
CREATE TABLE Admin (
    Username VARCHAR(50) PRIMARY KEY NOT NULL,
    Password VARCHAR(255) NOT NULL CHECK (
        Password LIKE '%[A-Z]%' AND
        Password LIKE '%[a-z]%' AND
        Password LIKE '%[0-9]%'
    )
);


drop table Dokter

-- 2. TABEL DOKTER
CREATE TABLE Dokter (
    DokterID INT PRIMARY KEY IDENTITY(1,1),
    Nama VARCHAR(100) NOT NULL CHECK (Nama NOT LIKE '%[^a-zA-Z ]%'),
    Spesialisasi VARCHAR(50) NOT NULL 
        CHECK (Spesialisasi IN (
            'Umum', 
            'Gigi', 
            'Anak', 
            'Kandungan', 
            'Gizi'
        )),
    NoHP VARCHAR(20) NOT NULL CHECK (
        LEFT(NoHP,2) = '08' AND LEN(NoHP) <= 15 AND NoHP NOT LIKE '%[^0-9]%'
    ),
    Username VARCHAR(50) NOT NULL UNIQUE,
    Email VARCHAR(100) NOT NULL CHECK (Email LIKE '%@%'),
    Password VARCHAR(255) NOT NULL CHECK (
        Password LIKE '%[A-Z]%' AND
        Password LIKE '%[a-z]%' AND
        Password LIKE '%[0-9]%'
    )
);



-- 3. TABEL RESEPSIONIS
CREATE TABLE Resepsionis (
    ResepsionisID INT PRIMARY KEY IDENTITY(1,1),
    Nama VARCHAR(100) NOT NULL CHECK (Nama NOT LIKE '%[^a-zA-Z ]%'),
    Username VARCHAR(50) NOT NULL UNIQUE,
    Email VARCHAR(100) NOT NULL CHECK (Email LIKE '%@%'),
    Password VARCHAR(255) NOT NULL CHECK (
        Password LIKE '%[A-Z]%' AND
        Password LIKE '%[a-z]%' AND
        Password LIKE '%[0-9]%'
    )
);


-- 4. TABEL PASIEN
CREATE TABLE Pasien (
    PasienID INT PRIMARY KEY IDENTITY(1,1),
    Nama VARCHAR(100) NOT NULL CHECK (Nama NOT LIKE '%[^a-zA-Z ]%'),
    TanggalLahir DATE NOT NULL CHECK (
        TanggalLahir >= '1900-01-01' AND TanggalLahir <= GETDATE()
    ),
    JenisKelamin VARCHAR(10) CHECK (JenisKelamin IN ('Laki-laki', 'Perempuan')),
    Alamat TEXT,
    NoHP VARCHAR(20) NOT NULL CHECK (
        LEFT(NoHP,2) = '08' AND LEN(NoHP) <= 15 AND NoHP NOT LIKE '%[^0-9]%'
    )
);


-- 5. TABEL JADWAL DOKTER
CREATE TABLE JadwalDokter (
    JadwalID INT PRIMARY KEY IDENTITY(1,1),
    DokterID INT NOT NULL,
    Hari VARCHAR(10) NOT NULL CHECK (Hari IN ('Senin', 'Selasa', 'Rabu', 'Kamis', 'Jumat')),
    JamMulai TIME NOT NULL,
    JamSelesai TIME NOT NULL,
    CONSTRAINT CK_JamValid CHECK (JamMulai < JamSelesai),
    FOREIGN KEY (DokterID) REFERENCES Dokter(DokterID) ON DELETE CASCADE
);

Drop table RekamMedis
-- 6. TABEL RESERVASI
CREATE TABLE Reservasi (
    ReservasiID INT PRIMARY KEY IDENTITY(1,1),
    PasienID INT NOT NULL,
    DokterID INT NOT NULL,
    TanggalReservasi DATE NOT NULL CHECK (TanggalReservasi >= CAST(GETDATE() AS DATE)),
    JamReservasi TIME NOT NULL,
    Status VARCHAR(20) NOT NULL DEFAULT 'Terjadwal'
        CHECK (Status IN ('Terjadwal', 'Selesai', 'Batal')),
    ResepsionisID INT NOT NULL,
    FOREIGN KEY (PasienID) REFERENCES Pasien(PasienID) ON DELETE CASCADE,
    FOREIGN KEY (DokterID) REFERENCES Dokter(DokterID) ON DELETE CASCADE,
    FOREIGN KEY (ResepsionisID) REFERENCES Resepsionis(ResepsionisID) ON DELETE CASCADE
);


-- 7. TABEL REKAM MEDIS
CREATE TABLE RekamMedis (
    RekamMedisID INT PRIMARY KEY IDENTITY(1,1),
    PasienID INT NOT NULL,
    DokterID INT NOT NULL,
    Tanggal DATE NOT NULL CHECK (Tanggal <= GETDATE()),
    Keluhan TEXT,
    Diagnosa TEXT,
    Tindakan TEXT,
    Resep TEXT,
    FOREIGN KEY (PasienID) REFERENCES Pasien(PasienID) ON DELETE CASCADE,
    FOREIGN KEY (DokterID) REFERENCES Dokter(DokterID) ON DELETE CASCADE
);



Select * From RekamMedis

-- Insert 2 dokter spesialis Umum
INSERT INTO Dokter (Nama, Spesialisasi, NoHP, Username, Email, Password) VALUES
('Ahmad Sulaiman', 'Umum', '081234567890', 'ahmad.s', 'ahmad.s@example.com', 'Pass1234'),
('Nina Yusuf', 'Umum', '081234567891', 'nina.y', 'nina.y@example.com', 'Pass1234'),

-- 2 dokter spesialis Gigi
('Siti Rahmawati', 'Gigi', '082345678901', 'siti.r', 'siti.r@example.com', 'Secure2025'),
('Agus Santoso', 'Gigi', '082345678902', 'agus.s', 'agus.s@example.com', 'Secure2025'),

-- 2 dokter spesialis Anak
('Budi Santoso', 'Anak', '083456789012', 'budi.s', 'budi.s@example.com', 'Anak12345'),
('Lina Marlina', 'Anak', '083456789013', 'lina.m', 'lina.m@example.com', 'Anak12345'),

-- 2 dokter spesialis Kandungan
('Dewi Kartika', 'Kandungan', '084567890123', 'dewi.k', 'dewi.k@example.com', 'Kandungan99'),
('Rina Sari', 'Kandungan', '084567890124', 'rina.s', 'rina.s@example.com', 'Kandungan99'),

-- 2 dokter spesialis Gizi
('Dewi Hartati', 'Gizi', '085678901234', 'dewi.h', 'dewi.h@example.com', 'Gizi2023'),
('Rina Wulandari', 'Gizi', '085678901235', 'rina.w', 'rina.w@example.com', 'Gizi2023');

SELECT * from Dokter;

-- Cek DokterID
SELECT DokterID, Nama, Spesialisasi FROM Dokter;



-- Dokter Umum: ID 1 & 2 (Senin - Jumat)
INSERT INTO JadwalDokter (DokterID, Hari, JamMulai, JamSelesai) VALUES
-- DokterID 1
(1, 'Senin', '08:00', '10:00'),
(1, 'Selasa', '08:00', '10:00'),
(1, 'Rabu', '08:00', '10:00'),
(1, 'Kamis', '08:00', '10:00'),
(1, 'Jumat', '08:00', '10:00'),

-- DokterID 2
(2, 'Senin', '10:00', '12:00'),
(2, 'Selasa', '10:00', '12:00'),
(2, 'Rabu', '10:00', '12:00'),
(2, 'Kamis', '10:00', '12:00'),
(2, 'Jumat', '10:00', '12:00');

-- Dokter Gigi: ID 3 & 4 (2 hari)
INSERT INTO JadwalDokter (DokterID, Hari, JamMulai, JamSelesai) VALUES
(3, 'Senin', '13:00', '15:00'),
(3, 'Rabu', '13:00', '15:00'),
(4, 'Selasa', '15:00', '17:00'),
(4, 'Kamis', '15:00', '17:00');

Select * FROM Reservasi

-- Dokter Anak: ID 5 & 6 (2 hari)
INSERT INTO JadwalDokter (DokterID, Hari, JamMulai, JamSelesai) VALUES
(5, 'Senin', '08:00', '11:00'),
(5, 'Kamis', '08:00', '11:00'),
(6, 'Selasa', '11:00', '14:00'),
(6, 'Jumat', '11:00', '14:00');

-- Dokter Kandungan: ID 7 & 8 (2 hari)
INSERT INTO JadwalDokter (DokterID, Hari, JamMulai, JamSelesai) VALUES
(7, 'Senin', '14:00', '17:00'),
(7, 'Rabu', '14:00', '17:00'),
(8, 'Selasa', '08:00', '10:30'),
(8, 'Kamis', '08:00', '10:30');

-- Dokter Gizi: ID 9 & 10 (2 hari)
INSERT INTO JadwalDokter (DokterID, Hari, JamMulai, JamSelesai) VALUES
(9, 'Rabu', '10:30', '13:00'),
(9, 'Jumat', '10:30', '13:00'),
(10, 'Selasa', '13:00', '15:30'),
(10, 'Kamis', '13:00', '15:30');


-- Insert untuk Admin (1 data)
INSERT INTO Admin (Username, Password)
VALUES ('admin01', 'Admin123');

-- Insert untuk Resepsionis (4 data)
INSERT INTO Resepsionis (Nama, Username, Email, Password)
VALUES 
('Siti Rahma', 'srahma', 'srahma@google.com', 'Siti123'),
('Budi Santoso', 'budis', 'budis@google.com', 'Budi456'),
('Agus Wijaya', 'agusw', 'agusw@google.com', 'Agus789'),
('Dewi Lestari', 'dewiles', 'dewiles@google.com', 'Dewi321');

UPDATE Resepsionis
SET Email = REPLACE(Email, '@example.com', '@google.com')
WHERE Email LIKE '%@example.com';

select * From resepsionis

CREATE PROCEDURE sp_GetDokterTersedia
    @Tanggal DATE,
    @Jam TIME
AS
BEGIN
    SET NOCOUNT ON;

    -- Mengambil nama hari dari tanggal yang diberikan
    DECLARE @NamaHari VARCHAR(10) = FORMAT(@Tanggal, 'dddd', 'id-ID');

    -- Memilih dokter yang...
    SELECT
        d.DokterID,
        d.Nama,
        d.Spesialisasi
    FROM Dokter d
    WHERE
        -- 1. Memiliki jadwal pada hari dan jam tersebut
        EXISTS (
            SELECT 1
            FROM JadwalDokter jd
            WHERE jd.DokterID = d.DokterID
              AND jd.Hari = @NamaHari
              AND @Jam BETWEEN jd.JamMulai AND jd.JamSelesai
        )
        -- 2. Dan TIDAK memiliki reservasi pada tanggal dan jam tersebut
        AND NOT EXISTS (
            SELECT 1
            FROM Reservasi r
            WHERE r.DokterID = d.DokterID
              AND r.TanggalReservasi = @Tanggal
              AND r.JamReservasi = @Jam
              AND r.Status != 'Batal' -- Reservasi yang batal tidak dihitung
        );
END;
GO

CREATE TRIGGER trg_CekReservasiGanda
ON Reservasi
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Cek apakah ada reservasi lain pada dokter, tanggal, dan jam yang sama
    IF EXISTS (
        SELECT 1
        FROM Reservasi r
        JOIN inserted i ON r.DokterID = i.DokterID
                       AND r.TanggalReservasi = i.TanggalReservasi
                       AND r.JamReservasi = i.JamReservasi
        WHERE r.ReservasiID != i.ReservasiID AND r.Status != 'Batal'
    )
    BEGIN
        -- Jika ada, batalkan transaksi dan kirim pesan error
        ROLLBACK TRANSACTION;
        RAISERROR ('Jadwal untuk dokter ini pada tanggal dan jam tersebut sudah terisi. Reservasi gagal.', 16, 1);
        RETURN;
    END;
END;
GO

CREATE PROCEDURE sp_AddDokter
    @Nama VARCHAR(100),
    @Spesialisasi VARCHAR(50),
    @NoHP VARCHAR(20),
    @Username VARCHAR(50),
    @Email VARCHAR(100),
    @Password VARCHAR(255)
AS
BEGIN
    INSERT INTO Dokter (Nama, Spesialisasi, NoHP, Username, Email, Password)
    VALUES (@Nama, @Spesialisasi, @NoHP, @Username, @Email, @Password);
END;
GO

CREATE PROCEDURE sp_UpdateDokter
    @DokterID INT,
    @Nama VARCHAR(100),
    @Spesialisasi VARCHAR(50),
    @NoHP VARCHAR(20)
AS
BEGIN
    UPDATE Dokter
    SET Nama = @Nama, Spesialisasi = @Spesialisasi, NoHP = @NoHP
    WHERE DokterID = @DokterID;
END;
GO

CREATE PROCEDURE sp_DeleteDokter
    @DokterID INT
AS
BEGIN
    DELETE FROM Dokter WHERE DokterID = @DokterID;
END;
GO

CREATE PROCEDURE sp_GetDokterByID
    @DokterID INT
AS
BEGIN
    SELECT * FROM Dokter WHERE DokterID = @DokterID;
END;
GO


-- =================================================================================
-- STORED PROCEDURES CRUD - RESEPSIONIS
-- =================================================================================

CREATE PROCEDURE sp_AddResepsionis
    @Nama VARCHAR(100),
    @Username VARCHAR(50),
    @Email VARCHAR(100),
    @Password VARCHAR(255)
AS
BEGIN
    INSERT INTO Resepsionis (Nama, Username, Email, Password)
    VALUES (@Nama, @Username, @Email, @Password);
END;
GO

CREATE PROCEDURE sp_UpdateResepsionis
    @ResepsionisID INT,
    @Nama VARCHAR(100)
AS
BEGIN
    UPDATE Resepsionis SET Nama = @Nama WHERE ResepsionisID = @ResepsionisID;
END;
GO

CREATE PROCEDURE sp_DeleteResepsionis
    @ResepsionisID INT
AS
BEGIN
    DELETE FROM Resepsionis WHERE ResepsionisID = @ResepsionisID;
END;
GO


-- =================================================================================
-- STORED PROCEDURES CRUD - PASIEN
-- =================================================================================

CREATE PROCEDURE sp_AddPasien
    @Nama VARCHAR(100),
    @TanggalLahir DATE,
    @JenisKelamin VARCHAR(10),
    @Alamat TEXT,
    @NoHP VARCHAR(20)
AS
BEGIN
    INSERT INTO Pasien (Nama, TanggalLahir, JenisKelamin, Alamat, NoHP)
    VALUES (@Nama, @TanggalLahir, @JenisKelamin, @Alamat, @NoHP);
END;
GO

CREATE PROCEDURE sp_UpdatePasien
    @PasienID INT,
    @Nama VARCHAR(100),
    @TanggalLahir DATE,
    @JenisKelamin VARCHAR(10),
    @Alamat TEXT,
    @NoHP VARCHAR(20)
AS
BEGIN
    UPDATE Pasien
    SET Nama = @Nama, TanggalLahir = @TanggalLahir, JenisKelamin = @JenisKelamin, Alamat = @Alamat, NoHP = @NoHP
    WHERE PasienID = @PasienID;
END;
GO

CREATE PROCEDURE sp_DeletePasien
    @PasienID INT
AS
BEGIN
    DELETE FROM Pasien WHERE PasienID = @PasienID;
END;
GO

CREATE PROCEDURE sp_GetPasienByID
    @PasienID INT
AS
BEGIN
    SELECT * FROM Pasien WHERE PasienID = @PasienID;
END;
GO

-- =================================================================================
-- STORED PROCEDURES CRUD - JADWAL DOKTER
-- =================================================================================

CREATE PROCEDURE sp_AddJadwalDokter
    @DokterID INT,
    @Hari VARCHAR(10),
    @JamMulai TIME,
    @JamSelesai TIME
AS
BEGIN
    INSERT INTO JadwalDokter (DokterID, Hari, JamMulai, JamSelesai)
    VALUES (@DokterID, @Hari, @JamMulai, @JamSelesai);
END;
GO

CREATE PROCEDURE sp_DeleteJadwalDokter
    @JadwalID INT
AS
BEGIN
    DELETE FROM JadwalDokter WHERE JadwalID = @JadwalID;
END;
GO

-- =================================================================================
-- STORED PROCEDURES CRUD - RESERVASI
-- =================================================================================

CREATE PROCEDURE sp_AddReservasi
    @PasienID INT,
    @DokterID INT,
    @TanggalReservasi DATE,
    @JamReservasi TIME,
    @ResepsionisID INT
AS
BEGIN
    INSERT INTO Reservasi (PasienID, DokterID, TanggalReservasi, JamReservasi, ResepsionisID)
    VALUES (@PasienID, @DokterID, @TanggalReservasi, @JamReservasi, @ResepsionisID);
END;
GO

CREATE PROCEDURE sp_UpdateStatusReservasi
    @ReservasiID INT,
    @Status VARCHAR(20)
AS
BEGIN
    UPDATE Reservasi SET Status = @Status WHERE ReservasiID = @ReservasiID;
END;
GO


-- =================================================================================
-- STORED PROCEDURES CRUD - REKAM MEDIS
-- =================================================================================

CREATE PROCEDURE sp_AddRekamMedis
    @PasienID INT,
    @DokterID INT,
    @Tanggal DATE,
    @Keluhan TEXT,
    @Diagnosa TEXT,
    @Tindakan TEXT,
    @Resep TEXT
AS
BEGIN
    INSERT INTO RekamMedis (PasienID, DokterID, Tanggal, Keluhan, Diagnosa, Tindakan, Resep)
    VALUES (@PasienID, @DokterID, @Tanggal, @Keluhan, @Diagnosa, @Tindakan, @Resep);
END;
GO

CREATE PROCEDURE sp_GetRekamMedisByPasien
    @PasienID INT
AS
BEGIN
    SELECT rm.*, d.Nama AS NamaDokter
    FROM RekamMedis rm
    JOIN Dokter d ON rm.DokterID = d.DokterID
    WHERE rm.PasienID = @PasienID
    ORDER BY rm.Tanggal DESC;
END;
GO

GO

CREATE PROCEDURE sp_UnifiedLogin
    @Username VARCHAR(50),
    @Password VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    -- Cek tabel Admin
    IF EXISTS (SELECT 1 FROM Admin WHERE Username = @Username AND Password = @Password)
    BEGIN
        SELECT 'Admin' AS UserRole;
        RETURN;
    END

    -- Cek tabel Dokter
    IF EXISTS (SELECT 1 FROM Dokter WHERE Username = @Username AND Password = @Password)
    BEGIN
        SELECT 'Dokter' AS UserRole;
        RETURN;
    END

    -- Cek tabel Resepsionis
    IF EXISTS (SELECT 1 FROM Resepsionis WHERE Username = @Username AND Password = @Password)
    BEGIN
        SELECT 'Resepsionis' AS UserRole;
        RETURN;
    END

    -- Jika tidak ada yang cocok, kembalikan hasil kosong
    SELECT NULL AS UserRole WHERE 1=0;

END;
GO

GO

CREATE PROCEDURE sp_GetAllPasien
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        PasienID, 
        Nama, 
        TanggalLahir, 
        JenisKelamin, 
        Alamat, 
        NoHP 
    FROM Pasien 
    ORDER BY Nama;
END;
GO


-- =================================================================================
-- Stored Procedures untuk Form Reservasi
-- =================================================================================
GO

-- ---------------------------------------------------------------------------------
-- 1. SP untuk MENAMPILKAN DETAIL SEMUA RESERVASI (untuk DataGridView)
--    Menggabungkan data dari 4 tabel untuk tampilan yang informatif.
-- ---------------------------------------------------------------------------------
CREATE PROCEDURE sp_GetAllReservasiDetails
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        r.ReservasiID,
        p.Nama AS [Nama Pasien],
        d.Nama AS [Nama Dokter],
        d.Spesialisasi,
        r.TanggalReservasi,
        r.JamReservasi,
        r.Status,
        rs.Nama AS [Dibuat Oleh (Resepsionis)]
    FROM Reservasi r
    JOIN Pasien p ON r.PasienID = p.PasienID
    JOIN Dokter d ON r.DokterID = d.DokterID
    JOIN Resepsionis rs ON r.ResepsionisID = rs.ResepsionisID
    ORDER BY r.TanggalReservasi DESC, r.JamReservasi DESC;
END;
GO

-- ---------------------------------------------------------------------------------
-- 2. SP untuk MENDAPATKAN DAFTAR SPESIALISASI DOKTER
--    Digunakan untuk mengisi ComboBox Spesialisasi.
-- ---------------------------------------------------------------------------------
CREATE PROCEDURE sp_GetAllSpesialisasi
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DISTINCT Spesialisasi FROM Dokter ORDER BY Spesialisasi;
END;
GO

-- ---------------------------------------------------------------------------------
-- 3. SP untuk MENGHAPUS RESERVASI
-- ---------------------------------------------------------------------------------
CREATE PROCEDURE sp_DeleteReservasi
    @ReservasiID INT
AS
BEGIN
    DELETE FROM Reservasi WHERE ReservasiID = @ReservasiID;
END;
GO

-- =================================================================================
-- Stored Procedures BARU untuk Logika Form Reservasi Dinamis
-- =================================================================================
GO

-- ---------------------------------------------------------------------------------
-- 1. SP untuk MENCARI DOKTER BERDASARKAN SPESIALISASI
--    Digunakan untuk mengisi ComboBox Dokter setelah spesialisasi dipilih.
-- ---------------------------------------------------------------------------------
CREATE PROCEDURE sp_GetDokterBySpesialisasi
    @Spesialisasi VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DokterID, Nama FROM Dokter WHERE Spesialisasi = @Spesialisasi ORDER BY Nama;
END;
GO

-- ---------------------------------------------------------------------------------
-- 2. SP untuk MENCARI JAM TERSEDIA BERDASARKAN DOKTER DAN TANGGAL
--    Secara otomatis menghasilkan slot waktu dan menghilangkan yang sudah dipesan.
-- ---------------------------------------------------------------------------------
CREATE PROCEDURE sp_GetJamTersediaByDokter
    @DokterID INT,
    @Tanggal DATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NamaHari VARCHAR(10) = FORMAT(@Tanggal, 'dddd', 'id-ID');
    DECLARE @JamMulai TIME, @JamSelesai TIME;

    -- Ambil jadwal dokter pada hari tersebut
    SELECT @JamMulai = JamMulai, @JamSelesai = JamSelesai
    FROM JadwalDokter
    WHERE DokterID = @DokterID AND Hari = @NamaHari;

    -- Jika dokter tidak punya jadwal pada hari itu, hentikan
    IF @JamMulai IS NULL
    BEGIN
        RETURN;
    END

    -- Buat tabel sementara untuk semua slot waktu yang mungkin (interval 30 menit)
    DECLARE @AllTimeSlots TABLE (SlotWaktu TIME);
    DECLARE @CurrentTime TIME = @JamMulai;

    WHILE @CurrentTime < @JamSelesai
    BEGIN
        INSERT INTO @AllTimeSlots (SlotWaktu) VALUES (@CurrentTime);
        SET @CurrentTime = DATEADD(MINUTE, 30, @CurrentTime);
    END;

    -- Pilih hanya slot waktu yang BELUM ada di tabel Reservasi
    SELECT
        ats.SlotWaktu
    FROM @AllTimeSlots ats
    WHERE NOT EXISTS (
        SELECT 1
        FROM Reservasi r
        WHERE r.DokterID = @DokterID
          AND r.TanggalReservasi = @Tanggal
          AND r.JamReservasi = ats.SlotWaktu
          AND r.Status != 'Batal'
    );
END;
GO

CREATE PROCEDURE sp_UpdateReservasi
    @ReservasiID INT,
    @PasienID INT,
    @DokterID INT,
    @TanggalReservasi DATE,
    @JamReservasi TIME
AS
BEGIN
    UPDATE Reservasi
    SET 
        PasienID = @PasienID,
        DokterID = @DokterID,
        TanggalReservasi = @TanggalReservasi,
        JamReservasi = @JamReservasi
    WHERE ReservasiID = @ReservasiID;
END;

GO

CREATE PROCEDURE sp_GetAllRekamMedisDetails
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        rm.RekamMedisID,
        p.Nama AS [Nama Pasien],
        d.Nama AS [Nama Dokter],
        rm.Tanggal,
        rm.Keluhan,
        rm.Diagnosa,
        rm.Tindakan,
        rm.Resep
    FROM RekamMedis rm
    JOIN Pasien p ON rm.PasienID = p.PasienID
    JOIN Dokter d ON rm.DokterID = d.DokterID
    ORDER BY rm.Tanggal DESC, p.Nama;
END;
GO


-- =================================================================================
-- Stored Procedures untuk Form Rekam Medis (Alur Kerja Dokter)
-- =================================================================================
GO

-- ---------------------------------------------------------------------------------
-- 1. SP untuk MENDAPATKAN PASIEN YANG SUDAH KONSULTASI DENGAN DOKTER
--    Hanya menampilkan pasien yang status reservasinya 'Selesai' dan 
--    BELUM dibuatkan rekam medisnya.
-- ---------------------------------------------------------------------------------
CREATE PROCEDURE sp_GetPasienForRekamMedis
    @DokterID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DISTINCT 
        p.PasienID, 
        p.Nama
    FROM Pasien p
    JOIN Reservasi r ON p.PasienID = r.PasienID
    WHERE r.DokterID = @DokterID 
      AND r.Status = 'Selesai'
      -- Pastikan rekam medis untuk reservasi ini belum ada
      AND NOT EXISTS (SELECT 1 FROM RekamMedis rm WHERE rm.ReservasiID = r.ReservasiID);
END;
GO

-- ---------------------------------------------------------------------------------
-- 2. SP untuk MENDAPATKAN TANGGAL KONSULTASI PASIEN
--    Menampilkan tanggal reservasi yang statusnya 'Selesai' untuk pasangan
--    dokter dan pasien tertentu.
-- ---------------------------------------------------------------------------------
CREATE PROCEDURE sp_GetTanggalKonsultasi
    @DokterID INT,
    @PasienID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        r.ReservasiID,
        r.TanggalReservasi
    FROM Reservasi r
    WHERE r.DokterID = @DokterID 
      AND r.PasienID = @PasienID 
      AND r.Status = 'Selesai'
      AND NOT EXISTS (SELECT 1 FROM RekamMedis rm WHERE rm.ReservasiID = r.ReservasiID);
END;
GO

-- ---------------------------------------------------------------------------------
-- 3. SP untuk MENAMPILKAN SEMUA REKAM MEDIS DOKTER
--    Digunakan untuk mengisi DataGridView utama.
-- ---------------------------------------------------------------------------------
CREATE PROCEDURE sp_GetRekamMedisByDokter
    @DokterID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        rm.RekamMedisID,
        p.Nama AS [Nama Pasien],
        rm.Tanggal,
        rm.Keluhan,
        rm.Diagnosa,
        rm.Tindakan,
        rm.Resep,
        rm.PasienID -- Sertakan untuk kemudahan di sisi aplikasi
    FROM RekamMedis rm
    JOIN Pasien p ON rm.PasienID = p.PasienID
    WHERE rm.DokterID = @DokterID
    ORDER BY rm.Tanggal DESC;
END;
GO

-- ---------------------------------------------------------------------------------
-- 4. SP untuk MENGUBAH DATA REKAM MEDIS
-- ---------------------------------------------------------------------------------
CREATE PROCEDURE sp_UpdateRekamMedis
    @RekamMedisID INT,
    @Keluhan TEXT,
    @Diagnosa TEXT,
    @Tindakan TEXT,
    @Resep TEXT
AS
BEGIN
    UPDATE RekamMedis
    SET
        Keluhan = @Keluhan,
        Diagnosa = @Diagnosa,
        Tindakan = @Tindakan,
        Resep = @Resep
    WHERE RekamMedisID = @RekamMedisID;
END;
GO

-- =================================================================================
-- Stored Procedure untuk MENAMBAH Rekam Medis Baru (Diperbaiki)
-- =================================================================================
-- Versi ini sudah ditambahkan parameter @ReservasiID untuk menghubungkan
-- rekam medis dengan reservasi yang sesuai.
-- =================================================================================
GO

ALTER PROCEDURE sp_AddRekamMedis
    @PasienID INT,
    @DokterID INT,
    @Tanggal DATE,
    @Keluhan TEXT,
    @Diagnosa TEXT,
    @Tindakan TEXT,
    @Resep TEXT
AS
BEGIN
    INSERT INTO RekamMedis (PasienID, DokterID, Tanggal, Keluhan, Diagnosa, Tindakan, Resep)
    VALUES (@PasienID, @DokterID, @Tanggal, @Keluhan, @Diagnosa, @Tindakan, @Resep); -- <-- NILAI BARU DISIMPAN
END;
GO

ALTER PROCEDURE sp_GetPasienForRekamMedis
    @DokterID INT
AS
BEGIN
    SET NOCOUNT ON;
    -- Pilih Pasien yang punya reservasi 'Selesai' dengan dokter ini
    SELECT DISTINCT
        p.PasienID,
        p.Nama
    FROM Pasien p
    JOIN Reservasi r ON p.PasienID = r.PasienID
    WHERE r.DokterID = @DokterID
      AND r.Status = 'Selesai'
      -- DAN untuk reservasi tersebut, belum ada rekam medis yang cocok
      AND NOT EXISTS (
          SELECT 1 FROM RekamMedis rm
          WHERE rm.PasienID = r.PasienID
            AND rm.DokterID = r.DokterID
            AND rm.Tanggal = r.TanggalReservasi
      );
END;
GO

------------------------------------------------
ALTER PROCEDURE sp_GetTanggalKonsultasi
    @DokterID INT,
    @PasienID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DISTINCT
        r.TanggalReservasi
    FROM Reservasi r
    WHERE r.DokterID = @DokterID 
      AND r.PasienID = @PasienID 
      AND r.Status = 'Selesai'
      AND NOT EXISTS (
          SELE
-- 1. Perbaiki stored procedure untuk menampilkan semua rekam medis
IF OBJECT_ID('sp_GetAllRekamMedisDetails', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetAllRekamMedisDetails;
GO

CREATE PROCEDURE sp_GetAllRekamMedisDetails
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        rm.RekamMedisID,
        p.Nama AS [Nama Pasien],
        d.Nama AS [Nama Dokter],
        d.Spesialisasi,
        rm.Tanggal,
        rm.Keluhan,
        rm.Diagnosa,
        rm.Tindakan,
        rm.Resep
    FROM RekamMedis rm
    INNER JOIN Pasien p ON rm.PasienID = p.PasienID
    INNER JOIN Dokter d ON rm.DokterID = d.DokterID
    ORDER BY rm.Tanggal DESC, p.Nama;
END;
GO

-- 2. Perbaiki stored procedure untuk menambah rekam medis
IF OBJECT_ID('sp_AddRekamMedis', 'P') IS NOT NULL
    DROP PROCEDURE sp_AddRekamMedis;
GO

CREATE PROCEDURE sp_AddRekamMedis
    @PasienID INT,
    @DokterID INT,
    @Tanggal DATE,
    @Keluhan TEXT = NULL,
    @Diagnosa TEXT,
    @Tindakan TEXT = NULL,
    @Resep TEXT = NULL,
    @ReservasiID INT = NULL -- Parameter opsional untuk tracking reservasi
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        INSERT INTO RekamMedis (PasienID, DokterID, Tanggal, Keluhan, Diagnosa, Tindakan, Resep)
        VALUES (@PasienID, @DokterID, @Tanggal, @Keluhan, @Diagnosa, @Tindakan, @Resep);
        
        -- Jika ReservasiID disediakan, update status reservasi menjadi selesai
        IF @ReservasiID IS NOT NULL
        BEGIN
            UPDATE Reservasi 
            SET Status = 'Selesai' 
            WHERE ReservasiID = @ReservasiID;
        END
        
        SELECT SCOPE_IDENTITY() AS NewRekamMedisID; -- Return ID yang baru dibuat
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO

-- 3. Stored procedure untuk debugging - cek data
CREATE PROCEDURE sp_DebugRekamMedis
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 'Jumlah Rekam Medis' AS Info, COUNT(*) AS Jumlah FROM RekamMedis
    UNION ALL
    SELECT 'Jumlah Pasien' AS Info, COUNT(*) AS Jumlah FROM Pasien
    UNION ALL
    SELECT 'Jumlah Dokter' AS Info, COUNT(*) AS Jumlah FROM Dokter;
    
    -- Tampilkan 5 data rekam medis terakhir dengan detail
    SELECT TOP 5
        rm.RekamMedisID,
        p.Nama AS NamaPasien,
        d.Nama AS NamaDokter,
        rm.Tanggal,
        rm.Diagnosa
    FROM RekamMedis rm
    LEFT JOIN Pasien p ON rm.PasienID = p.PasienID
    LEFT JOIN Dokter d ON rm.DokterID = d.DokterID
    ORDER BY rm.RekamMedisID DESC;
END;
GO

-- 4. Perbaiki stored procedure untuk mendapatkan pasien untuk rekam medis
IF OBJECT_ID('sp_GetPasienForRekamMedis', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetPasienForRekamMedis;
GO

CREATE PROCEDURE sp_GetPasienForRekamMedis
    @DokterID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Ambil semua pasien yang punya reservasi dengan dokter ini
    -- Tidak peduli status, karena dokter mungkin perlu membuat rekam medis untuk reservasi lama
    SELECT DISTINCT
        p.PasienID,
        p.Nama
    FROM Pasien p
    INNER JOIN Reservasi r ON p.PasienID = r.PasienID
    WHERE r.DokterID = @DokterID
    ORDER BY p.Nama;
END;
GO

-- 5. Perbaiki stored procedure untuk mendapatkan tanggal konsultasi
IF OBJECT_ID('sp_GetTanggalKonsultasi', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetTanggalKonsultasi;
GO

CREATE PROCEDURE sp_GetTanggalKonsultasi
    @DokterID INT,
    @PasienID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        r.ReservasiID,
        CONVERT(VARCHAR(10), r.TanggalReservasi, 103) AS TanggalReservasi -- Format DD/MM/YYYY
    FROM Reservasi r
    WHERE r.DokterID = @DokterID 
      AND r.PasienID = @PasienID
    ORDER BY r.TanggalReservasi DESC;
END;
GO

select * From Reservasi

GO

-- 1. Index untuk Tabel Dokter
-- Mempercepat pencarian berdasarkan Username (untuk login) dan Spesialisasi (untuk filter)
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_Dokter_Username' AND object_id = OBJECT_ID('dbo.Dokter'))
BEGIN
    CREATE NONCLUSTERED INDEX idx_Dokter_Username ON dbo.Dokter(Username);
    PRINT 'Created index: idx_Dokter_Username';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_Dokter_Spesialisasi' AND object_id = OBJECT_ID('dbo.Dokter'))
BEGIN
    CREATE NONCLUSTERED INDEX idx_Dokter_Spesialisasi ON dbo.Dokter(Spesialisasi);
    PRINT 'Created index: idx_Dokter_Spesialisasi';
END
GO


-- 2. Index untuk Tabel Resepsionis
-- Mempercepat pencarian berdasarkan Username (untuk login)
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_Resepsionis_Username' AND object_id = OBJECT_ID('dbo.Resepsionis'))
BEGIN
    CREATE NONCLUSTERED INDEX idx_Resepsionis_Username ON dbo.Resepsionis(Username);
    PRINT 'Created index: idx_Resepsionis_Username';
END
GO


-- 3. Index untuk Tabel Pasien
-- Mempercepat pencarian atau pengurutan berdasarkan Nama Pasien
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_Pasien_Nama' AND object_id = OBJECT_ID('dbo.Pasien'))
BEGIN
    CREATE NONCLUSTERED INDEX idx_Pasien_Nama ON dbo.Pasien(Nama);
    PRINT 'Created index: idx_Pasien_Nama';
END
GO


-- 4. Index untuk Tabel Reservasi (SANGAT PENTING)
-- Mempercepat JOIN ke tabel Pasien dan Dokter
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_Reservasi_PasienID' AND object_id = OBJECT_ID('dbo.Reservasi'))
BEGIN
    CREATE NONCLUSTERED INDEX idx_Reservasi_PasienID ON dbo.Reservasi(PasienID);
    PRINT 'Created index: idx_Reservasi_PasienID';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_Reservasi_DokterID' AND object_id = OBJECT_ID('dbo.Reservasi'))
BEGIN
    CREATE NONCLUSTERED INDEX idx_Reservasi_DokterID ON dbo.Reservasi(DokterID);
    PRINT 'Created index: idx_Reservasi_DokterID';
END
GO

-- 5. Index untuk Tabel JadwalDokter
-- Mempercepat JOIN ke tabel Dokter saat mencari jadwal
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_JadwalDokter_DokterID' AND object_id = OBJECT_ID('dbo.JadwalDokter'))
BEGIN
    CREATE NONCLUSTERED INDEX idx_JadwalDokter_DokterID ON dbo.JadwalDokter(DokterID);
    PRINT 'Created index: idx_JadwalDokter_DokterID';
END
GO

PRINT 'Semua index yang direkomendasikan telah dibuat atau sudah ada.';

Select * From Admin

GO

CREATE PROCEDURE sp_GetAllResepsionis
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        ResepsionisID, 
        Nama, 
        Username, 
        Email
    FROM Resepsionis 
    ORDER BY Nama;
END;
GO