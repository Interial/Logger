
CREATE TABLE EventLog( 
	 [ID] int identity primary key NULL,
	[EventID] int,  
    [LogLevel] nvarchar(50), 
    [Message] nvarchar(4000), 
	[CreatedTime] datetime2 
	
)
