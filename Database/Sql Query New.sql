create database CarSaleNew;

use CarSaleNew;

create table Manufactured_Campany
(MCName varchar(200)primary key);

insert into Manufactured_Campany values
('Toyota');
delete from Manufactured_Campany;
select * from Manufactured_Campany;

create table Model
(modelID int identity primary key,
MCID varchar(200),
ModelName varchar(30),
foreign key (MCID) references Manufactured_Campany(MCName));

select * from Model;


insert into Model values
('Toyota','Hybrid');
delete from Model where modelid=5; 

create table Supplier
(NIC varchar(13) primary key,
FirstName varchar(200),
LastName varchar(200),
Telephone varchar(15),
Email varchar(200));

insert into Supplier values
('982930023v','Malith','W',0789670226,'Email');

delete from Supplier;
select * from Supplier;

create table Vehicle
(VehicleID varchar(200)primary key,
MCID int,
SupplierID varchar(13),
YearOfMade varchar(4),
ChassisNumber varchar(20),
EngineNumber varchar(20),
EngineCapacity varchar(10),
FuelType varchar(10),
Transmissiontype varchar(20),
Condition varchar(15),
BarCode varchar(500),
QRCode varchar(500),
Vehiclehphoto varchar(500),
InColor varchar(15),
OutColor varchar(15),
ImportedDate date,
YearOfRegistered varchar(4),
Mileage decimal(10,2),
PlateNumber varchar(20),
VehicleStatus varchar(15),
Price decimal(10,2),
foreign key (MCID) references Model(modelID),
foreign key (SupplierID) references Supplier(NIC));

select * from Vehicle;
delete from Vehicle;

create table Warranty
(WID int identity primary key,
VehicleID varchar(200),
period int,
WStartDate date,
WEndDate date,
ServiceProvider varchar(100),
WarrantyStatus varchar(200),
foreign key (VehicleID) references Vehicle(VehicleID));

delete from Warranty;

create table Salesman
(SalesmanID varchar(200)primary key,
NIC varchar(120),
FirstName varchar(200),
LastName varchar(200),
Gender varchar(10),
Telephone varchar(13),
Email varchar(200),
JoinedDate date,
Salary decimal(10,2),
ProfilePicture varchar(300));

insert into Salesman values
('SM01','972934421v','Malith','W','Male','077','malith@gmail.com','2021-10-10',1000,00);

delete from Salesman;
select * from Salesman;

create table Customer
(CustomerNIC varchar(13) primary key,
FirstName varchar(200),
LastName varchar(200),
Gender varchar(10) ,
Telephone varchar(15),
Email varchar(200),
AddressNO varchar(50),
FirstAddressLine varchar(100) ,
SecondAddressLine varchar(100),
City varchar(100));

delete from Customer;

create table Bill
(BillID varchar(100) primary key,
CustomerID varchar(13),
IssuedDateTime datetime ,
Discount varchar(100),
Balance varchar(100),
paymenttype varchar(10),
FinanceAmount varchar(100) ,
CustomerAmount varchar(100) ,
CheckID varchar(100),
Amount varchar(100),
foreign key(CustomerID) references Customer(CustomerNIC));

delete from Bill;

create table Sale
(SaleID int identity primary key,
VehicleID varchar(200) ,
BillID varchar(100),
SalesmanID varchar(200),
SoldDateTime datetime,
Commission varchar(100),
foreign key (VehicleID) references Vehicle(VehicleID),
foreign key (BillID) references Bill(BillID),
foreign key (SalesmanID) references Salesman(SalesmanID));

delete from Sale;



create table VehicleSaleUser
(UserName varchar(10) primary key,
UserPassword varchar(16),
UserType varchar(5),
UserImage varchar(200));



insert into VehicleSaleUser values
('Malith','12345','1');
select*from VehicleSaleUser;
delete from VehicleSaleUser;
drop table VehicleSaleUser;


