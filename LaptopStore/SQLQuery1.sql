CREATE DATABASE [LapTopStore]
GO

USE [LapTopStore]
GO

CREATE TABLE [Category] (
    [CateID] int NOT NULL IDENTITY(1,1),
	[Name] nvarchar(50) NOT NULL,
	[Alias] text NULL,
	CONSTRAINT [PK_Category] PRIMARY KEY ([CateID])
);
GO

CREATE TABLE [Product] (
    [ProductID] int NOT NULL IDENTITY(1,1),
	[CateID] int NOT NULL,
    [Name] nvarchar(50) NOT NULL,
	[Price] float NOT NULL,
	[Image] nvarchar(max) NOT NULL,
	[Description] nvarchar(2000) NULL,
	[Quantity] int NOT NULL,
	[Discount] float NOT NULL,
	[Status] bit NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([ProductID]),
	CONSTRAINT [FK_Product_Category_CateID] FOREIGN KEY ([CateID]) REFERENCES [Category] ([CateID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Customer] (
	[CustomerID] nvarchar(100) NOT NULL,
	[Email] nvarchar(50) NOT NULL,
	[Password] nvarchar(20) NOT NULL,
	[Fullname] nvarchar(max) NOT NULL,
	[Address] nvarchar(1000) NULL,
	[Activated] bit NOT NULL,
	CONSTRAINT [PK_Customer] PRIMARY KEY ([CustomerID])
);
GO

CREATE TABLE [Order] (
	[OrderID] int NOT NULL IDENTITY(1,1),
	[CustomerID] nvarchar(100) NOT NULL,
	[Address] nvarchar(1000) NOT NULL,
	[Amount] float NOT NULL,
	[RequireDate] datetime NOT NULL,
	[OrderDate] datetime NOT NULL,
	[Description] nvarchar(max) NULL,
	[Status] bit NOT NULL,
	CONSTRAINT [PK_Order] PRIMARY KEY ([OrderID]),
	CONSTRAINT [FK_Order_Customer_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [Customer] ([CustomerID]) ON DELETE CASCADE
);
GO

CREATE TABLE [OrderDetail] (
	[Id] int NOT NULL IDENTITY(1,1),
	[OrderID] int NOT NULL,
	[ProductID] int NOT NULL,
	[Discount] float NOT NULL,
	[Quantity] int NOT NULL,
	[UnitPrice] float NOT NULL,
    CONSTRAINT [PK_OrderDetail] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderDetail_Order_OrderID] FOREIGN KEY ([OrderID]) REFERENCES [Order] ([OrderID]) ON DELETE CASCADE,
	CONSTRAINT [FK_OrderDetail_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Product] ([ProductID]) ON DELETE CASCADE
);
GO

INSERT INTO [Category] (Name, Alias) 
VALUES ('DELL','dell'), ('ASUS','asus'), ('SONY','sony'), ('ACER','acer'), ('HP','hp')
GO

INSERT INTO [Product] (CateID, Name, Price, Image, Description, Quantity, Discount, Status)
VALUES ('2', 'Asus Vivobook 14 X405UA-BV327T','15000000', 'asus1.jpg', 'Laptop ASUS-1', '10', '0.1', '1'),
		('2', 'Asus ZenBook UX430UA-GV261T','14000000', 'asus2.jpg', 'Laptop ASUS-2', '15', '0.05', '0'),
		('1', 'Dell Vostro 7570-70138565','13600000', 'dell1.jpg', 'Laptop DELL-1', '10', '0.05', '1'),
		('1', 'Dell Inspiron 3467-M20NR11','12300000', 'dell2.jpg', 'Laptop DELL-2', '21', '0.05', '0'),
		('3', 'Sony VAIO SVF1521BYA','17600000', 'sony1.jpg', 'Laptop SONY-1', '22', '0.02', '1'),
		('3', 'Sony VAIO VPC-W111XX','1930000', 'sony2.jpg', 'Laptop SONY-2', '30', '0.02', '1'),
		('5', 'HP Pavilion 14-ab117TU','9500000', 'hp1.jpg', 'Laptop HP-1', '23', '0.15', '0'),
		('5', 'HP Probook 450 G3-T1A15PA','10500000', 'hp2.jpg', 'Laptop HP-2', '14', '0.15', '1'),
		('4', 'Acer AS E5-575G-73DR','11450000', 'acer1.jpg', 'Laptop ACER-1', '6', '0.08', '1'),
		('4', 'Acer E5-473-58U5','10870000', 'acer2.jpg', 'Laptop ACER-2', '12', '0.08', '0'),
		('1', 'Dell Latitude 5480-70127518','14590000', 'dell3.jpg', 'Laptop DELL-3', '5', '0.05', '0'),
		('1', 'Dell Inspiron 5379-JYN0N1','15670000', 'dell4.jpg', 'Laptop DELL-4', '18', '0.05', '1')
GO

INSERT INTO [Customer] (CustomerID, Email, Password, Fullname, Address, Activated)
VALUES ('CUS001', 'Xuan123@gmail.com', 'abc123','Le Thi Xuan', '119/34A Tran Hoan Street District 3 HCMC', '1'),
		('CUS002', 'Toan123@yahoo.com', 'abc456','Tran Hung Toan', '34Bis Nguyen Van Loi Street District HCMC', '0'),
		('CUS003', 'Huy123@gmail.com', 'abc789','Le Huy', '18/8H Tran Ke Xuong Street Phu Nhuan District HCMC', '1'),
		('CUS004', 'Nhan123@gmail.com', 'def123','Dang Hoang Nhan', '45/9 Le Loi Street District 1 HCMC', '0'),
		('CUS005', 'Khanh123@yahoo.com', 'def45', 'Le Viet Khanh', '23B Nguyen Van Nghi Street Go Vap District HCMC', '1')
GO

INSERT INTO[Order] (CustomerID, Address, Amount, RequireDate, OrderDate, Description, Status)
VALUES ('CUS002', '34Bis Nguyen Van Loi Street District 1 HCMC', '2', '03/25/2017', '03/27/2017', '', '1'),
		('CUS001', '119/34A Tran Hoan Street District 3 HCMC', '3', '04/14/2017', '04/16/2017', '', '0'),
		('CUS005', '23B Nguyen Van Nghi Street Go Vap District HCMC', '5', '09/25/2017', '09/30/2017', '', '1'),
		('CUS003', '18/8H Tran Ke Xuong Street Phu Nhuan District HCMC', '10', '10/14/2017', '10/16/2017', '', '1'),
		('CUS004', '45/9 Le Loi Street District 1 HCMC', '8', '04/01/2017', '04/04/2017', '', '0')
GO  

INSERT INTO[OrderDetail] (OrderID, ProductID, Discount, Quantity, UnitPrice)
VALUES ('1', '2', '0.1', '10', '14500000'),
		('2', '1', '0.05', '15', '13800000'),
		('5', '3', '0.05', '8', '13200000'),
		('4', '4', '0.05', '10', '12000000'),
		('3', '5', '0.02', '12', '17100000')
GO

