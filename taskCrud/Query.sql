CREATE TABLE Employee (
    EmployeeID int primary key identity,
    EmployeeName varchar(100) NOT NULL,
    Address varchar(255) NOT NULL,
    RepotingManager varchar(100) NOT NULL,
    Location varchar(100) NOT NULL,
    IsActive bit NOT NULL
);
