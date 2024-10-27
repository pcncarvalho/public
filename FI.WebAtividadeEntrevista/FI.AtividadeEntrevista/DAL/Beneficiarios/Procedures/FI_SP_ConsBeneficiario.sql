--==============================================================================================================
-- Criacao
-- Motivo:	Listar os beneficiarios
-- Autor:       Paulo Carvalho
-- Data:        2024-10-27
--==============================================================================================================
CREATE PROC [dbo].[FI_SP_ConsBeneficiario]
    @ID BIGINT = NULL,
    @IDCLIENTE BIGINT = NULL
AS
BEGIN
	
    SELECT ID, CPF, NOME, IDCLIENTE
    FROM BENEFICIARIOS
    WITH(NOLOCK) WHERE ID = ISNULL(@ID, ID)
    AND IDCLIENTE = ISNULL(@IDCLIENTE, IDCLIENTE)


END