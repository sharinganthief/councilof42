
MERGE [dbo].[AspNetUsers] Users
USING (VALUES
 	
('41da71df-8421-4e81-bf3f-4ac01a97ccc8', N'nic', N'APFLaJw8r/0HBqwC6jMWpbOhcu3BKg6jzFN7KIErN3Q1hMUiLFV9UDgryw5AiDxTcQ==', N'd5bf7b9a-8234-4b8b-aac6-464120aeee24', N'ApplicationUser'),

(N'9f813136-3288-4695-958b-8062310a5cfd', N'phillip', N'ANKBxyLFZAnabIaqP0mjxOdT/yJhJ36B8MvKknv3+SIDBfJW8JH4DjK8tA62UUoFAg==', N'68684b00-894f-46dc-983c-9299dd021920', N'ApplicationUser')
)

AS S ([Id],	[UserName],	[PasswordHash],	[SecurityStamp], [Discriminator])
ON users.Id = S.Id
--WHEN MATCHED THEN
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Id],[UserName],[PasswordHash],[SecurityStamp],[Discriminator]) VALUES (S.Id, S.UserName, S.PasswordHash, S.SecurityStamp, S.Discriminator);