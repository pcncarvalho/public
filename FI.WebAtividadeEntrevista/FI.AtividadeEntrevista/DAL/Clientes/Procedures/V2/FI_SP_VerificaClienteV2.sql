--==============================================================================================================
-- Alteracao
-- Motivo:      Retornar o ID do Cliente
-- Autor:       Paulo Carvalho
-- Data:        2024-10-24
--==============================================================================================================
CREATE PROC FI_SP_VerificaClienteV2
	@CPF VARCHAR(11)
AS
BEGIN
	SELECT  ID FROM CLIENTES WITH(NOLOCK) WHERE CPF = @CPF
END