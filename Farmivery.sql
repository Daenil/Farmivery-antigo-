create database Farmivery
use Farmivery

create table Produtos
(
	produtoId	int				primary key		identity,
	descricao	varchar(60)		not null,
	preco		decimal(10,2)	not null,
	prod_qtd	int				not null
)
