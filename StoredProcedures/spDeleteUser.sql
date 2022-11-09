USE [MoodJournal]
GO

/****** Object:  StoredProcedure [dbo].[spDeleteUser]    Script Date: 09/11/2022 15:42:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



create PROCEDURE [dbo].[spDeleteUser]
(@ID uniqueidentifier)
AS
BEGIN
SET NOCOUNT ON
 
DELETE FROM [User]
WHERE ID = @ID
 
END
GO


