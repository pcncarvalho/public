﻿using FI.AtividadeEntrevista.Utils;
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

            //if (VerificarExistencia(beneficiario.CPF, beneficiario.Id))
            //{
            //    throw new AtividadeEntrevistaException("CPF informado já cadastrado.");
            //}

            return ben.Incluir(beneficiario);
        }

        /// <summary>
        /// Altera um cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public void Alterar(DML.Cliente cliente)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();

            cliente.CPF = cliente.CPF.SomenteAlfaNumericos();
            cliente.CEP = cliente.CEP.SomenteAlfaNumericos();

            if (!Utils.Utils.CPFValido(cliente.CPF))
            {
                throw new Exception("CPF informado é inválido.");
            }

            if (VerificarExistencia(cliente.CPF, cliente.Id))
            {
                throw new AtividadeEntrevistaException("CPF informado já cadastrado.");
            }

            cli.Alterar(cliente);
        }

        /// <summary>
        /// Consulta o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public DML.Cliente Consultar(long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Consultar(id);
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
        /// Excluir o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            cli.Excluir(id);
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Cliente> Listar()
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Listar();
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Pesquisa(iniciarEm, quantidade, campoOrdenacao, crescente, out qtd);
        }

        /// <summary>
        /// Verificar existencia de um cliente com CPF já cadastrado
        /// </summary>
        /// <param name="CPF"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool VerificarExistencia(string CPF, long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.VerificarExistencia(CPF, id);
        }
    }
}