USE [Target]
GO

/****** Object:  Table [dbo].[Plano]    Script Date: 04/01/2022 23:03:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Plano](
	[PlanoId] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [varchar](30) NOT NULL,
	[Descricao] [varchar](255) NOT NULL,
	[Preco] [money] NOT NULL,
	[CriadoEm] [datetime] NOT NULL,
 CONSTRAINT [PK_Plano] PRIMARY KEY CLUSTERED 
(
	[PlanoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

