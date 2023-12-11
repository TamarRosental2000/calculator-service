USE [Calculator]
GO

/****** Object:  StoredProcedure [dbo].[STP_Get_Monthly_Operation]    Script Date: 11/12/2023 12:32:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[STP_Get_Monthly_Operation]
	@Operator NVARCHAR(1)
AS
BEGIN
	declare @OperationId int ;
	declare @Today DateTime= getUTCDate() ;

	SELECT @OperationId = OperationId
    FROM Operations
    WHERE Operator = @Operator;

	select * from CalculateMemory
	where @OperationId = OperationId
END;
GO

