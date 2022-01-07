USE [Target]
GO

/****** Object:  Table [dbo].[ClienteEndereco]    Script Date: 04/01/2022 23:03:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ClienteEndereco](
	[ClienteEnderecoId] [int] IDENTITY(1,1) NOT NULL,
	[Logradouro] [varchar](50) NOT NULL,
	[Bairro] [varchar](50) NOT NULL,
	[Cidade] [varchar](50) NOT NULL,
	[Complemento] [varchar](50) NULL,
	[UF] [char](2) NOT NULL CHECK ([UF] IN('RO', 'AC', 'AM', 'RR', 'PA', 'AP', 'TO', 'MA', 'PI', 'CE', 'RN', 'PB', 'PE', 'AL', 'SE', 'BA', 'MG', 'ES', 'RJ', 'SP', 'PR', 'SC', 'RS', 'MS', 'MT', 'GO', 'DF')),
	[CEP] [varchar](8) NOT NULL,
	[ClienteId] [int] NOT NULL,
	[CriadoEm] [datetime] NOT NULL,
 CONSTRAINT [PK_ClienteEndereco] PRIMARY KEY CLUSTERED 
(
	[ClienteEnderecoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ClienteEndereco]  WITH CHECK ADD  CONSTRAINT [FK_ClienteEndereco_Cliente] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Cliente] ([ClienteId])
GO

ALTER TABLE [dbo].[ClienteEndereco] CHECK CONSTRAINT [FK_ClienteEndereco_Cliente]
GO

