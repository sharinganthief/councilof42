﻿CREATE TABLE [dbo].[DreamLog]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
	[Title] NVARCHAR(50) NOT NULL, 
	[Text] NVARCHAR(MAX) NOT NULL, 
	[Date] DATETIME NOT NULL
)
