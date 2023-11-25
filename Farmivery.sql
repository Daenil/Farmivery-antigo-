----------------------------------------------------------------------------
-- Criando Database 
----------------------------------------------------------------------------
create database Farmivery
go

=




--use master

----------------------------------------------------------------------------
-- Usando database 
----------------------------------------------------------------------------
use Farmivery
go

----------------------------------------------------------------------------
-- Criando tabelas
----------------------------------------------------------------------------
--Tabela Pessoas
create table Pessoas
(
	pessoasId int not null primary key identity, 
	nome varchar(50) not null, 
	email varchar(50) not null unique,
	senha varchar(50) not null unique,
	telefone varchar(15) not null unique,
	dataNasc varchar(11) not null
)


create table Endereco
(
	idPessoa int not null references Pessoas(pessoasId),
	enderecoId int not null identity,
	rua varchar(50) not null,
	numero int not null,
	bairro varchar(50) not null,
	cep varchar(10) not null,
	cidade varchar(50) not null,
	estado varchar(50) not null,
	foreign key(idPessoa) references Pessoas(pessoasId)
)

--Tabela Farmac�utico
create table Farmaceuticos
(
	FarmaceuticoId int not null primary key references Pessoas(pessoasId),
	salario decimal(10,2) not null
)

--Tabela Entregadores
create table Entregadores
(
	entregadorId int not null primary key references Pessoas(pessoasId),
	entregador_salario decimal(10,2) not null,
)	


--Tabela Farmacias
create table Farmacias
(
	farmaciaId int not null primary key identity,
	nome varchar(50) not null,
	cnpj varchar(18) not null,
)


--Tabela Clientes
create table Clientes
(
	clienteId int not null primary key,
	foreign key(clienteId) references Pessoas(pessoasId)
)

--Tabela Produtos
create table Produtos
(
	produtoId int not null primary key identity,
	nome varchar(50) not null,
	descricao varchar(50) not null,
	preco decimal(10,2) not null,
	prod_qtd int not null,
	imagem varchar(255) not null
)

--Tabela Pedidos
create table Pedidos
(
	pedidoId INT NOT NULL primary key IDENTITY,
	ped_data DATETIME,
	ped_valor DECIMAL(10, 2) CHECK (ped_valor > 0),
	status INT NULL CHECK (status IN (1, 2, 3)),
	idCli int not null references Clientes(clienteId),
	idEntregador int not null references Entregadores(entregadorId)
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
	@nomeEntregador varchar (50), @emailEntregador varchar (50), @senhaEntregador varchar(50), @dataNascEntregador varchar(11), @salarioEntregador money,
	@celularEntregador varchar(14)
)
as
begin 
	insert into Pessoas (nome, email, senha, telefone, dataNasc)
	values (@nomeEntregador, @emailEntregador, @senhaEntregador, @celularEntregador, @dataNascEntregador)

	declare @idEntregador int
	set @idEntregador = @@identity

	insert into Entregadores(entregadorId, entregador_salario)
	values (@idEntregador, @salarioEntregador)

end

--Testando Procedure sp_cadEntregador
---exec sp_cadEntregador 'Rog�rio Mendon�a', '333-333-333-33', '07-01-1999', '5000.00'

--select * from Pessoas
--Select * from Entregadores



-- Procedure para cadastrar Clientes
create Procedure sp_cadCliente
(
	@nome varchar(50), @emailCli	varchar(50), @senhaCli varchar(50), @celularCli varchar(15), 	@dataNascCli varchar(11)
)
as 
begin 
	insert into Pessoas(nome, email, senha, telefone, dataNasc)
	values	(@nome, @emailCli, @senhaCli, @celularCli,  @dataNascCli)

	declare @idCli int
	set @idCli = @@IDENTITY

	insert into Clientes(clienteId)
	values(@idCli)

go


-- Testando Procedure
--exec sp_cadCliente 'Daniel', 'rafa@hotmail.com', '1234', '15-11-2003', '(17)991798353'

--select * from clientes
--select * from Pessoas
--select * from Telefones go

-- Procedure para cadastrar Farmaceuticos
create Procedure sp_cadFarmaceuticos
(
	@nomeFarmaceutico varchar(50),	@emailFarmaceutico varchar(50), @senhaFarmaceutico varchar(50), @dataNascFarmaceutico varchar(11), @salarioFarmaceutico decimal(10,2),
	@nomeFarmacia varchar(50), @cnpjFarmacia varchar(18), @celularFarmaceutico varchar(14)
)
as
begin
	insert into Pessoas(nome, email, senha, telefone,dataNasc)
	values (@nomeFarmaceutico, @emailFarmaceutico, @senhaFarmaceutico, @celularFarmaceutico, @dataNascFarmaceutico)


	declare @idFarmaceutico int
	set @idFarmaceutico = @@identity

	insert into Farmaceuticos(FarmaceuticoId, salario)
	values (@idFarmaceutico, @salarioFarmaceutico)


	declare @idFarmacia int 
	set @idFarmacia = @@identity
	insert  into Farmacias(nome, cnpj)
	values (@nomeFarmacia, @cnpjFarmacia)
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

-------------------------------------------------------------------------------------------
--Criando Views
-------------------------------------------------------------------------------------------
create view v_Clientes
as
	select P.nome, P.email, P.senha, P.telefone, P.dataNasc
	from Pessoas P, Clientes C
	where P.pessoasId = C.clienteId
go

select v_Clientes.email, v_Clientes.senha from v_Clientes

