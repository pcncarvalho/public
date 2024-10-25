﻿--==============================================================================================================
-- Criacao
-- Motivo:    Inclusão do campo CPF na tabela CLIENTES
-- Autor:       Paulo Carvalho
-- Data:        2024-10-24
--==============================================================================================================
CREATE PROC FI_SP_AltClienteV2
    @NOME          VARCHAR (50) ,
    @SOBRENOME     VARCHAR (255),
    @NACIONALIDADE VARCHAR (50) ,
    @CEP           VARCHAR (9)  ,
    @ESTADO        VARCHAR (2)  ,
    @CIDADE        VARCHAR (50) ,
    @LOGRADOURO    VARCHAR (500),
    @EMAIL         VARCHAR (2079),
    @TELEFONE      VARCHAR (15),
    @Id            BIGINT,
    @CPF	   VARCHAR(11)
AS
BEGIN
	UPDATE CLIENTES 
	SET 
		NOME = @NOME, 
		SOBRENOME = @SOBRENOME, 
		NACIONALIDADE = @NACIONALIDADE, 
		CEP = @CEP, 
		ESTADO = @ESTADO, 
		CIDADE = @CIDADE, 
		LOGRADOURO = @LOGRADOURO, 
		EMAIL = @EMAIL, 
		TELEFONE = @TELEFONE,
		CPF = @CPF
	WHERE Id = @Id
END

