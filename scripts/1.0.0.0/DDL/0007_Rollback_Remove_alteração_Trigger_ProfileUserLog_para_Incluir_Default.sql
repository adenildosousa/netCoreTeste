USE [AccessControl]
GO

CREATE OR ALTER TRIGGER [Tg_ProfileUserLog] on [ProfileUser]
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