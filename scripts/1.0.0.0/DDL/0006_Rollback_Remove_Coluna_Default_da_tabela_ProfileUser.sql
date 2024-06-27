USE [AccessControl]
GO

ALTER TABLE [ProfileUser] DROP CONSTRAINT [DF_ProfileUser_Default]
GO

ALTER TABLE [ProfileUser] DROP COLUMN [Default]
GO

ALTER TABLE [ProfileUserLog] DROP CONSTRAINT [DF_ProfileUserLog_Default]
GO

ALTER TABLE [ProfileUserLog] DROP COLUMN [Default]
GO