USE [MoodJournal]
GO

/****** Object:  StoredProcedure [dbo].[spInsertUser]    Script Date: 10/11/2022 00:40:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[spInsertUser]
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
 
 Insert Into [dbo].[User]([ID], [DateCreated], [DateModified], [UserName],[Password],[FirstName],[LastName],[Gender],[Email]) values (@ID,@DateCreated,@DateModified,@UserName,@Password,@FirstName,@LastName,@Gender,@Email) 
 
END
GO


