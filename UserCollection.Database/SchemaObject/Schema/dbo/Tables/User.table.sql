﻿CREATE TABLE [dbo].[User]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(255) NOT NULL, 
    [LastName] NVARCHAR(255) NOT NULL, 
    [Email] NVARCHAR(255) NOT NULL, 
    [Phone] NVARCHAR(50) NOT NULL, 
    [Active] BIT NOT NULL
)