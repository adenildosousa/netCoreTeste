USE [AccessControl]
GO

ALTER TABLE [User] ADD [CompanyId] BIGINT
GO

ALTER TABLE [UserLog] ADD [CompanyId] BIGINT
GO

ALTER TABLE [User] ADD CONSTRAINT [UK_User_CompanyId_Username] UNIQUE (CompanyId, Username)
GO