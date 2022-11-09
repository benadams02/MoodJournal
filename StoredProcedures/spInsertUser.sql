USE [MoodJournal]
GO

/****** Object:  StoredProcedure [dbo].[spInsertUser]    Script Date: 09/11/2022 18:03:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[spInsertUser]
(@ID uniqueidentifier,
@DateCreated datetime,
@DateModified datetime,
@UserName varchar,
@Password varchar,
@FirstName varchar,
@LastName varchar,
@Gender varchar,
@Email varchar) 
AS
BEGIN
SET NOCOUNT ON
 
 Insert Into [dbo].[User]([ID], [DateCreated], [DateModified], [UserName],[Password],[FirstName],[LastName],[Gender],[Email]) values (@ID,@DateCreated,@DateModified,@UserName,@Password,@FirstName,@LastName,@Gender,@Email) 
 
END
GO


