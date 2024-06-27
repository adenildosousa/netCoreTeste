USE [AccessControl]

create table [Functionality](
	Id int IDENTITY(1,1) not null, 
	Name VARCHAR(100) not null,
	Description Varchar(500),
	CONSTRAINT PK_Functionality PRIMARY KEY CLUSTERED(Id)
)

create table [User](
	Id BIGINT IDENTITY(1,1) not null, 
	PersonId BIGINT not  null INDEX IX_User_PersonId NONCLUSTERED,
	Username Varchar(50) not  null INDEX IX_User_UserName NONCLUSTERED,
	Password Varchar(200) not  null INDEX IX_User_Password NONCLUSTERED,
	StatusId int not null INDEX IX_User_StatusId NONCLUSTERED,
	UserUpdateId bigint,
	CONSTRAINT PK_User PRIMARY KEY CLUSTERED(Id),
	CONSTRAINT UK_User_PersonId UNIQUE(Id, PersonId),
	CONSTRAINT UK_User UNIQUE(PersonId, Username),
	CONSTRAINT FK_User_UserUpdateId FOREIGN KEY (UserUpdateId) REFERENCES [User](Id),
)

create table [UserLog](
	Id BIGINT, 
	PersonId BIGINT not  null,
	Username Varchar(50) not  null,
	Password Varchar(200) not  null,
	StatusId int not null,
	UserUpdateId bigint,

	Operation int not null INDEX IX_ProfileLog_Operation NONCLUSTERED,
	OperationNote varchar(3) not null,
	OperationDate Datetime not null INDEX IX_ProfileLog_OperationDate NONCLUSTERED,

	CONSTRAINT FK_UserLog_UserId FOREIGN KEY (Id) REFERENCES [User](Id),
	CONSTRAINT FK_UserLog_UserUpdateId FOREIGN KEY (UserUpdateId) REFERENCES [User](Id),
	CONSTRAINT CHK_UserLog_Operation check ([Operation] = 1 OR [Operation]= 2 OR [Operation] = 3),
	CONSTRAINT CHK_UserLog_OperationNote check ([OperationNote] = 'Ins' OR [OperationNote]= 'Upd' OR [OperationNote] = 'Del'),
	
)

create table [Profile](
	Id BIGINT IDENTITY(1,1) not null, 
	CompanyId BIGINT not null,
	Name varchar(100) not null,
	Description Varchar(500),
	StatusId int not null INDEX IX_Profile_StatusId NONCLUSTERED,
	UserUpdateId bigint not null,
	CONSTRAINT PK_Profile PRIMARY KEY CLUSTERED(Id, CompanyId),
	CONSTRAINT FK_Profile_UserUpdateId FOREIGN KEY (UserUpdateId) REFERENCES [User](Id)
)

create table [ProfileLog](
	Id BIGINT not null,
	CompanyId BIGINT not null,
	Name varchar(100) not null,
	Description Varchar(500),
	StatusId int not null,
	UserUpdateId bigint not null,

	Operation int not null INDEX IX_ProfileLog_Operation NONCLUSTERED,
	OperationNote varchar(3) not null,
	OperationDate Datetime  not null INDEX IX_ProfileLog_OperationDate NONCLUSTERED,

	CONSTRAINT CHK_ProfileLog_Operation check ([Operation] = 1 OR [Operation]= 2 OR [Operation] = 3),
	CONSTRAINT CHK_ProfileLog_OperationNote check ([OperationNote] = 'Ins' OR [OperationNote]= 'Upd' OR [OperationNote] = 'Del'),
	CONSTRAINT FK_ProfileLog_ProfileId FOREIGN KEY (Id, CompanyId) REFERENCES [Profile](Id, CompanyId),
	CONSTRAINT FK_ProfileLog_UserUpdateId FOREIGN KEY (UserUpdateId) REFERENCES [User](Id)
)

create table [ProfileUser](
	Id bigint IDENTITY(1,1) not null, 
	PersonId BIGINT not  null INDEX IX_ProfileUser_PersonId NONCLUSTERED,
	UserId bigint not null INDEX IX_ProfileUser_UserId NONCLUSTERED,
	ProfileId bigint not null,
	CompanyId BIGINT not null ,
	UserUpdateId bigint not null,
	CONSTRAINT PK_ProfileUser PRIMARY KEY CLUSTERED(Id),
	CONSTRAINT FK_ProfileUser_ProfileId FOREIGN KEY (ProfileId, CompanyId) REFERENCES [Profile](Id, CompanyId),
	CONSTRAINT FK_ProfileUser_UserId FOREIGN KEY (UserId) REFERENCES [User](Id)
)

create table [ProfileUserLog](
	Id bigint not null, 
	PersonId BIGINT not null,
	UserId bigint not null,
	ProfileId bigint not null,
	CompanyId BIGINT not null ,
	UserUpdateId bigint not null,

	Operation int not null INDEX IX_ProfileLog_Operation NONCLUSTERED,
	OperationNote varchar(3) not null,
	OperationDate Datetime  not null INDEX IX_ProfileLog_OperationDate NONCLUSTERED,

	CONSTRAINT FK_ProfileUserLog_ProfileId FOREIGN KEY (ProfileId, CompanyId) REFERENCES [Profile](Id, CompanyId),
	CONSTRAINT FK_ProfileUserLog_UserId FOREIGN KEY (UserId) REFERENCES [User](Id),
	CONSTRAINT CHK_ProfileUserLog_Operation check ([Operation] = 1 OR [Operation]= 2 OR [Operation] = 3),
	CONSTRAINT CHK_ProfileUserLog_OperationNote check ([OperationNote] = 'Ins' OR [OperationNote]= 'Upd' OR [OperationNote] = 'Del'),
	CONSTRAINT FK_ProfileUserLog_UserUpdateId FOREIGN KEY (UserUpdateId) REFERENCES [User](Id)
)

create table [FunctionalityProfile](
	Id int IDENTITY(1,1) not null, 
	CompanyId BIGINT not null,
	ProfileId bigint NOT NULL,
	FunctionalityId INT NOT NULL CONSTRAINT FK_FunctionalityProfile_Functionality_ID FOREIGN KEY (FunctionalityId) REFERENCES Functionality(ID),
	UserUpdateId bigint not null,
	CONSTRAINT PK_FunctionalityProfile_ID PRIMARY KEY CLUSTERED(Id),
	CONSTRAINT FK_FunctionalityProfile_profile_ID FOREIGN KEY (ProfileId, CompanyId) REFERENCES [Profile](Id, CompanyId),
	CONSTRAINT FK_FunctionalityProfile_FunctionalityId FOREIGN KEY (ProfileId, CompanyId) REFERENCES [Profile](Id, CompanyId),
	CONSTRAINT FK_FunctionalityProfile_UserUpdateId FOREIGN KEY (UserUpdateId) REFERENCES [User](Id)
)

create table [FunctionalityProfileLog](
	Id int not null, 
	CompanyId BIGINT not null,
	ProfileId bigint NOT NULL,
	FunctionalityId INT NOT NULL,
	UserUpdateId bigint not null,
	
	Operation int not null INDEX IX_ProfileLog_Operation NONCLUSTERED,
	OperationNote varchar(3) not null,
	OperationDate Datetime  not null INDEX IX_ProfileLog_OperationDate NONCLUSTERED,

	CONSTRAINT FK_FunctionalityProfileLog_profile_ID FOREIGN KEY (ProfileId, CompanyId) REFERENCES [Profile](Id, CompanyId),
	CONSTRAINT FK_FunctionalityProfileLog_FunctionalityId FOREIGN KEY (ProfileId, CompanyId) REFERENCES [Profile](Id, CompanyId),
	CONSTRAINT FK_FunctionalityProfileLog_UserUpdateId FOREIGN KEY (UserUpdateId) REFERENCES [User](Id),
	CONSTRAINT CHK_FunctionalityProfileLog_Operation check ([Operation] = 1 OR [Operation]= 2 OR [Operation] = 3),
	CONSTRAINT CHK_FunctionalityProfileLog_OperationNote check ([OperationNote] = 'Ins' OR [OperationNote]= 'Upd' OR [OperationNote] = 'Del'),
)