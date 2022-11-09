USE [MoodJournal]
GO

/****** Object:  StoredProcedure [dbo].[spGetUser]    Script Date: 09/11/2022 15:45:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[spGetUser]
(@ID uniqueidentifier)
AS
BEGIN
SET NOCOUNT ON
 
SELECT [ID], [DateCreated], [DateModified], [UserName], [FirstName], [LastName], [Password], [Email], [Gender] FROM [User]
WHERE ID = iif(@ID is null,ID,@ID)
 
END
GO


