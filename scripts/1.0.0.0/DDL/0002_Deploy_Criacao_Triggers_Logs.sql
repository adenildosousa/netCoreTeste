USE [AccessControl]
GO

CREATE TRIGGER [Tg_FunctionalityProfileLog] on [FunctionalityProfile]
AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON
    IF (EXISTS(SELECT * FROM Inserted) AND EXISTS (SELECT * FROM Deleted))
    BEGIN
        
         INSERT INTO [FunctionalityProfileLog]
            SELECT 
				i.*,
				2,
				'UPD',
				getdate()
            FROM Inserted i
    END
    ELSE BEGIN
 
        IF (EXISTS(SELECT * FROM Inserted))
        BEGIN
 
            INSERT INTO [FunctionalityProfileLog]
            SELECT 
				i.*,
				1,
				'INS',
				getdate()
            FROM Inserted i
 
        END
        ELSE BEGIN
 
             INSERT INTO [FunctionalityProfileLog]
				SELECT 
					d.*,
					3,
					'INS',
					getdate()
            FROM Deleted d
        END
    END
END
GO

CREATE TRIGGER [Tg_UserLog] on [User]
AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON
    IF (EXISTS(SELECT * FROM Inserted) AND EXISTS (SELECT * FROM Deleted))
    BEGIN
        
         INSERT INTO [UserLog]
            SELECT 
				i.*,
				2,
				'UPD',
				getdate()
            FROM Inserted i
    END
    ELSE BEGIN
 
        IF (EXISTS(SELECT * FROM Inserted))
        BEGIN
 
            INSERT INTO [UserLog]
            SELECT 
				i.*,
				1,
				'INS',
				getdate()
            FROM Inserted i
 
        END
        ELSE BEGIN
 
             INSERT INTO [UserLog]
				SELECT 
					d.*,
					3,
					'INS',
					getdate()
            FROM Deleted d
        END
    END
END
GO

CREATE TRIGGER [Tg_ProfileLog] on [Profile]
AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON
    IF (EXISTS(SELECT * FROM Inserted) AND EXISTS (SELECT * FROM Deleted))
    BEGIN
        
         INSERT INTO [ProfileLog]
            SELECT 
				i.*,
				2,
				'UPD',
				getdate()
            FROM Inserted i
    END
    ELSE BEGIN
 
        IF (EXISTS(SELECT * FROM Inserted))
        BEGIN
 
            INSERT INTO [ProfileLog]
            SELECT 
				i.*,
				1,
				'INS',
				getdate()
            FROM Inserted i
 
        END
        ELSE BEGIN
 
             INSERT INTO [ProfileLog]
				SELECT 
					d.*,
					3,
					'INS',
					getdate()
            FROM Deleted d
        END
    END
END
GO

CREATE TRIGGER [Tg_ProfileUserLog] on [ProfileUser]
AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON
    IF (EXISTS(SELECT * FROM Inserted) AND EXISTS (SELECT * FROM Deleted))
    BEGIN
        
         INSERT INTO [ProfileUserLog]
            SELECT 
				i.*,
				2,
				'UPD',
				getdate()
            FROM Inserted i
    END
    ELSE BEGIN
 
        IF (EXISTS(SELECT * FROM Inserted))
        BEGIN
 
            INSERT INTO [ProfileUserLog]
            SELECT 
				i.*,
				1,
				'INS',
				getdate()
            FROM Inserted i
 
        END
        ELSE BEGIN
 
             INSERT INTO [ProfileUserLog]
				SELECT 
					d.*,
					3,
					'INS',
					getdate()
            FROM Deleted d
        END
    END
END
GO