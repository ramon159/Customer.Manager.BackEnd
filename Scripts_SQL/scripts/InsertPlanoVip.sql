USE [Target]
GO

INSERT INTO [dbo].[Plano]
           ([Titulo]
           ,[Descricao]
           ,[Preco]
           ,[CriadoEm])
     VALUES
           ('Plano VIP'
           ,'O plano vip vai possibilitar o cliente a ter um rob� que avisar� sobre tend�ncias de investimentos, e sugerir a compra de a��es de uma determinada empresa.'
           ,50.00
           ,GETDATE())
GO

