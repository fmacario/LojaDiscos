
CREATE TABLE Genero(
	 id_genero int IDENTITY(1,1) NOT NULL,
	 genero varchar(50),
	 PRIMARY KEY (id_genero)
 );
 
 
 CREATE TABLE Tipos_Discos(
	 id_tipo int IDENTITY(1,1)   NOT NULL,
	 tipo varchar(50),
	 PRIMARY KEY (id_tipo)
 );
 
 CREATE TABLE Pessoa(
	 nif int NOT NULL,
	 nome varchar(50) NOT NULL,
	 telefone int NOT NULL,
	 morada varchar(50) NOT NULL,
	 email varchar(30)NOT NULL,
	 
	 PRIMARY KEY (nif),
	
);

 CREATE TABLE Fornecedor(
	 nif int NOT NULL,
	 FOREIGN KEY (nif) REFERENCES Pessoa(nif),
	 PRIMARY KEY (nif)
 );
 
  CREATE TABLE Funcionario(
	 nif int NOT NULL,
	 FOREIGN KEY (nif) REFERENCES Pessoa(nif),
	 PRIMARY KEY (nif)
 );
 
  CREATE TABLE Cliente(
	 nif int NOT NULL,
	 FOREIGN KEY (nif) REFERENCES Pessoa(nif),
	 PRIMARY KEY (nif)
 );
 

 CREATE TABLE Artistas(
	 id_artista int IDENTITY(1,1) NOT NULL,
	 artista varchar(50),
	 PRIMARY KEY (id_artista)
	 
 );
CREATE TABLE Discos(
	 id_disco int NOT NULL,
	 preço   money  NOT NULL,
	 stock   int  NOT NULL,
	 titulo   varchar (30) NOT NULL,
	 ano   int  NOT NULL,
	 id_artista   int  NOT NULL,
	 id_genero   int  NOT NULL,
	 id_tipo   int  NOT NULL,
	 imagemDisco   varbinary (max) NULL,
	 PRIMARY KEY (id_disco),
     FOREIGN KEY (id_genero) REFERENCES Genero(id_genero),
	 FOREIGN KEY (id_tipo) REFERENCES Tipos_Discos(id_tipo),
	 FOREIGN KEY (id_artista) REFERENCES Artistas(id_artista)
	 
);

CREATE TABLE Encomendas(
	 id_encomenda int IDENTITY(1,1)  NOT NULL,
	 data_encomenda date,
	 nif_fornecedor int,
	 id_disco int,
	 FOREIGN KEY (nif_fornecedor) REFERENCES Fornecedor(nif),
	 FOREIGN KEY (id_disco) REFERENCES Discos(id_disco),
	 PRIMARY KEY (id_encomenda)
 );
 
CREATE TABLE Vendas(
	 id_venda int IDENTITY(1,1) NOT NULL,
	 data_venda date NOT NULL,
	 nif_funcionario int NOT NULL,
	 id_disco int NOT NULL,
	 
	 PRIMARY KEY (id_venda),
	 FOREIGN KEY (nif_funcionario) REFERENCES Funcionario(nif),
	 FOREIGN KEY (id_disco) REFERENCES Discos(id_disco)
	  
);

CREATE TABLE Login(
	 UserID INT IDENTITY(1,1) NOT NULL,
	 username varchar(50)  NOT NULL,
	 passwordHash varchar(50) NOT NULL,
	 nif_funcionario int NOT NULL,
	 FOREIGN KEY (nif_funcionario) REFERENCES Pessoa(nif),
	 PRIMARY KEY (UserID)
 ); 

CREATE TABLE [dbo].Compras
(
	[Id_compra] INT NOT NULL PRIMARY KEY, 
    [nif_cliente] INT NOT NULL,
	FOREIGN KEY (nif_cliente) REFERENCES Cliente(nif)
)
