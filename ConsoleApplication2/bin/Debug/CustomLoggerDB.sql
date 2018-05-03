CREATE DATABASE CustomLoggerDB  
GO  
USE CustomLoggerDB 
GO  
CREATE TABLE EventLog(  
    [ID] int identity primary key,  
    [EventID] int,  
    [LogLevel] nvarchar(50), 
    [Message] nvarchar(4000), 
    [CreatedTime] datetime2 
	
) 
GO 
CREATE TABLE Student(  
    [ID] int identity primary key,  
    [Name] nvarchar(50),  
    [Age] int  
	
)  
GO 
INSERT INTO Student VALUES('Bear',18)  
INSERT INTO Student VALUES('Frank',20) 
GO
create procedure spInsertLog
@ExceptionMessage nvarchar(max)
as
begin 
		insert into tblLog([Date], [ExceptionMessage])
		values (Getdate(), @ExceptionMessage)
		end