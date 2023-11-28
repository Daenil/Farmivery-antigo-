----------------------------------------------------------------------------
-- Criando Database 
----------------------------------------------------------------------------
create database Farmivery
go


select * from Pessoas



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
	senha varchar(50) not null,
	telefone varchar(15) not null unique,
	dataNasc varchar(11) not null
)

-- Tabela Enderecos
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
	foreign key(idPessoa) references Pessoas(pessoasId),
	primary key(idPessoa, enderecoId)
)

--Tabela Farmac�utico
create table Farmaceuticos
(
	FarmaceuticoId int not null primary key references Pessoas(pessoasId),
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
	farmaciaId int not null identity,
	gerenteId int not null references Farmaceuticos(farmaceuticoId),
	nome varchar(50) not null,
	cnpj varchar(18) not null unique,
	primary key (farmaciaId, gerenteId)
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

--------------------------------------------------------------------------------
-- Criando Procedures
--------------------------------------------------------------------------------

-- 1 - Procedure para cadastrar entregadores
create procedure sp_cadEntregador
(
	@nomeEntregador varchar (50), 
	@emailEntregador varchar (50), 
	@senhaEntregador varchar(50), 
	@dataNascEntregador varchar(11), 
	@salarioEntregador money,
	@celularEntregador varchar(14),
	@rua varchar(50),
    @numero int,
    @bairro varchar(50),
    @cep varchar(10),
    @cidade varchar(50),
    @estado varchar(50)
)
as
begin 

	--Inserindo na tabela Pessoas
	insert into Pessoas (nome, email, senha, telefone, dataNasc)
	values (@nomeEntregador, @emailEntregador, @senhaEntregador, @celularEntregador, @dataNascEntregador)

	declare @idEntregador int
	set @idEntregador = SCOPE_IDENTITY();

	-- Inserindo na tabela Entregadores
	insert into Entregadores(entregadorId, entregador_salario)
	values (@idEntregador, @salarioEntregador)

	--Inserindo na tabela Endereco
	INSERT INTO Endereco (idPessoa, rua, numero, bairro, cep, cidade, estado)
    VALUES (@idEntregador, @rua, @numero, @bairro, @cep, @cidade, @estado);

end
go

--Testando Procedure sp_cadEntregador
exec sp_cadEntregador 'Rogério Mendonça', 'Rogerio@gmail.com', '123456','21-03-2001', 1300.00, '(11)992853242', 'rua testee', 56 , 'bairro teste', '15046460', 'cidade teste', 'estado teste'
select * from Pessoas
Select * from Entregadores
select * from Endereco



--  2- Procedure para cadastrar Clientes
create Procedure sp_cadCliente
(
	@nome varchar(50), 
	@emailCli	varchar(50), 
	@senhaCli varchar(50), 
	@celularCli varchar(15), 	
	@dataNascCli varchar(11),
	@rua varchar(50),
    @numero int,
    @bairro varchar(50),
    @cep varchar(10),
    @cidade varchar(50),
    @estado varchar(50)
)
as 
begin 

	-- Inserindo na tabela pessoas
	insert into Pessoas(nome, email, senha, telefone, dataNasc)
	values	(@nome, @emailCli, @senhaCli, @celularCli,  @dataNascCli)

	declare @idCli int
	set @idCli = SCOPE_IDENTITY();

	--Inserindo na tabela Clientes
	insert into Clientes(clienteId)
	values(@idCli)

	-- Inserindo na tabela Endereco
	INSERT INTO Endereco (idPessoa, rua, numero, bairro, cep, cidade, estado)
    VALUES (@idCli, @rua, @numero, @bairro, @cep, @cidade, @estado);
end
go

/*Testando Procedure
exec sp_cadCliente 'Daniel', 'rafa@hotmail.com', '1234','(17)991798353', '15-11-2003', 'rua testee', 56 , 'bairro teste', '15046460', 'cidade teste', 'estado teste'

select * from clientes
select * from Pessoas
select * from Endereco
*/

-- 3 - Procedure para cadastrar Farmaceuticos
create PROCEDURE sp_cadFarmaceuticos
(
	@nomeFarmaceutico varchar(50),
	@emailFarmaceutico varchar(50),
	@senhaFarmaceutico varchar(50),
	@dataNascFarmaceutico varchar(11),
	@nomeFarmacia varchar(50),
	@cnpjFarmacia varchar(18),
	@celularFarmaceutico varchar(14)
)
AS
BEGIN
    DECLARE @idFarmaceutico int;

    -- Inserir na tabela Pessoas
    INSERT INTO Pessoas (nome, email, senha, telefone, dataNasc)
    VALUES (@nomeFarmaceutico, @emailFarmaceutico, @senhaFarmaceutico, @celularFarmaceutico, @dataNascFarmaceutico);

    -- Obter o ID do Farmaceutico inserido
    SET @idFarmaceutico = SCOPE_IDENTITY();

    -- Inserir na tabela Farmaceuticos
    INSERT INTO Farmaceuticos (FarmaceuticoId)
    VALUES (@idFarmaceutico);

    -- Inserir na tabela Farmacias
    INSERT INTO Farmacias (gerenteId, nome, cnpj)
    VALUES (@idFarmaceutico, @nomeFarmacia, @cnpjFarmacia);
END;
go

-- Testeando Procedure
exec sp_cadFarmaceuticos 'Eduardo Gomes da silva Pereira', 'Farmatec@outlook.com', 'senha123', '11-11-1995', 'Farmatec', '13.345.678/0001-90','(17)993514992' 
select * from Farmacias
select * from Farmaceuticos
select * from Pessoas

-- 4 - procedure Endereco
create PROCEDURE sp_cadEndereco
(
    @rua varchar(50),
    @numero int,
    @bairro varchar(50),
    @cep varchar(10),
    @cidade varchar(50),
    @estado varchar(50)
)
AS
BEGIN
    declare @idPessoa int;

    -- Inserir na tabela Pessoas e obter o ID gerado
    INSERT INTO Pessoas (nome, dataNasc, email, senha, telefone) VALUES ('', '', '', '', '');
    set @idPessoa = SCOPE_IDENTITY();

    -- Inserir na tabela Endereco
    INSERT INTO Endereco (idPessoa, rua, numero, bairro, cep, cidade, estado)
    VALUES (@idPessoa, @rua, @numero, @bairro, @cep, @cidade, @estado);
END;
go

-- testando Procedure
exec sp_cadEndereco 'rua teste1', 35, 'Bairro teste1', '14044150', 'cidade teste1', 'estado teste1'
select * from Endereco
select * from Pessoas

-- 5 - procedure sp_CadFarmacia
create procedure sp_CadFarmacia
(
	@nomefarmacia varchar(50), 
	@cnpj varchar(18)
)
as
begin 
	DECLARE @idFarmaceutico int;

    -- Obter o ID do Farmaceutico inserido
    SET @idFarmaceutico = @@IDENTITY;

    -- Inserir na tabela Farmaceuticos
    INSERT INTO Farmaceuticos (FarmaceuticoId, salario)
    VALUES (@idFarmaceutico, 00.00);

    -- Inserir na tabela Farmacias
    INSERT INTO Farmacias (gerenteId, nome, cnpj)
    VALUES (@idFarmaceutico, @nomeFarmacia, @cnpj);
end
go
--Testando Procedure
exec sp_CadFarmacia 'Droga Raia', '11.111.111/1111-11'
select * from Farmacias
select * from Farmaceuticos

-- 6 - Procedure sp_CadEstoque
create procedure sp_CadEstoque
(
	@nome varchar(50),
    @descricao varchar(200),
    @preco decimal(10,2),
    @prod_qtd int,
    @imagem varchar(255)
)
AS
BEGIN
    INSERT INTO Produtos (nome, descricao, preco, prod_qtd, imagem)
    VALUES (@nome, @descricao, @preco, @prod_qtd, @imagem);
END;
go
-- testando procedure
exec sp_CadEstoque 'Dipirona', 'Bom para dor de cabeça', 45.50, 100, 'Imagem.png'
select * from Produtos


-- 7 - Procedure para Baixar o Estoque sempre que um produto for comprado
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
-- Testando procedure
exec sp_baixarEstoque 1, 10
select * from Produtos

-- 8 - Procedure para atualizar o estoque sempre que um produto for comprado
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
--Testando Procedure
exec sp_atualizarEstoque 1, 40
select * from Produtos

-- 9 - Procedure CadPedido
alter PROCEDURE sp_cadPedido
(
    @ped_data DATETIME,
    @ped_valor money,
    @status INT,
    @idCli int,
    @idEntregador int
)
AS
BEGIN
	set @ped_data = getdate()
    INSERT INTO Pedidos (ped_data, ped_valor, status, idCli, idEntregador)
    VALUES (@ped_data, @ped_valor, @status, @idCli, @idEntregador);
END;
go
-- Testando Procedure
exec sp_cadPedido 1,35.00, 1, 7, 6
select * from Pedidos

-- 10 - Procedure CadItens_Pedidos
CREATE PROCEDURE sp_cadItens_Pedidos
(
    @idPedido int,
    @idProduto int,
    @itp_qtd int,
    @itp_valor money
)
AS
BEGIN
    INSERT INTO itens_Pedidos (idCodigo, idProduto, itp_qtd, itp_valor)
    VALUES (@idPedido, @idProduto, @itp_qtd, @itp_valor);
END;
go
-- Testando Procedure
exec sp_cadItens_Pedidos 6, 1, 10, 500.00
select * from itens_Pedidos
-------------------------------------------------------------------------------------------
--Criando Views
-------------------------------------------------------------------------------------------
-- 1 - View Clientes
create view v_Clientes
as
	select P.nome, P.email, P.senha, P.telefone, P.dataNasc
	from Pessoas as P inner join Clientes C on P.pessoasId = C.clienteId
go
-- Executando View
select * from v_Clientes

-- 2 - View Entregadores
create view v_Entregadores
as
	select p.nome, p.email, p.senha,p.telefone, p.dataNasc, e.entregador_salario 'Salario'
	from Pessoas as P inner join Entregadores E on P.pessoasId = E.entregadorId
go
-- Executando View
select * from v_Entregadores

-- 3 - View Farmaceuticos
create view v_Farmaceuticos
as
	select p.pessoasId 'Pessoa_código', f.FarmaceuticoId [Farmaceutico_codigo],p.nome 'Nome', p.email [Email], p.senha Senha, p.telefone 'Telefone', p.dataNasc [Data_Nascimento], f.salario 'Salário'
	from Pessoas as P inner join Farmaceuticos F on P.pessoasId = F.FarmaceuticoId
go
-- Executando View
select * from v_Farmaceuticos

-- 4 - View Endereços
create view v_Enderecos
as
	select P.nome 'Nome', p.email [Email], p.telefone 'Telefone', e.rua, e.numero, e.bairro, e.cep, e.cidade, e.estado
	from Pessoas P inner join Endereco E on P.pessoasId = E.idPessoa
go
-- Executando View
select * from v_Enderecos

-- 5 - View Farmacias
create view v_Farmacias
as
	Select F.farmaciaId 'Farmácia_código', F.gerenteId [Gerente_código], f.nome 'Nome_Farmácia', f.cnpj cnpj
	from Farmacias as F
go

--Executando View
select * from v_Farmacias

-- 6 - View Farmacias_Farmaceuticos
create view v_Farmacias_Farmaceuticos
as
	select vf.Nome 'Nome_Farmaceutico', Vfar.Nome_Farmácia, vf.Email, vf.Telefone
	from v_Farmacias Vfar inner join v_Farmaceuticos Vf on vfar.Gerente_código = vf.Farmaceutico_codigo
go

--Executando View
select * from v_Farmacias_Farmaceuticos

-- 7 - View Produtos
create view v_Produtos
as 
	select p.produtoId [Produto_Código], p.nome 'Nome', P.descricao Descrição, p.preco 'Preco', p.prod_qtd 'Quantidade', p.imagem Imagem
	from Produtos as P
go
-- Executando view
select * from v_Produtos

-- 8 - View Pedidos
create view v_Pedidos
as 
	select p.pedidoId[Codigo_Pedido], p.idCli 'Codigo_Cliente', p.idEntregador Entregador_Codigo, p.ped_valor [Valor], p.ped_data Data_Pedido, p.status Status
	 FROM 
        Pedidos p
    INNER JOIN 
        Entregadores e ON p.idEntregador = e.entregadorId
    INNER JOIN 
        Clientes c ON p.idCli = c.clienteId;
go
-- Executando View
select * from v_Pedidos

--9 - View Itens_Pedidos
create view v_Itens_Pedidos
as
	select ip.idCodigo [Codigo_Pedido], ip.idProduto Codigo_Produto, ip.itp_qtd 'Quantidade', ip.itp_valor Valor_Pedido
	from itens_Pedidos IP inner join v_Pedidos Vp on ip.idCodigo = vp.Codigo_Pedido inner join v_Produtos Vpro on ip.idProduto = Vpro.Produto_Código
go

select * from v_Itens_Pedidos



