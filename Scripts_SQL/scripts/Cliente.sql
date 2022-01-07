USE [Target]
GO

/****** Object:  Table [dbo].[Cliente]    Script Date: 04/01/2022 23:02:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cliente](
	[ClienteId] [int] IDENTITY(1,1) NOT NULL,
	[NomeCompleto] [varchar](150) NOT NULL,
	[DataNascimento] [date] NOT NULL,
	[CPF] [varchar](11) NOT NULL,
	[PlanoId] [int] NULL,
	[RendaMensal] [money] NOT NULL,
	[CriadoEm] [datetime] NOT NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[ClienteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Cliente]  WITH CHECK ADD  CONSTRAINT [FK_Cliente_Plano] FOREIGN KEY([PlanoId])
REFERENCES [dbo].[Plano] ([PlanoId])
GO

ALTER TABLE [dbo].[Cliente] CHECK CONSTRAINT [FK_Cliente_Plano]
GO

