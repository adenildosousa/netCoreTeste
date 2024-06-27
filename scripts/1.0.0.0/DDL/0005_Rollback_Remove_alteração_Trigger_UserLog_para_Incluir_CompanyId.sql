USE [AccessControl]
GO

CREATE OR ALTER TRIGGER [Tg_UserLog] on [User]
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
					'DEL',
					getdate()
            FROM Deleted d
        END
    END
END
GO