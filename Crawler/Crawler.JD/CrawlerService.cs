using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Crawler.JD
{
    public class CrawlerService
    {
        /// <summary>
        /// 获取产品数据列表
        /// </summary>
        /// <param name="pageHtml"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public List<ProductInfo> GetProductList(string pageHtml, string xpath)
        {
            List<ProductInfo> productList = new List<ProductInfo>();
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(pageHtml);

            // 商品数据
            HtmlNodeCollection htmlNode = document.DocumentNode.SelectNodes(xpath);
            if (htmlNode != null)
            {
                foreach (var node in htmlNode)
                {
                    ProductInfo product = new ProductInfo();
                    product.DetailUrl = node.Attributes["href"].Value;
                    product.Name = node.InnerText;
                    productList.Add(product);
                }
            }

            return productList;
        }

        /// <summary>
        /// 获取手机信息
        /// </summary>
        /// <param name="pageHtml"></param>
        /// <returns></returns>
        public List<ProductInfo> GetPhoneList(string pageHtml)
        {
            List<ProductInfo> productList = new List<ProductInfo>();
            List<string> phoneHtmlList = new List<string>();
            HtmlDocument document = new HtmlDocument();

            document.LoadHtml(pageHtml);

            // 一级目录
            string firstXPath = "//*[@id='J_goodsList']/ul/li/div";
            HtmlNodeCollection htmlNode = document.DocumentNode.SelectNodes(firstXPath);
            if (htmlNode != null)
            {
                foreach (var node in htmlNode)
                {
                    phoneHtmlList.Add(node.OuterHtml);
                }
            }

            int j = 0;
            int totlaCount = phoneHtmlList.Count;
            // 遍历一级目录，获取二级目录数据
            foreach (var item in phoneHtmlList)
            {
                j += 1;
                document.LoadHtml(item);
                Console.WriteLine($"抓取进度({totlaCount}/{j})...");

                ProductInfo product = new ProductInfo();
                string secondInfo = "//*[@class='p-img']/a";
                var infoNode = document.DocumentNode.SelectSingleNode(secondInfo);
                if (infoNode != null)
                {
                    product.DetailUrl = infoNode.Attributes["href"].Value;
                    product.Description = infoNode.Attributes["title"].Value;
                    product.Name = product.Description.Split(' ')[0];
                }

                // 图片
                string secondImg = "//*[@class='p-img']/a/img";
                var imgNode = document.DocumentNode.SelectSingleNode(secondImg);
                if (imgNode != null)
                {
                    if (imgNode.Attributes.Contains("src"))
                    {
                        if (imgNode.Attributes["src"].Value != null)
                            product.ImgUrl = imgNode.Attributes["src"].Value;
                    }
                    else if (imgNode.Attributes["source-data-lazy-img"].Value != null)
                        product.ImgUrl = imgNode.Attributes["source-data-lazy-img"].Value;
                }

                // 价格
                string secondPrice = "//*[@id='J_goodsList']/ul/li/div/div[3]/strong/i";
                var priceNode = document.DocumentNode.SelectSingleNode(secondPrice);
                if (priceNode != null)
                {
                    product.Price = priceNode.InnerText;
                }

                productList.Add(product);
            }
            return productList;
        }
    }
}
