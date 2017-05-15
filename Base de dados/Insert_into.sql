 
Insert into Artistas values ('Xutos e Pontapés');
Insert into Artistas values ('Red Hot Chili Peppers');
Insert into Artistas values ('The Beatles');
Insert into Artistas values ('Metallica');
Insert into Artistas values ('Amália');

 
Insert into Genero values ('Rock');
Insert into Genero values ('Pop');
Insert into Genero values ('Metal');
Insert into Genero values ('Fado');

 
Insert into Tipos_Discos values ('CD');
Insert into Tipos_Discos values ('Vinil');
Insert into Tipos_Discos values ('DVD');
Insert into Tipos_Discos values ('BLU-RAY');

Insert into Pessoa values (98372344,'Antonio Oliveira Salazar', 222771970, 'Santa Comba Dão', 'cadeiramaldita@gmail.com');
Insert into Pessoa values (04392934,'Aníbal Cavaco Silva',211771939, 'Boliqueime - Loulé', 'cavaco@gmail.com');
Insert into Pessoa values (24543495,'Álvaro Barreirinhas Cunhal', 221362005, 'Coimbra', 'PCP@gmail.com');
Insert into Pessoa values (12342545,'Luís de Camões', 221061524, 'Lisboa', 'umolho@gmail.com');

 
Insert into Funcionario values (98372344);
Insert into Cliente values (04392934);
Insert into Fornecedor values (24543495);
Insert into Funcionario values (12342545);



Insert into Discos values (12345, 19.99, 50, 'Circo de Feras', 1987, 1, 1, 1, (SELECT BulkColumn 
FROM Openrowset( Bulk 'C:\Imagens_Discos_IHC\circo.jpeg', Single_Blob) as img));
Insert into Discos values (99999,39.99, 30, 'Californication', 1999, 2, 1, 3, (SELECT BulkColumn 
FROM Openrowset( Bulk 'C:\Imagens_Discos_IHC\californication.jpg', Single_Blob) as img));
Insert into Discos values (43242,49.99, 20, 'Let It Be', 1970, 3, 2, 2, (SELECT BulkColumn 
FROM Openrowset( Bulk 'C:\Imagens_Discos_IHC\let.jpg', Single_Blob) as img));
Insert into Discos values (11111,24.99, 15, 'Master of Puppets', 1986, 4, 3, 4, (SELECT BulkColumn 
FROM Openrowset( Bulk 'C:\Imagens_Discos_IHC\master.jpg', Single_Blob) as img));
Insert into Discos values (23435,14.99, 55, 'Gostava de Ser Quem Era', 1980, 5, 4, 1, (SELECT BulkColumn 
FROM Openrowset( Bulk 'C:\Imagens_Discos_IHC\amalia.jpg', Single_Blob) as img));

Insert into Encomendas values ('2017-06-18 ', 24543495, 12345);
Insert into Encomendas values ('2017-07-18 ', 24543495, 11111);
Insert into Encomendas values ('2017-08-12 ', 24543495, 23435);


Insert into Vendas values ('2017-06-24 ', 98372344, 12345);
Insert into Vendas values ('2017-07-30 ', 12342545, 11111);
Insert into Vendas values ('2017-08-28 ', 98372344, 23435);

Insert into Login values ('salazar','qwerty',98372344);
Insert into Login values ('camoes','qwerty',12342545);
