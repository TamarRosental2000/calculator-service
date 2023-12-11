USE [Calculator]
GO

/****** Object:  Table [dbo].[CalculateMemory]    Script Date: 11/12/2023 12:34:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CalculateMemory](
	[ItemId] [int] IDENTITY(1,1) NOT NULL,
	[FieldA] [nvarchar](80) NOT NULL,
	[FieldB] [nvarchar](80) NOT NULL,
	[Result] [nvarchar](1200) NOT NULL,
	[OperationId] [int] NOT NULL,
	[CalculateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_CalculateMemory] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CalculateMemory]  WITH CHECK ADD  CONSTRAINT [FK_CalculateMemory_Operations_OperationId] FOREIGN KEY([OperationId])
REFERENCES [dbo].[Operations] ([OperationId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[CalculateMemory] CHECK CONSTRAINT [FK_CalculateMemory_Operations_OperationId]
GO

