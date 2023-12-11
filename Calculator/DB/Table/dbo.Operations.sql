USE [Calculator]
GO

/****** Object:  Table [dbo].[Operations]    Script Date: 11/12/2023 12:34:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Operations](
	[OperationId] [int] IDENTITY(1,1) NOT NULL,
	[Operator] [nvarchar](1) NOT NULL,
	[LastUpdate] [datetime2](7) NOT NULL,
	[CountLastMonth] [int] NOT NULL,
 CONSTRAINT [PK_Operations] PRIMARY KEY CLUSTERED 
(
	[OperationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

