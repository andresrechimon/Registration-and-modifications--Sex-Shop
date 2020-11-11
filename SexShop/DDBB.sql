use master
go
drop database LaConchitaQueTengo

create database LaConchitaQueTengo
go
use LaConchitaQueTengo

create table colores
(id_color int identity,
color varchar(25)
constraint pk_id_color primary key(id_color))

create table tipos
(id_tipo int identity,
tipo varchar(50)
constraint pk_id_tipo primary key(id_tipo))

create table juguetes
(cod_juguete int identity,
descripcion varchar(50),
id_tipo int,
precio decimal,
medida int,
id_color int
constraint pk_cod_juguete primary key(cod_juguete)
constraint fk_id_tipo foreign key(id_tipo)
references tipos(id_tipo),
constraint fk_id_color foreign key(id_color)
references colores(id_color)
)

insert into tipos values ('Anal'),('Anillos'),('Consoladores'),('Doble penetración'),('Fetiche'),('Muñecas')

insert into colores values ('Azul'),('Rojo'),('Negro'),('Neutro'),('Fluor')

insert into juguetes values ('Inexpulsable',1, 770, 1,1),
							('Plug',1,770,1,2),
							('Vibe plug',1,1570,2,3),
							('Cock ring',2,240,1,2),
							('Macizo Choclo',3,1500,1,2),
							('Pie grande',3,3780,2,3),
							('Doble Lesbian',4,2160,1,5),
							('Ball Gag',5,1430,1,1),
							('Fusta cuero',5,750,1,3),
							('Nipple Sucker',5,1435,1,3)


