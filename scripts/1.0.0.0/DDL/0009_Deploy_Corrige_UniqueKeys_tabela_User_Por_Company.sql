USE [AccessControl]
GO

ALTER TABLE [User] DROP CONSTRAINT [UK_User]
GO

ALTER TABLE [User] DROP CONSTRAINT [UK_User_CompanyId_Username]
GO

ALTER TABLE [User] DROP CONSTRAINT [UK_User_PersonId]
GO

ALTER TABLE [User] DROP CONSTRAINT [UK_User_Username]
GO

ALTER TABLE [User] ADD CONSTRAINT [UK_User] UNIQUE (PersonId, Username, CompanyId)
GO

ALTER TABLE [User] ADD CONSTRAINT [UK_User_Username] UNIQUE (Username, CompanyId)
GO

ALTER TABLE [User] ADD CONSTRAINT [UK_User_PersonId] UNIQUE (PersonId, CompanyId)
GO