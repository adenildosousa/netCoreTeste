USE [AccessControl]
GO

ALTER TABLE [User] DROP CONSTRAINT [UK_User_CompanyId_Username]
GO

ALTER TABLE [UserLog] DROP COLUMN [CompanyId]
GO

ALTER TABLE [User] DROP COLUMN [CompanyId]
GO