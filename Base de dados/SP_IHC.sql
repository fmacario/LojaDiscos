-- inserir encomenda 
GO 
CREATE PROCEDURE InserirEncomenda
	@id_disco			int,
	@data1			date,
	@nif_fornecedor			int
AS

BEGIN
	INSERT INTO Encomendas (
		id_disco,
		nif_fornecedor,
		data_encomenda) VALUES(
			@id_disco,
			@nif_fornecedor,
			@data1)

END
-- Pesquisar encomenda por nome fornecedor
Go
Create procedure pesquisaEncomendaFornecedor
@nome varchar(50) OUTPUT
as
select @nome=nome 
from Pessoa As P JOIN Fornecedor AS F ON P.nif=F.nif JOIN Encomendas As E ON F.nif=E.nif_fornecedor


-- editar fornecedor
GO
CREATE PROCEDURE updateFornecedor
    @nif			int,
	@nome			varchar(50),
	@morada			varchar(50),
	@telefone			int,
	@email		    varchar(30)
AS
BEGIN TRANSACTION
		UPDATE Fornecedor SET nif=@nif From Fornecedor as F JOIN Pessoa as P ON F.nif = P.nif Where F.nif=@nif
		
        UPDATE Pessoa SET nif=@nif, nome=@nome, morada= @morada,telefone= @telefone, email=@email From Pessoa Where @nif=nif 
COMMIT


-- editar cliente
GO
CREATE PROCEDURE updateCliente
    @nif			int,
	@nome			varchar(50),
	@morada			varchar(50),
	@telefone			int,
	@email		    varchar(30)
AS
BEGIN TRANSACTION
		UPDATE Cliente SET nif=@nif From Cliente as C JOIN Pessoa as P ON C.nif = P.nif Where C.nif=@nif
		
        UPDATE Pessoa SET nif=@nif, nome=@nome, morada= @morada,telefone= @telefone, email=@email From Pessoa Where @nif=nif 
COMMIT


Create procedure registrarVenda
	@data_venda	date,
	@nif_funcionario int,
	@id_disco  int,
	@nif_cliente int
AS
BEGIN
	
	INSERT INTO Vendas (data_venda,
					   nif_funcionario,
					   id_disco
					   )
	VALUES (@data_venda,
			@nif_funcionario,
			@id_disco)

	INSERT INTO Compras(
			nif_cliente) 
			VALUES (@nif_cliente)

	END


-- inserir Fornecedor 
GO 
CREATE PROCEDURE InserirFornecedor
	@nif			int,
	@nome			varchar(50),
	@morada			varchar(50),
	@telefone			int,
	@email		    varchar(30)
AS
BEGIN
	INSERT INTO Pessoa (
		nif,
		nome,
		morada,
		telefone,
		email) VALUES(
			@nif,
			@nome,
			@morada,
			@telefone,
			@email)
		INSERT INTO Fornecedor(
			nif) 
			VALUES (@nif)

END

-- inserir cliente 
GO 
CREATE PROCEDURE InserirCliente
	@nif			int,
	@nome			varchar(50),
	@morada			varchar(50),
	@telefone			int,
	@email		    varchar(30)
AS
BEGIN
	INSERT INTO Pessoa (
		nif,
		nome,
		morada,
		telefone,
		email) VALUES(
			@nif,
			@nome,
			@morada,
			@telefone,
			@email)
		INSERT INTO Cliente(
			nif) 
			VALUES (@nif)

END
-- Pesquisar por Cliente_Venda
Go
Create procedure pesquisaClienteVenda
@pesquisa int,
@nome varchar(50) OUTPUT 
as
select @nome=nome 
from Pessoa As P JOIN Cliente As C ON P.nif = C.nif 
where C.nif = @pesquisa

-- Pesquisar por Disco Código
Go
Create procedure pesquisaDiscos
@pesquisa int,
@preço money OUTPUT,
@titulo varchar(30) OUTPUT,
@ano int OUTPUT,
@artista varchar(30) OUTPUT,
@genero varchar(30) OUTPUT,
@stock int OUTPUT
as
select @preço=preço, @titulo=titulo,@ano=ano, @artista=A.artista, @genero=G.genero, @stock=D.stock
from Discos As D JOIN Artistas AS A ON D.id_artista=A.id_artista JOIN Genero As G ON D.id_genero=G.id_genero
where id_disco = @pesquisa


-- Pesquisar por Disco ANO

Create procedure pesquisaDiscosAno
@pesquisa int,
@preço money OUTPUT,
@titulo varchar(30) OUTPUT,
@ano int OUTPUT,
@artista varchar(30) OUTPUT,
@genero varchar(30) OUTPUT,
@stock int OUTPUT
as
select @preço=preço, @titulo=titulo,@ano=ano, @artista=A.artista, @genero=G.genero, @stock=D.stock
from Discos As D JOIN Artistas AS A ON D.id_artista=A.id_artista JOIN Genero As G ON D.id_genero=G.id_genero
where ano = @pesquisa

-- inserir Artista
GO
CREATE PROCEDURE InserirArtista
       @artista						   VARCHAR(30)
AS
BEGIN
	SET NOCOUNT ON 
	INSERT INTO Artistas(artista) Values (@artista)

END

--pesquisar genero
GO
create procedure pesquisarGenero
@genero varchar(30),
@id_genero int OUTPUT
as
select @id_genero=G.id_genero from Genero as G
Where @genero=G.genero

--pesquisar tipo disco
GO
create procedure pesquisartipo
@tipo varchar(30),
@id_tipo int OUTPUT
as
select @id_tipo=T.id_tipo from Tipos_Discos as T
Where @tipo =T.tipo

--pesqusiar Artista
GO
Create procedure pesquisarArtista
@artista varchar(30),
@id_artista int OUTPUT
as
select @id_artista=id_artista from Artistas As A
where @artista = A.artista

--Inserir Disco
GO
CREATE PROCEDURE InserirDisco
       @id_disco	                   INT, 
       @titulo						   VARCHAR(30), 
       @preço						   money , 
       @artista                        VARCHAR(30), 
       @ano                            int, 
       @stock	                       int,
	   @genero						   varchar(30),
	   @tipo						   varchar(30)
	   --@imagem						 varbinary(max)              
AS 
BEGIN 
    
	 declare @id_art int
	 SET @id_art = SCOPE_IDENTITY()
	 exec pesquisarArtista @artista, @id_art
	 if @id_art is null
		begin
			exec InserirArtista @artista
			exec pesquisarArtista @artista, @id_art OUTPUT
		end

	 declare @id_gen int
	 exec pesquisarGenero @genero, @id_gen OUTPUT

	 declare @id_tipo int
	 exec pesquisartipo @tipo, @id_tipo OUTPUT

	 INSERT INTO Discos
          ( 
            id_disco                   ,
            titulo                     ,
            preço                      ,
            ano                        ,
			stock						,
			id_artista					,
			id_genero					,
			id_tipo						,			
			ImagemDisco
          ) 
     VALUES 
          ( 
            @id_disco                  ,
            @titulo                    ,
            @preço                     ,
            @ano                       ,
            @stock                 	,
			@id_art						,
			@id_gen					,
			@id_tipo		   ,
(SELECT BulkColumn  FROM Openrowset( Bulk 'C:\Imagens_Discos_IHC\noimage.png', Single_Blob) as img));
END 


-- atualizar stock
-- @update pode ser 1 ou -1, dependendo se é uma inserção ou uma venda
go
Create Procedure atualizarStock
	@id_disco	int,
	@update		int
as
	declare @stock int
	SELECT @stock = D.stock FROM Discos As D WHERE @id_disco = D.id_disco
	
	declare @value int
	SET @value = @stock + @update
	UPDATE Discos SET stock = @value WHERE @id_disco = id_disco