using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;
using FI.AtividadeEntrevista.Utils;
using FI.WebAtividadeEntrevista.Controllers;
using NLog;
using WebGrease.Activities;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            BoCliente bo = new BoCliente();

            try
            {
                if (!this.ModelState.IsValid)
                {
                    List<string> erros = (from item in ModelState.Values
                                          from error in item.Errors
                                          select error.ErrorMessage).ToList();

                    throw new AtividadeEntrevistaException((string.Join(Environment.NewLine, erros)));
                }

                model.Id = bo.Incluir(new Cliente()
                {
                    CEP = model.CEP,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone,
                    CPF = model.CPF
                });

                return Json("Cadastro efetuado com sucesso.");
            }
            catch (AtividadeEntrevistaException ex)
            {
                Response.StatusCode = 401;
                logger.Info($"{this.ControllerContext.Controller.GetType().Name} -> Incluir -> {ex.Message}");
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                logger.Error($"{this.ControllerContext.Controller.GetType().Name} -> Incluir -> {ex}");
                return Json("Não foi possível incluir o cliente, entre em contato com o suporte.");
            }
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            BoCliente bo = new BoCliente();

            try
            {
                if (!this.ModelState.IsValid)
                {
                    List<string> erros = (from item in ModelState.Values
                                          from error in item.Errors
                                          select error.ErrorMessage).ToList();

                    throw new AtividadeEntrevistaException((string.Join(Environment.NewLine, erros)));
                }
                else
                {
                    bo.Alterar(new Cliente()
                    {
                        Id = model.Id,
                        CEP = model.CEP,
                        Cidade = model.Cidade,
                        Email = model.Email,
                        Estado = model.Estado,
                        Logradouro = model.Logradouro,
                        Nacionalidade = model.Nacionalidade,
                        Nome = model.Nome,
                        Sobrenome = model.Sobrenome,
                        Telefone = model.Telefone,
                        CPF = model.CPF
                    });

                    return Json("Cadastro alterado com sucesso.");
                }
            }
            catch (AtividadeEntrevistaException ex)
            {
                Response.StatusCode = 401;
                logger.Info($"{this.ControllerContext.Controller.GetType().Name} -> Alterar -> {ex.Message}");
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                logger.Error($"{this.ControllerContext.Controller.GetType().Name} -> Alterar -> {ex}");
                return Json("Não foi possível alterar o cliente, entre em contato com o suporte.");
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoCliente bo = new BoCliente();

            try
            {
                Cliente cliente = bo.Consultar(id);
                Models.ClienteModel model = null;

                if (cliente != null)
                {
                    model = new ClienteModel()
                    {
                        Id = cliente.Id,
                        CEP = cliente.CEP,
                        Cidade = cliente.Cidade,
                        Email = cliente.Email,
                        Estado = cliente.Estado,
                        Logradouro = cliente.Logradouro,
                        Nacionalidade = cliente.Nacionalidade,
                        Nome = cliente.Nome,
                        Sobrenome = cliente.Sobrenome,
                        Telefone = cliente.Telefone,
                        CPF = cliente.CPF
                    };
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }

                return View(model);
            }
            catch (Exception ex)
            {
                logger.Error($"{this.ControllerContext.Controller.GetType().Name} -> Alterar -> {ex}");
                return View();
            }
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                logger.Error($"{this.ControllerContext.Controller.GetType().Name} -> ClienteList -> {ex}");
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}