USE [MoodJournal]
GO

/****** Object:  StoredProcedure [dbo].[spGetJournal]    Script Date: 11/11/2022 10:24:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[spGetJournalByUser_UserID]
(@User_UserID uniqueidentifier)
AS
BEGIN
SET NOCOUNT ON
 
SELECT [ID], [DateCreated], [DateModified], [User_UserID], [Mood], [JournalContent] FROM [Journal] WHERE [User_UserID] = @User_UserID
 
END
GO


