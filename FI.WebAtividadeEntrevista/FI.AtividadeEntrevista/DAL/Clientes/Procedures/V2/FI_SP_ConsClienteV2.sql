--==============================================================================================================
-- Alteracao
-- Motivo:    Inclusão do campo CPF na tabela CLIENTES
-- Autor:       Paulo Carvalho
-- Data:        2024-10-24
--==============================================================================================================

ALTER PROC FI_SP_ConsClienteV2
	@ID  BIGINT,
	@CPF VARCHAR(11) = NULL
AS
BEGIN
	
	IF @CPF IS NOT NULL
		SELECT NOME, SOBRENOME, NACIONALIDADE, CEP, ESTADO, CIDADE, LOGRADOURO, EMAIL, TELEFONE, ID, CPF FROM CLIENTES WITH(NOLOCK) WHERE CPF = @CPF
	ELSE IF(ISNULL(@ID,0) = 0)
		SELECT NOME, SOBRENOME, NACIONALIDADE, CEP, ESTADO, CIDADE, LOGRADOURO, EMAIL, TELEFONE, ID, CPF FROM CLIENTES WITH(NOLOCK)
	ELSE
		SELECT NOME, SOBRENOME, NACIONALIDADE, CEP, ESTADO, CIDADE, LOGRADOURO, EMAIL, TELEFONE, ID, CPF FROM CLIENTES WITH(NOLOCK) WHERE ID = @ID
END