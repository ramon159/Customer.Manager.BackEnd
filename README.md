# Desafio Back-End TARGET 
API ASP.NET Core 3.1
___
## 🚀 Começando

Essas instruções permitirão que você obtenha uma cópia do projeto em operação na sua máquina local para fins de desenvolvimento e teste.

Consulte **Implantação** para saber como implantar o projeto.

### 📋 Pré-requisitos

* banco de dados SQL Server
```
executar o script tudo.sql da pasta \Scripts_SQL para gerar o banco de dados, 
as tabelas e as inserções
```

### 🔧 Instalação

Antes de tudo é necessário definir a sql string:
no arquivo de variaveis de ambiente \Target.Backend.Web\appsettings.Development.json
altere o Data Source para o seu servidor SQL Server
```
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=YourServer;Initial Catalog=Target;Integrated Security=True"
  },
```
# API endpoints
---
## clientes
| verbo | rota | descrição |
|---|---|---|
| [GET] | /api/v1/clientes | retorna lista de clientes |
| [GET] | /api/v1/clientes/:id | retorna uma cliente com base no id |
| [POST] | /api/v1/clientes | cadastra um cliente |
| [GET] | /api/v1/clientes/:id/endereco | retorna o endereço do cliente pelo id |
| [PUT] | /api/v1/clientes/:id/endereco | atualiza o endereço com base no id do cliente |
| [GET] | /api/v1/clientes/rendamensal/:rendaMensal | retorna uma lista de clientes com renda superior a informada |
| [GET] | /api/v1/clientes/:id/confirmarplanovip | confirma o plano vip do cliente pelo id |
## estados
| verbo | rota | descrição |
|---|---|---|
| [GET] | /api/v1/estados | retorna lista com todos os estados brasileiros |
| [GET] | /api/v1/estados/:uf/cidades | retorna lista com todas as cidades brasileiras com base no UF |
## planovip
| verbo | rota | descrição |
|---|---|---|
| [GET] | /api/v1/planovip | retorna detelhas do plano vip |
| [GET] | /api/v1/planovip/indice | retorna indice com o total de clientes que tem renda superior a 6000 reais e que ainda não possuem plano vip |