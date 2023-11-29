----------------------------------------------------------------------------
-- Criando Database 
----------------------------------------------------------------------------
create database Farmivery
go

--use master

select * from Clientes

----------------------------------------------------------------------------
-- Usando database 
----------------------------------------------------------------------------
use Farmivery
go

----------------------------------------------------------------------------
-- Criando tabelas
----------------------------------------------------------------------------
--Tabela Pessoas

--Tabela Farmac�utico
create table Farmaceuticos
(
	farmaceuticoId		int				not null	primary key		identity,
	nomeFarmaceutico	varchar(100)	not null,
	email				varchar(100)	not null,
	senha				varchar(30)		not null,
	telefone			varchar(20)		not null,
	cep					varchar(10)		not null,
	numeroCasa			int					null,
	cidade				varchar(100)	not null,
	estado				varchar(30)		not null
)

--Tabela Farmacias
create table Farmacias
(
	farmaciaId			int				not null	primary key		identity,
	nome				varchar(50)		not null,
	cnpj				varchar(18)		not null,
)


--Tabela Clientes
create table Clientes
(
	clienteId		int				not null	primary key		identity,
	nomeCliente		varchar(100)	not null,
	email			varchar(100)	not null,
	senha			varchar(30)		not null,
	telefone		varchar(20)		not null,
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
	imagem			varchar(255)	not null,
)

--Tabela Pedidos
create table Pedidos
(
	pedidoId INT NOT NULL primary key IDENTITY,
	ped_data DATETIME,
	ped_valor DECIMAL(10, 2) CHECK (ped_valor > 0),
	status INT NULL CHECK (status IN (1, 2, 3)),
	idCli int not null references Clientes(clienteId),
)

--Tabela itens_pedidos
create table itens_Pedidos
(
	idCodigo int not null references Pedidos(pedidoId),
	idProduto int not null references Produtos(produtoId),
	itp_qtd int not null check(itp_qtd > 0),
	itp_valor money not null check(itp_valor > 0), 
	primary key(idCodigo, idProduto)
)


--Procedure para cadastrar entregadores
create procedure sp_cadEntregador
(
	@nomeEntregador varchar (50), @cpfEntregador varchar (14), @dataNascEntregador varchar(11), @salarioEntregador money
)
as
begin 
	insert into Pessoas (pes_nome, pes_cpf, pes_dataNasc)
	values (@nomeEntregador, @cpfEntregador, @dataNascEntregador)

	declare @idEntregador int
	set @idEntregador = @@identity

	insert into Entregadores(entregadorId, entregador_salario)
	values (@idEntregador, @salarioEntregador)
end

--Testando Procedure sp_cadEntregador
--exec sp_cadEntregador 'Rog�rio Mendon�a', '333-333-333-33', '07-01-1999', '5000.00'

--select * from Pessoas
--Select * from Entregadores



-- Procedure para cadastrar Clientes
create Procedure sp_cadCliente
(
	@nome varchar(50), @cpfCli	varchar(14),	@dataNascCli varchar(11), @celularCli varchar(14)
)
as 
begin 
	insert into Pessoas(pes_nome, pes_cpf, pes_dataNasc)
	values	(@nome, @cpfCli, @dataNascCli)

	declare @idCli int
	set @idCli = @@IDENTITY

	insert into Clientes(clienteId, cli_celular)
	values(@idCli, @celularCli)
end
go


-- Testando Procedure
--exec	sp_cadCliente 'Daniel', '111-111-111-11', '15-11-2003', '(17)991798353'

--select * from clientes
--select * from Pessoas go

-- Procedure para cadastrar Farmaceuticos
create Procedure sp_cadFarmaceuticos
(
	@nomeFarmaceutico varchar(50),	@cpfFarmaceutico varchar(15), @dataNascFarmaceutico varchar(11), @salarioFarmaceutico decimal(10,2)
)
as
begin
	insert into Pessoas(pes_nome, pes_cpf, pes_dataNasc)
	values (@nomeFarmaceutico, @cpfFarmaceutico, @dataNascFarmaceutico)

	declare @idFarmaceutico int
	set @idFarmaceutico = @@identity

	insert into Farmaceuticos(FarmaceuticoId, salario)
	values (@idFarmaceutico, @salarioFarmaceutico)
end
go
-- Testeando Procedure
--exec sp_cadFarmaceuticos 'Eduardo Gomes da silva Pereira', '222-222-222-22', '23-10-1994', 2300.00


--select * from Farmaceuticos
--select*from Pessoas go

-- Procedure para Baixar o Estoque sempre que um produto for comprado
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

--Procedure para atualizar o estoque sempre que um produto for comprado
create procedure sp_atualizarEstoque
(
	@idProduto int, @qtdComprada int
)
as 
begin
	if @qtdComprada >= 0
		update Produtos set prod_qtd = prod_qtd - @qtdComprada
		where produtoId = @idProduto
end
go

