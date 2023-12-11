USE [Calculator]
GO

/****** Object:  StoredProcedure [dbo].[STP_Insert_Result]    Script Date: 11/12/2023 12:33:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[STP_Insert_Result]
    @FieldA NVARCHAR(80),
    @FieldB NVARCHAR(80),
	@Operator NVARCHAR(1),
	@Result NVARCHAR(1200)
AS
BEGIN
    DECLARE @Counter INT = 0;
	declare @OperatorId int ;
	declare @Count int ;
	declare @Today DateTime= getDate() ;
	declare @LastUpdate DateTime ;

	SELECT @OperatorId = OperationId,
           @Count = ISNULL(CountLastMonth, 0),
		   @LastUpdate= LastUpdate
    FROM Operations
    WHERE Operator = @Operator;

	--If Update Last Month
	if(DATEDIFF(month, @LastUpdate,@Today)>=1)
	set	@Count =0

	INSERT INTO CalculateMemory (FieldA, FieldB, OperationId, CalculateDate,Result)
    VALUES (@FieldA, @FieldB, @OperatorId, @Today,@Result);

	update Operations SET LastUpdate = @Today, CountLastMonth = @Count +1 where OperationId=@OperatorId 
	select @OperatorId as OperatorId
END;
GO

