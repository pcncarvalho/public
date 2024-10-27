--==============================================================================================================
-- Criacao
-- Motivo:	Excluir um beneficiario da tabela 
-- Autor:       Paulo Carvalho
-- Data:        2024-10-27
--==============================================================================================================
CREATE PROC [dbo].[FI_SP_DelBeneficiario]
	@ID BIGINT
AS
BEGIN
	DELETE FROM BENEFICIARIOS WHERE ID = @ID
END
GO