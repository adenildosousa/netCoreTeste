USE [AccessControl]
GO

CREATE TABLE [FuncAux] (Id INT NOT NULL, Name VARCHAR(100), Description VARCHAR(500))
GO

INSERT INTO [FuncAux] (Id, Name, Description)
	SELECT Id, Name, Description FROM [Functionality]
GO

TRUNCATE TABLE [Functionality]
GO

ALTER TABLE [FunctionalityProfile] DROP CONSTRAINT [FK_FunctionalityProfile_Functionality_ID]
GO

ALTER TABLE [Functionality] DROP CONSTRAINT [PK_Functionality]
GO

ALTER TABLE [Functionality] DROP COLUMN [Id]
GO

ALTER TABLE [Functionality] ADD [Id] INT NOT NULL IDENTITY(1,1)
GO

SET IDENTITY_INSERT [Functionality] ON
GO

INSERT INTO [Functionality] (Id, Name, Description)
	SELECT Id, Name, Description FROM [FuncAux]
GO

SET IDENTITY_INSERT [Functionality] OFF
GO

ALTER TABLE [Functionality] ADD CONSTRAINT [PK_Functionality] PRIMARY KEY (Id)
GO
	
ALTER TABLE [FunctionalityProfile] ADD CONSTRAINT [FK_FunctionalityProfile_Functionality_ID] 
	FOREIGN KEY (FunctionalityId) REFERENCES [FunctionalityProfile](Id)
GO

DROP TABLE IF EXISTS [FuncAux]
GO