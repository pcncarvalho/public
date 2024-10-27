--==============================================================================================================
-- Criacao
-- Motivo:	Incluir um beneficiario 
-- Autor:       Paulo Carvalho
-- Data:        2024-10-27
--==============================================================================================================
CREATE PROC [dbo].[FI_SP_IncBeneficiario]
	@CPF           VARCHAR (11),
    @NOME          VARCHAR (50) ,
    @IDCLIENTE     BIGINT
AS
BEGIN
	INSERT INTO BENEFICIARIOS (CPF, NOME, IDCLIENTE) VALUES (@CPF, @NOME, @IDCLIENTE)
END
GO