----------------------------------------------------------------------------
-- Criando Database 
----------------------------------------------------------------------------
create database Farmivery
go

--use master
select * from Produtos

----------------------------------------------------------------------------
-- Usando database 
----------------------------------------------------------------------------
use Farmivery
go

----------------------------------------------------------------------------
-- Criando tabelas
----------------------------------------------------------------------------

--Tabela Farmacias
create table Farmacias
(
	farmaciaId			int				not null	primary key		identity,
	email				varchar(100)	not null,
	senha				varchar(30)		not null,
	nome				varchar(50)		not null,
	cnpj				varchar(18)		not null,
	cep					varchar(10)		not null,
	numeroRua			int					null,
	cidade				varchar(100)	not null,
	estado				varchar(30)		not null,
	telefone			varchar(20)		not null	unique
)

--Tabela Farmac�utico
create table Farmaceuticos
(
	farmaceuticoId		int				not null		identity,
	idFarmacia			int				not null		references	Farmacias(farmaciaId),
	nomeFarmaceutico	varchar(100)	not null,
	telefone			varchar(20)		not null		unique,
	primary key(farmaceuticoId, idFarmacia),
)

--Tabela Entregadores
create table Entregadores
(
	entregadorId	int					not null		Primary Key identity,
	idFarmacia		int					not null		references	Farmacias(farmaciaId),
	nomeEntregador	varchar(100)		not null,
	telefone		varchar(20)			not null		unique,
	unique(entregadorId, idFarmacia)
)



--Tabela Clientes
create table Clientes
(
	clienteId		int					not null		primary key		identity,
	nomeCliente		varchar(100)		not null,
	email			varchar(100)		not null,
	senha			varchar(30)			not null,
	telefone		varchar(20)			not null		unique,
	cep				varchar(10)			not null,
	numeroCasa		int						null,
	cidade			varchar(100)		not null,
	estado			varchar(30)			not null
)

--Tabela Produtos
create table Produtos
(
	produtoId		int					not null		primary key		identity,
	idFarmacia		int					not null		references	Farmacias(Farmaciaid),
	nome			varchar(50)			not null,
	descricao		varchar(50)			not null,
	preco			decimal(10,2)		not null,
	prod_qtd		int					not null,
	imagem			varchar(255)		not null
)

--Tabela Pedidos
create table Pedidos
(
	pedidoId		INT					not null		Primary Key	IDENTITY,
	idCliente		int					not null		references	Clientes(clienteId),
	idProduto		int					not null		references	Produtos(produtoId),
	qtd				int					not null,
	tipoPagamento	int						null		check(tipoPagamento in (1, 2, 3))
)

----------------------------------------------------------------------
-- procedure
----------------------------------------------------------------------

-- 1 -  Procedure para Baixar o Estoque sempre que um produto for comprado
create Procedure sp_baixarEstoque
(
	@idProduto int, @qtdVendida int
)
as
begin
	update Produtos set prod_qtd = prod_qtd - @qtdVendida
	where produtoId = @idProduto and prod_qtd >= @qtdVendida
end
go