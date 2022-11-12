USE [MoodJournal]
GO

/****** Object:  StoredProcedure [dbo].[spInsertJournal]    Script Date: 12/11/2022 01:38:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[spUpdateJournal]
(@ID uniqueidentifier,
@DateCreated datetime,
@DateModified datetime,
@User_UserID uniqueidentifier,
@Mood varchar(max),
@JournalContent varchar(max))
AS
BEGIN
SET NOCOUNT ON
 
UPDATE [dbo].[Journal] SET [ID] = @ID,[DateModified] = @DateModified, [User_UserID] = @User_UserID, [Mood] = @Mood, [JournalContent] = @JournalContent WHERE ID = @ID
 
END
GO


