
SET IDENTITY_INSERT [dbo].[UserInformation] ON

MERGE [dbo].[UserInformation] UserInfos
USING (VALUES
 	
('1', N'41da71df-8421-4e81-bf3f-4ac01a97ccc8', N'921358D2-8AE1-4B58-963B-6366086D9AFF', N'Nic', N'Collins', N''),

('2', N'9f813136-3288-4695-958b-8062310a5cfd', N'542F6AAF-FB43-4A48-941F-B522EA16605B', N'Phillip', N'Berglund', N'')
)

AS S ([Id],[UserId],[ExternalID],[FirstName], [LastName], [SerializedUserInfo])
ON UserInfos.Id = S.Id
--WHEN MATCHED THEN
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Id],[UserId],[ExternalID],[FirstName], [LastName], [SerializedUserInfo]) VALUES (S.[Id],S.[UserId], S.[ExternalID], S.[FirstName], S.[LastName], S.[SerializedUserInfo]);


SET IDENTITY_INSERT [dbo].[UserInformation] OFF