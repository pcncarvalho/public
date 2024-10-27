--==============================================================================================================
-- Criacao
-- Motivo:	Verificar a existencia de um beneficiario cadastrado 
-- Autor:       Paulo Carvalho
-- Data:        2024-10-27
--==============================================================================================================
CREATE PROC [dbo].[FI_SP_VerificaBeneficiario]
	@CPF VARCHAR(14)
AS
BEGIN
	SELECT ID, IDCLIENTE FROM BENEFICIARIOS WITH(NOLOCK) WHERE CPF = @CPF
END
GO