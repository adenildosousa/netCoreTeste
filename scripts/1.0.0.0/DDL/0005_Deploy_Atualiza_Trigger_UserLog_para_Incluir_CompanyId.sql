USE [AccessControl]
GO

CREATE OR ALTER TRIGGER [Tg_UserLog] on [User]
AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON
    IF (EXISTS(SELECT * FROM Inserted) AND EXISTS (SELECT * FROM Deleted))
    BEGIN
		INSERT INTO [UserLog]
			select 
				i.Id,
				i.PersonId, 
				i.Username, 
				i.Password, 
				i.StatusId, 
				i.UserUpdateId,
				2,
				'UPD',
				getdate(),
				i.CompanyId
			from Inserted i
    END
    ELSE BEGIN
        IF (EXISTS(SELECT * FROM Inserted))
        BEGIN
            INSERT INTO [UserLog]
				select 
					i.Id,
					i.PersonId, 
					i.Username, 
					i.Password, 
					i.StatusId, 
					i.UserUpdateId,
					1,
					'INS',
					getdate(),
					i.CompanyId
				from Inserted i
 
        END
        ELSE BEGIN
             INSERT INTO [UserLog]
			 select 
					d.Id,
					d.PersonId, 
					d.Username, 
					d.Password, 
					d.StatusId, 
					d.UserUpdateId,
					3,
					'DEL',
					getdate(),
					d.CompanyId
				from Deleted d
        END
    END
END
GO