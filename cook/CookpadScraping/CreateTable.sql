USE [Cookpad]
GO

/****** Object:  Table [dbo].[Recipe]    Script Date: 2016/07/15 10:10:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Recipe](
	[Url] [nvarchar](255) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Ingredients] [xml] NOT NULL,
	[Steps] [xml] NOT NULL,
	[Point] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Recipe] PRIMARY KEY CLUSTERED 
(
	[Url] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


