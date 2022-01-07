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

