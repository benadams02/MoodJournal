USE [MoodJournal]
GO

/****** Object:  StoredProcedure [dbo].[spInsertJournal]    Script Date: 11/11/2022 15:32:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[spInsertJournal]
(@ID uniqueidentifier,
@DateCreated datetime,
@DateModified datetime,
@User_UserID uniqueidentifier,
@Mood varchar(max),
@JournalContent varchar(max))
AS
BEGIN
SET NOCOUNT ON
 
 Insert Into [dbo].[Journal]([ID], [DateCreated], [DateModified], [User_UserID], [Mood], [JournalContent]) values (@ID,@DateCreated,@DateModified,@User_UserID, @Mood, @JournalContent) 
 
END
GO


