USE [mapdb]
GO
/****** Object:  StoredProcedure [dbo].[SP_CREATE_CUSTOMER]    Script Date: 11/27/2023 7:29:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CREATE_CUSTOMER]
@custName CHAR (30), @email CHAR (20), @phone VARCHAR(50)
AS
BEGIN
    INSERT  INTO CUSTOMERTABLE ([CUSTID], [NAME], EMAIL, PHONE)
    VALUES                    (NEWID(), @custName, @email, @phone);
END

GO
