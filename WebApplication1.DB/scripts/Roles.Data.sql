﻿
MERGE [dbo].[AspNetRoles] roles
USING (VALUES

(N'12546C33-B182-4716-B4DA-A9448473B118', N'user'),
(N'1DC8DF90-E592-486B-BD5A-982350D95642', N'developer'),
(N'212CCB3D-203B-45F3-B586-7787EA65859B', N'movieadmin'),
(N'4161D107-0B07-4736-87C9-0015542F7BC7', N'friend') 	

)

AS S ([Id], [Name])
ON roles.Id = S.Id
--WHEN MATCHED THEN
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Id], [Name]) VALUES (S.Id, S.Name);