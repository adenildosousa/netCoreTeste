USE [AccessControl]
GO

ALTER TABLE [ProfileUser] 
	ADD [Default] BIT NOT NULL 
	CONSTRAINT [DF_ProfileUser_Default] DEFAULT(0)
GO

ALTER TABLE [ProfileUserLog]
	ADD [Default] BIT NOT NULL
	CONSTRAINT [DF_ProfileUserLog_Default] DEFAULT(0)
GO