using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace crawler_teste
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StartCrawlerAsync();
            Console.ReadLine();

        }

        private static async Task StartCrawlerAsync()
        {
            var url = "https://www.giraffas.com.br/cardapio";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var divs = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Equals("l-menu__categories")).ToList();

            var produtos = new List<Produto>();

            foreach (var div in divs)
            {

                var produto = new Produto
                {

                    Name = div?.Descendants("h2")?.FirstOrDefault()?.InnerText,
                    //Preco = div?.Descendants("span")?.FirstOrDefault()?.InnerText,
                    Img = div?.Descendants("img")?.FirstOrDefault()?
                        .ChildAttributes("src")?.FirstOrDefault()?.Value,
                    Link = div?.Descendants("a")?.FirstOrDefault()?
                        .ChildAttributes("href")?.FirstOrDefault()?.Value
                };
                produtos.Add(produto);
            }
        }
    }

    //[DebuggerDisplay("{Nome}, {Preco}")]
    internal class Produto
    {
        public string Name { get; set; }
        public string Preco { get; set; }
        public string Link { get; set; }
        public string Img { get; set; }
    }
}


