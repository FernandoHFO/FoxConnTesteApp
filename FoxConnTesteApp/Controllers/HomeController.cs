using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FoxConnTesteApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace FoxConnTesteApp.Controllers
{
    public class HomeController : Controller
    {
        List<Produto> listProductFull = new List<Produto>();

        public IActionResult Index()
        {            
            return View();
        }

        [HttpGet]
        public IActionResult NewProduct()
        {
            Constant constant = new Constant();
            try
            {
                ViewBag.ListTipoProduto = new SelectList(constant.GetListTipoProduto(), "Descricao", "Descricao");
                return View();
            }
            catch (Exception ex)
            {
                ViewData["Erro"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }     
           
        }

        [HttpPost]
        public IActionResult NewProduct(Produto product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AppDAO appDAO = new AppDAO();
                    if (TempData["listProductFull"] != null)
                    {
                        listProductFull = JsonConvert.DeserializeObject<List<Produto>>(TempData["listProductFull"].ToString());
                        TempData.Keep("listProductFull");
                        Produto productSelected = listProductFull.Find(p => p.Codigo == product.Codigo);

                        if (productSelected != null)
                        {
                            Constant constant = new Constant();
                            ViewBag.ListTipoProduto = new SelectList(constant.GetListTipoProduto(), "Descricao", "Descricao");
                            ModelState.AddModelError("", "Código de Produto já Cadastrado");
                            return View();
                        }

                    }

                    listProductFull = appDAO.PostProduct(product, listProductFull);
                    TempData["listProductFull"] = JsonConvert.SerializeObject(listProductFull);
                    TempData.Keep("listProductFull");

                    return RedirectToAction("ConsultProduct", "Home");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewData["Erro"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }

        }

        public IActionResult ConsultProduct()
        {
            List<Produto> listProduct = new List<Produto>();
            try
            {
                if (TempData["listProductFull"] != null)
                {
                    listProduct = JsonConvert.DeserializeObject<List<Produto>>(TempData["listProductFull"].ToString()); ;
                    TempData.Keep("listProductFull");
                }

                return View(listProduct);
            }
            catch (Exception ex)
            {

                ViewData["Erro"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }

        }

        public IActionResult EditProduct(int codigo)
        {
            Constant constant = new Constant();
            List<Produto> listProduct = new List<Produto>();
            try
            {
                if (TempData["listProductFull"] != null)
                {
                    
                    listProduct = JsonConvert.DeserializeObject<List<Produto>>(TempData["listProductFull"].ToString()); ;
                    TempData.Keep("listProductFull");
                }

                Produto product = listProduct.FirstOrDefault(p => p.Codigo == codigo);

                if (product == null)
                {
                    ViewData["Error"] = "Produto não encontrado";
                    return RedirectToAction("Error", "Home");
                }

                ViewBag.ListTipoProduto = new SelectList(constant.GetListTipoProduto(), "Descricao", "Descricao");
                return View(product);
            }
            catch (Exception ex)
            {
                ViewData["Erro"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public IActionResult EditProduct(Produto productAlt)
        {
            List<Produto> listProduct = new List<Produto>();
            try
            {
                if (TempData["listProductFull"] != null)
                {

                    listProduct = JsonConvert.DeserializeObject<List<Produto>>(TempData["listProductFull"].ToString()); ;
                    TempData.Keep("listProductFull");
                }

                //foreach (Produto item in listProduct)
                //{
                //    if (productAlt.Codigo == item.Codigo)
                //    {
                //        item.Codigo = productAlt.Codigo;
                //        item.Descricao = productAlt.Descricao;
                //        item.TipoProduto = productAlt.TipoProduto;
                //        item.DataLancamento = productAlt.DataLancamento;
                //        item.Valor = productAlt.Valor;

                //        break;
                //    };
                //}

                listProduct.FirstOrDefault(p => p.Codigo == productAlt.Codigo).Codigo = productAlt.Codigo;
                listProduct.FirstOrDefault(p => p.Codigo == productAlt.Codigo).Descricao = productAlt.Descricao;
                listProduct.FirstOrDefault(p => p.Codigo == productAlt.Codigo).DataLancamento = productAlt.DataLancamento;
                listProduct.FirstOrDefault(p => p.Codigo == productAlt.Codigo).TipoProduto = productAlt.TipoProduto;
                listProduct.FirstOrDefault(p => p.Codigo == productAlt.Codigo).Valor = productAlt.Valor;

                TempData["listProductFull"] = JsonConvert.SerializeObject(listProduct);
                TempData.Keep("listProductFull");

                return RedirectToAction("ConsultProduct");
            }
            catch (Exception ex)
            {
                ViewData["Erro"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
            
        }

        public IActionResult DeleteProduct(int codigo)
        {
            Constant constant = new Constant();
            List<Produto> listProduct = new List<Produto>();
            try
            {
                if (TempData["listProductFull"] != null)
                {

                    listProduct = JsonConvert.DeserializeObject<List<Produto>>(TempData["listProductFull"].ToString()); ;
                    TempData.Keep("listProductFull");
                }

                Produto product = listProduct.FirstOrDefault(p => p.Codigo == codigo);

                if (product == null)
                {
                    ViewData["Error"] = "Produto não encontrado";
                    return RedirectToAction("Error", "Home");
                }

                TempData["ProductDel"] = JsonConvert.SerializeObject(product);
                TempData.Keep("ProductDel");

                return View(product);
            }
            catch (Exception ex)
            {
                ViewData["Erro"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public IActionResult DeleteProduct()
        {
            List<Produto> listProduct = new List<Produto>();
            try
            {
                if (TempData["listProductFull"] != null)
                {

                    listProduct = JsonConvert.DeserializeObject<List<Produto>>(TempData["listProductFull"].ToString()); ;
                    TempData.Keep("listProductFull");
                }


                Produto productDel = TempData["ProductDel"] != null ?
                                                JsonConvert.DeserializeObject<Produto>(TempData["ProductDel"].ToString()) :
                                                new Produto();
                                                                                         

                foreach (Produto item in listProduct)
                {
                    if (productDel.Codigo == item.Codigo)
                    {
                        listProduct.Remove(item);

                        break;
                    };
                }


                TempData["listProductFull"] = JsonConvert.SerializeObject(listProduct);
                TempData.Keep("listProductFull");
                TempData.Remove("ProductDel");

                return RedirectToAction("ConsultProduct");
            }
            catch (Exception ex)
            {
                ViewData["Erro"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }

        }

        public IActionResult ClearConsult()
        {
            TempData.Remove("listProductFull");

            return RedirectToAction("ConsultProduct");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string errorMessage = "")
        {
            if (errorMessage != "")
            {
                ViewData["Erro"] = errorMessage;
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
