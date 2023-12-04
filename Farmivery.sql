----------------------------------------------------------------------------
-- Criando Database 
----------------------------------------------------------------------------
create database Farmivery
go

--use master

--select * from Produtos

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
	clienteId		int				not null	primary key		identity,
	nomeCliente		varchar(100)	not null,
	email			varchar(100)	not null,
	senha			varchar(30)		not null,
	telefone		varchar(20)		not null unique,
	cep				varchar(10)		not null,
	numeroCasa		int					null,
	cidade			varchar(100)	not null,
	estado			varchar(30)		not null
)

--Tabela Produtos
create table Produtos
(
	produtoId		int				not null	primary key		identity,
	nome			varchar(50)		not null,
	descricao		varchar(50)		not null,
	preco			decimal(10,2)	not null,
	prod_qtd		int				not null,
	imagem			varchar(255)	not null
)

--Tabela Pedidos
create table Pedidos
(
	pedidoId		INT				NOT NULl	Primary Key	IDENTITY,
	ped_data		DATETIME,
	ped_valor		DECIMAL(10, 2)	CHECK(ped_valor > 0),
	status			INT				NULL CHECK(status IN (1, 2, 3)),
	idCli			int				not null	references	Clientes(clienteId),
	idEntregador	int				not null	references	Entregadores(entregadorId),
)

--Tabela itens_pedidos
create table itens_Pedidos
(
	idCodigo	int		not null	references Pedidos(pedidoId),
	idProduto	int		not null	references Produtos(produtoId),
	itp_qtd		int		not null	check(itp_qtd > 0),
	itp_valor	money	not null	check(itp_valor > 0), 
	primary key(idCodigo, idProduto)
)

----------------------------------------------------------------------
-- procedures
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

-- 2 - Create Procedure Cadastrar Farmaceutico
create Procedure sp_CadFarmaceuticos
(
	@idFarmacia int, @nomeFarmaceutico varchar(50), @telefoneFarmaceutico varchar(20)
)
as
begin
	insert into Farmaceuticos (idFarmacia, nomeFarmaceutico, telefone)
	values (@idFarmacia, @nomeFarmaceutico, @telefoneFarmaceutico)
end
go

-- 3 - Create Procedure Cadastrar Entregadores
create Procedure sp_CadEntregadores
(
	@idFarmacia int, @nomeEntregador varchar(50), @telefoneEntregador varchar(20)
)
as
begin
	
	insert into Entregadores (idFarmacia, nomeEntregador, telefone)
	values (@idFarmacia, @nomeEntregador, @telefoneEntregador)
end
go


----------------------------------------------------------------------------------------
-- Views
----------------------------------------------------------------------------------------
-- 1 - create view farmacias e farmaceuticos
create view v_Farmacias_Farmaceuticos
as
	select f.farmaceuticoId, f.idFarmacia,  F.nomeFarmaceutico, f.telefone
	from Farmacias far inner join Farmaceuticos F on Far.farmaciaId = f.idFarmacia
go
-- Testando View
select * from v_Farmacias_Farmaceuticos

-- 2 - create view Farmacias e Entregadores
create view v_Farmacias_Entregadores
as
	select e.entregadorId, e.idFarmacia, e.nomeEntregador, e.telefone
	from Farmacias F inner join Entregadores E on F.farmaciaId = E.idFarmacia
go
-- Testando View
select * from v_Farmacias_Entregadores


--3 - View Itens_Pedidos
create view v_Itens_Pedidos
as
	select ip.idCodigo [Código_Pedido], ip.idProduto Código_Produto, Pro.nome [Nome_Produto], ip.itp_qtd 'Quantidade', ip.itp_valor Valor_Pedido
	from itens_Pedidos IP 
	inner join 
	Pedidos P  on ip.idCodigo = P.pedidoId 
	inner join 
	Produtos Pro on ip.idProduto = pro.produtoId
go
-- Testando View
select * from v_Itens_Pedidos

-- 4 - View Pedidos
create View v_Pedidos
as
	select	p.pedidoId Código_Pedido, P.idCli Código_Cliente, c.nomeCliente Nome_Cliente, p.idEntregador Código_Entregador, e.nomeEntregador[Nome_Entregador], p.ped_valor 'Valor', p.ped_data [Data_Pedido],
			case status
						when 1 then 'Andamento'
						when 2 then 'Entregue'
						when 3 then	'Cancelado'
						else		'Indisponível'
					end 'situação'
	from Pedidos P
	inner join
	Entregadores E on p.idEntregador = E.entregadorId
	inner join
	Clientes C on p.idCli = C.clienteId
go
-- Testando View 
select * from v_Pedidos