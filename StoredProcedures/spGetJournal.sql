USE [MoodJournal]
GO

/****** Object:  StoredProcedure [dbo].[spGetJournal]    Script Date: 11/11/2022 10:26:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[spGetJournal]
(@ID uniqueidentifier)
AS
BEGIN
SET NOCOUNT ON
 
SELECT [ID], [DateCreated], [DateModified], [User_UserID], [Mood], [JournalContent] FROM [Journal] WHERE ID = IIF(@ID IS NULL, ID, @ID)
 
END
GO


