CREATE TABLE [dbo].[UserInformation]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[UserId] NVARCHAR (128) NOT NULL, 
	[ExternalID] UNIQUEIDENTIFIER NOT NULL, 
	[FirstName] NVARCHAR(50) NOT NULL, 
	[LastName] NVARCHAR(50) NOT NULL,
	
	[SerializedUserInfo] VARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_User_UserInformation] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
)
