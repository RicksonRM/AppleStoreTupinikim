using Microsoft.AspNetCore.Mvc;
using FireSharp.Config;
using FireSharp.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using FireSharp.Response;
using AppleStoreTupinikim.Models;

namespace AppleStoreTupinikim.Controllers
{
    public class Produto : Controller
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "NhBtbde9JW8FYj2SowRVbiuRPIWL7n89SJo6GNpL",
            BasePath = "https://firemvc-f9998-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;
        public ActionResult Index()
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Produtos");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<Produtos>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    list.Add(JsonConvert.DeserializeObject<Produtos>(((JProperty)item).Value.ToString()));
                }
            }

            return View(list);
            
        }

        [HttpGet]
        public ActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Criar(Produtos produto)
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
                var dados = produto;
                PushResponse resposta = client.Push("Produtos/", dados);
                dados.Id = resposta.Result.name;
    
               
                SetResponse setResposta = client.Set("Produtos/" + dados.Id, dados);

                if(setResposta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ModelState.AddModelError(string.Empty, "Produto cadastrado com sucesso!");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Algo deu errado!");
                }
            }

            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Editar(string codigo)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse resposta = client.Get("Produtos/" + codigo);
            Produtos dados = JsonConvert.DeserializeObject<Produtos>(resposta.Body);
            return View(dados);
        }

        [HttpPost]
        public ActionResult Editar(Produtos produto)
        {
            client = new FireSharp.FirebaseClient(config);
            SetResponse resposta = client.Set("Produtos/" + produto.Id, produto);
            return RedirectToAction("Index");
        }

        public ActionResult Excluir(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Delete("Produtos/" + id);
            return RedirectToAction("Index");
        }
    }
}
