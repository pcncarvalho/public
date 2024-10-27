using FI.AtividadeEntrevista.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// Inclui um novo beneficiário
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiário</param>
        public long Incluir(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiario ben = new DAL.DaoBeneficiario();

            beneficiario.CPF = beneficiario.CPF.SomenteAlfaNumericos();

            if (!Utils.Utils.CPFValido(beneficiario.CPF))
            {
                throw new AtividadeEntrevistaException("CPF informado é inválido.");
            }

            if (VerificarExistencia(beneficiario.CPF, beneficiario.Id, beneficiario.IdCliente))
            {
                throw new AtividadeEntrevistaException("CPF informado já cadastrado para este cliente.");
            }

            return ben.Incluir(beneficiario);
        }

        /// <summary>
        /// Altera um beneficiario
        /// </summary>
        /// <param name="cliente">Objeto de beneficiario</param>
        public void Alterar(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiario ben = new DAL.DaoBeneficiario();

            beneficiario.CPF = beneficiario.CPF.SomenteAlfaNumericos();

            if (!Utils.Utils.CPFValido(beneficiario.CPF))
            {
                throw new Exception("CPF informado é inválido.");
            }

            if (VerificarExistencia(beneficiario.CPF, beneficiario.Id, beneficiario.IdCliente))
            {
                throw new AtividadeEntrevistaException("CPF informado já cadastrado para este cliente.");
            }

            ben.Alterar(beneficiario);
        }

        /// <summary>
        /// Listar beneficiarios por cliente
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        public List<DML.Beneficiario> ListarPorCliente(long idCliente)
        {
            DAL.DaoBeneficiario ben = new DAL.DaoBeneficiario();
            return ben.ListarPorCliente(idCliente);
        }

        /// <summary>
        /// Excluir o beneficiario pelo id
        /// </summary>
        /// <param name="id">id do beneficiario</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoBeneficiario ben = new DAL.DaoBeneficiario();
            ben.Excluir(id);
        }

        /// <summary>
        /// Verificar existencia de um beneficiario com CPF já cadastrado para o mesmo cliente
        /// </summary>
        /// <param name="CPF"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool VerificarExistencia(string CPF, long id, long idCliente)
        {
            DAL.DaoBeneficiario ben = new DAL.DaoBeneficiario();
            return ben.VerificarExistencia(CPF, id, idCliente);
        }
    }
}
