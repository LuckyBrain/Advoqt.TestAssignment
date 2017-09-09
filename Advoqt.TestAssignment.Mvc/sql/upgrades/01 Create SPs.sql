USE [AdvoqtTestAssignment]
GO
IF OBJECT_ID(N'[dbo].[usp_GetUsers]') IS NOT NULL
	DROP PROCEDURE [dbo].[usp_GetUsers]
GO
CREATE PROC [dbo].[usp_GetUsers]
    @userId nvarchar(128) = NULL,
    @userName nvarchar(256) = NULL,
    @email nvarchar(256) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT *
	FROM dbo.AspNetUsers X
	WHERE   (@userId IS NULL OR X.Id = @userId)
		AND (@userName IS NULL OR X.UserName = @userName)
		AND (@email IS NULL OR X.Email = @email)
END
GO

IF OBJECT_ID(N'[dbo].[usp_AddUser]') IS NOT NULL
	DROP PROCEDURE [dbo].usp_AddUser
GO
CREATE PROC [dbo].[usp_AddUser]
    @id nvarchar(128),
    @userName nvarchar(256),
    @email nvarchar(256),
	@passwordHash nvarchar(MAX),
	@isAdmin bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT dbo.AspNetUsers
		(Id, Email, EmailConfirmed, PasswordHash, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName, IsAdmin)
	SELECT
		Id = @id,
		Email = @email,
		EmailConfirmed = 0,
		PasswordHash = @passwordHash,
		PhoneNumberConfirmed = 0,
		TwoFactorEnabled = 0,
		LockoutEnabled = 0,
		AccessFailedCount = 0,
		UserName = @userName,
		IsAdmin = @isAdmin
	WHERE	NOT EXISTS
			(
				SELECT *
				FROM dbo.AspNetUsers
				WHERE	Email = @email
					OR	UserName = @userName
			)
END
GO

IF OBJECT_ID(N'[dbo].[usp_ModifyUser]') IS NOT NULL
	DROP PROCEDURE [dbo].[usp_ModifyUser]
GO
CREATE PROC [dbo].[usp_ModifyUser]
    @id nvarchar(128),
	@passwordHash nvarchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE dbo.AspNetUsers SET
		PasswordHash = @passwordHash
	WHERE	Id = @id
END
GO

IF OBJECT_ID(N'[dbo].[usp_RemoveUser]') IS NOT NULL
	DROP PROCEDURE [dbo].[usp_RemoveUser]
GO
CREATE PROC [dbo].[usp_RemoveUser]
    @id nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE dbo.AspNetUsers
	WHERE   Id = @id
END
GO
