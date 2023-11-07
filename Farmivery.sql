create database Farmivery
use Farmivery

create table Produtos
(
	produtoId	int				not null	primary key		identity,
	nome		varchar(20)		not null,
	descricao	varchar(350)	not null,
	preco		decimal(10,2)	not null,
	prod_qtd	int				not null,
	imagem		varchar(255)	null 	default 'download.jpg'
)

select * from Produtos