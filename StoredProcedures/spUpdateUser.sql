USE [MoodJournal]
GO

/****** Object:  StoredProcedure [dbo].[spUpdateUser]    Script Date: 10/11/2022 00:40:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[spUpdateUser]
(@ID uniqueidentifier,
@DateCreated datetime,
@DateModified datetime,
@UserName varchar(max),
@Password varchar(max),
@FirstName varchar(max),
@LastName varchar(max),
@Gender varchar(max),
@Email varchar(max)) 
AS
BEGIN
SET NOCOUNT ON
 
update [dbo].[User] set [DateModified] = @DateModified, [UserName] = @UserName,[Password] = @Password,[FirstName] = @FirstName,[LastName] = @LastName,[Gender] = @Gender,[Email] = @Email where ID = @ID 
 
END
GO


