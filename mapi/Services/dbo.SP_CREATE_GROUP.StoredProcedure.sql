USE [mapdb]
GO
/****** Object:  StoredProcedure [dbo].[SP_CREATE_GROUP]    Script Date: 11/27/2023 7:29:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CREATE_GROUP]
@GROUPID NCHAR (10), @GROUPNAME NCHAR (30)
AS
BEGIN
    INSERT  INTO [dbo].[USERGROUP] ([RECID], [GROUPID], [GROUPNAME])
    VALUES                        (REPLACE(CONVERT (NVARCHAR, GETDATE(), 112) + CONVERT (VARCHAR, GETDATE(), 114), ':', ''), @GROUPID, @GROUPNAME);
END

GO
