create database Farmivery
use Farmivery

create table Produtos
(
	ProdutoId	int			primary key		identity,
	Nome		varchar(60)	not null,
	Preco		decimal(10,2) not null
)
