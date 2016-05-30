Create Database QLCV;
use QLCV;

Create table NGUOIDUNG
(
	ID int IDENTITY(1,1) PRIMARY KEY,
	TENDANGNHAP nvarchar(50) UNIQUE,
	MATKHAU nvarchar(MAX),
	TENNGUOIDUNG nvarchar(MAX),
	EMAIL nvarchar(MAX),
	TRANGTHAI bit,
	NGAYTAO datetime,
	IDPHONGBAN int,
	IDCHUCDANH int,
	IDCHUCVU int
)

Create table PHONGBAN
(
	ID int IDENTITY(1,1) PRIMARY KEY,
	TENPHONG nvarchar(MAX)
)

Create table CHUCVU
(
	ID int IDENTITY(1,1) PRIMARY KEY,
	TENCHUCVU nvarchar(MAX)
)

Create table CHUCDANH
(
	ID int IDENTITY(1,1) PRIMARY KEY,
	TENCHUCDANH nvarchar(MAX),
	TENVIETTAT nvarchar(MAX)
)

Create table CONGVIEC
(
	ID int IDENTITY(1,1) PRIMARY KEY,
	IDNGUOITAO int,
	TIEUDE nvarchar(MAX),
	NOIDUNG nvarchar(MAX),
	GHICHU nvarchar(MAX),
	NGAYTAO datetime,
	HOANTHANH bit, --DONG HOAC MO--
	IDDANGCONGVIEC int, --YEU CAU 1, XU LY 2, 
	NGAYCAPNHAT datetime,
	XOA bit,
	PARENTID int,
	TAPTIN nvarchar(MAX),
	THUMUC nvarchar(MAX)
)

Create table DANGCONGVIEC
(
	ID int IDENTITY(1,1) PRIMARY KEY,
	TEN nvarchar(MAX)
)

Create table DANGPHANCONG
(
	ID int IDENTITY(1,1) PRIMARY KEY,
	TEN nvarchar(MAX)
)

Create table TRANGTHAI
(
	ID int IDENTITY(1,1) PRIMARY KEY,
	TEN nvarchar(MAX)
)

Create table PHANCONG
(
	IDPHANCONG int,
	IDNGUOINHAN int,
	IDCONGVIEC int,
	TENPHANCONG nvarchar(MAX),
	IDTRANGTHAI int, --DA HOAN THANH 1, DANG XU LY 2, CHUA XU LY 3
	IDDANGPHANCONG int, --GIAM SAT 1, XU LY 2
	NOIDUNG nvarchar(MAX),
	GHICHU nvarchar(MAX),
	NGAYCAPNHAT datetime,
	NGAYBATDAU datetime,
	NGAYKETTHUC datetime,
	PRIMARY KEY(IDPHANCONG, IDNGUOINHAN, IDCONGVIEC)
)

Create table BINHLUAN
(
	ID int IDENTITY(1,1) PRIMARY KEY,
	IDNGUOITAO int,
	IDCONGVIEC int,
	NOIDUNG nvarchar(MAX),
	NGAYTAO datetime,
	NGAYCAPNHAT datetime
)

Create table BAOCAOCONGVIEC
(
	ID int IDENTITY(1,1) PRIMARY KEY,
	IDNGUOITAO int,
	IDCONGVIEC int,
	IDPHANCONG int,
	NOIDUNG nvarchar(MAX),
	NGAYTAO datetime
)

Create table GROUP
(
	ID int IDENTITY(1,1) PRIMARY KEY,
	TENGROUP nvarchar(MAX)
)

Create table GROUP_NGUOIDUNG
(
	IDGROUP int,
	IDNGUOIDUNG int,
	PRIMARY KEY(IDGROUP, IDNGUOIDUNG)
)

Create table ACTION
(
	ID int IDENTITY(1,1) PRIMARY KEY,
	ACTION nvarchar(MAX)
)

Create table GROUP_ACTION
(
	IDGROUP int,
	IDACTION int,
	PRIMARY KEY(IDGROUP, IDACTION)
)

Create table PHANCONG_HISTORY
(
	IDPHANCONG
	IDCONGVIEC
	EDIT_TIME
	IDNGUOINHAN
)

ALTER TABLE GROUP_NGUOIDUNG
ADD CONSTRAINT fk_GROUP_NGUOIDUNG_GROUP
FOREIGN KEY (IDGROUP)
REFERENCES GROUP(ID);

ALTER TABLE GROUP_NGUOIDUNG
ADD CONSTRAINT fk_GROUP_NGUOIDUNG_NGUOIDUNG
FOREIGN KEY (IDNGUOIDUNG)
REFERENCES NGUOIDUNG(ID);

ALTER TABLE GROUP_ACTION
ADD CONSTRAINT fk_GROUP_ACTION_GROUP
FOREIGN KEY (IDGROUP)
REFERENCES GROUP(ID);

ALTER TABLE GROUP_ACTION
ADD CONSTRAINT fk_GROUP_ACTION_ACTION
FOREIGN KEY (IDACTION)
REFERENCES ACTION(ID);

ALTER TABLE BAOCAOCONGVIEC
ADD CONSTRAINT fk_BAOCAO_NGUOIDUNG
FOREIGN KEY (IDNGUOITAO)
REFERENCES NGUOIDUNG(ID);

ALTER TABLE BAOCAOCONGVIEC
ADD CONSTRAINT fk_BAOCAO_CONGVIEC
FOREIGN KEY (IDCONGVIEC)
REFERENCES CONGVIEC(ID);


ALTER TABLE BINHLUAN
ADD CONSTRAINT fk_BINHLUAN_NGUOIDUNG
FOREIGN KEY (IDNGUOITAO)
REFERENCES NGUOIDUNG(ID);

ALTER TABLE BINHLUAN
ADD CONSTRAINT fk_BINHLUAN_CONGVIEC
FOREIGN KEY (IDCONGVIEC)
REFERENCES CONGVIEC(ID);

ALTER TABLE NGUOIDUNG
ADD CONSTRAINT fk_NGUOIDUNG_CHUCDANH
FOREIGN KEY (IDCHUCDANH)
REFERENCES CHUCDANH(ID);

ALTER TABLE NGUOIDUNG
ADD CONSTRAINT fk_NGUOIDUNG_CHUCVU
FOREIGN KEY (IDCHUCVU)
REFERENCES CHUCVU(ID);

ALTER TABLE NGUOIDUNG
ADD CONSTRAINT fk_NGUOIDUNG_PHONGBAN
FOREIGN KEY (IDPHONGBAN)
REFERENCES PHONGBAN(ID);

ALTER TABLE PHANCONG
ADD CONSTRAINT fk_PHANCONG_NGUOIDUNG
FOREIGN KEY (IDNGUOINHAN)
REFERENCES NGUOIDUNG(ID);

ALTER TABLE PHANCONG
ADD CONSTRAINT fk_PHANCONG_CONGVIEC
FOREIGN KEY (IDCONGVIEC)
REFERENCES CONGVIEC(ID);

ALTER TABLE PHANCONG
ADD CONSTRAINT fk_PHANCONG_TRANGTHAI
FOREIGN KEY (IDTRANGTHAI)
REFERENCES TRANGTHAI(ID);

ALTER TABLE PHANCONG
ADD CONSTRAINT fk_PHANDONG_DANGPHANCONG
FOREIGN KEY (IDDANGPHANCONG)
REFERENCES DANGPHANCONG(ID);

ALTER TABLE CONGVIEC
ADD CONSTRAINT fk_CONGVIEC_DANGCONGVIEC
FOREIGN KEY (IDDANGCONGVIEC)
REFERENCES DANGCONGVIEC(ID);

ALTER TABLE CONGVIEC
ADD CONSTRAINT fk_CONGVIEC_NGUOIDUNG
FOREIGN KEY (IDNGUOITAO)
REFERENCES NGUOIDUNG(ID);

INSERT INTO PHONGBAN(TENPHONG) VALUES ('CNTT')
INSERT INTO CHUCVU(TENCHUCVU) VALUES ('ADMIN')
INSERT INTO CHUCVU(TENCHUCVU) VALUES ('USER')

INSERT INTO CHUCDANH(TENCHUCDANH,TENVIETTAT) VALUES (N'KỸ SƯ','KS')
INSERT INTO TRANGTHAI(TEN) VALUES (N'CHƯA XỬ LÝ')
INSERT INTO TRANGTHAI(TEN) VALUES (N'ĐANG XỬ LÝ')
INSERT INTO TRANGTHAI(TEN) VALUES (N'ĐÃ XỬ LÝ')
INSERT INTO TRANGTHAI(TEN) VALUES (N'ĐÃ KIỂM DUYỆT')
INSERT INTO TRANGTHAI(TEN) VALUES (N'XỬ LÝ LẠI')
INSERT INTO DANGCONGVIEC(TEN) VALUES (N'YÊU CẦU')
INSERT INTO DANGCONGVIEC(TEN) VALUES (N'ĐỀ XUẤT')
INSERT INTO DANGPHANCONG(TEN) VALUES (N'GIÁM SÁT')
INSERT INTO DANGPHANCONG(TEN) VALUES (N'XỬ LÝ')
INSERT INTO NGUOIDUNG(TENDANGNHAP,MATKHAU,TENNGUOIDUNG,TRANGTHAI,NGAYTAO,IDPHONGBAN,IDCHUCDANH,IDCHUCVU, EMAIL) VALUES ('ADMIN','ADMIN','ADMIN','TRUE','1/1/2016',1,1,1,'admin@gmail.com')
INSERT INTO NGUOIDUNG(TENDANGNHAP,MATKHAU,TENNGUOIDUNG,TRANGTHAI,NGAYTAO,IDPHONGBAN,IDCHUCDANH,IDCHUCVU, EMAIL) VALUES ('QUAN','QUAN',N'QUÂN','TRUE','1/1/2016',1,1,2,'quanvmh1407@gmail.com')
INSERT INTO NGUOIDUNG(TENDANGNHAP,MATKHAU,TENNGUOIDUNG,TRANGTHAI,NGAYTAO,IDPHONGBAN,IDCHUCDANH,IDCHUCVU, EMAIL) VALUES ('VUONG','VUONG',N'VƯƠNG','TRUE','1/1/2016',1,1,2,'hoangvuong1910@gmail.com')


