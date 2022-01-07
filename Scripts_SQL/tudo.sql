USE [master]
GO

/****** Object:  Database [Target]    Script Date: 04/01/2022 23:02:39 ******/
CREATE DATABASE [Target]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Target', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Target.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Target_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Target_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Target].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [Target] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [Target] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [Target] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [Target] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [Target] SET ARITHABORT OFF 
GO

ALTER DATABASE [Target] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [Target] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [Target] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [Target] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [Target] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [Target] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [Target] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [Target] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [Target] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [Target] SET  DISABLE_BROKER 
GO

ALTER DATABASE [Target] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [Target] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [Target] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [Target] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [Target] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [Target] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [Target] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [Target] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [Target] SET  MULTI_USER 
GO

ALTER DATABASE [Target] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [Target] SET DB_CHAINING OFF 
GO

ALTER DATABASE [Target] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [Target] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [Target] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [Target] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [Target] SET QUERY_STORE = OFF
GO

ALTER DATABASE [Target] SET  READ_WRITE 
GO

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

USE [Target]
GO

INSERT INTO [dbo].[Plano]
           ([Titulo]
           ,[Descricao]
           ,[Preco]
           ,[CriadoEm])
     VALUES
           ('Plano VIP'
           ,'O plano vip vai possibilitar o cliente a ter um robô que avisará sobre tendências de investimentos, e sugerir a compra de ações de uma determinada empresa.'
           ,50.00
           ,GETDATE())
GO

