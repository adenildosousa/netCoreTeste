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
				i.Id,
				i.PersonId,
				i.UserId,
				i.ProfileId,
				i.CompanyId,
				i.UserUpdateId,
				2,
				'UPD',
				getdate(),
				i.[Default]
            FROM Inserted i
    END
    ELSE BEGIN
 
        IF (EXISTS(SELECT * FROM Inserted))
        BEGIN
            INSERT INTO [ProfileUserLog]
            SELECT 
				i.Id,
				i.PersonId,
				i.UserId,
				i.ProfileId,
				i.CompanyId,
				i.UserUpdateId,
				1,
				'INS',
				getdate(),
				i.[Default]
            FROM Inserted i
        END
        ELSE BEGIN
             INSERT INTO [ProfileUserLog]
				SELECT 
					d.Id,
					d.PersonId,
					d.UserId,
					d.ProfileId,
					d.CompanyId,
					d.UserUpdateId,
					3,
					'DEL',
					getdate(),
					d.[Default]
            FROM Deleted d
        END
    END
END