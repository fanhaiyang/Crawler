using Crawler.Framework;
using Crawler.JD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler
{
    public class Program
    {
        static void Main(string[] args)
        {
            // 数据抓取服务
            CrawlerService service = new CrawlerService();

            #region 京东首页商品标题数据

            // 获取京东首页Html
            string pageHtml = RequestHelper.HttpGetPageHtml("https://www.jd.com/");

            //// 首页商品标题数据
            //string xpathFirst = "//*[@class='cate_menu_item']/a";
            //List<ProductInfo> productList = service.GetProductList(pageHtml, xpathFirst);

            #endregion

            #region 抓取京东手机信息


            List<ProductInfo> phoneList = new List<ProductInfo>();

            // 商品总页数
            int TotalCount = 200;
            for (int i = 0; i < TotalCount - 1; i++)
            {
                // 该链接目前偶数页数据和前一个奇数页数据相同
                if (i % 2 != 0)
                {
                    // 获取京东手机信息Html
                    string phoneHtml = RequestHelper.WebClientDownloadHtml("https://search.jd.com/Search?keyword=%E6%89%8B%E6%9C%BA&enc=utf-8&qrst=1&rt=1&stop=1&vt=2&cid2=653&cid3=655&page=" + i + "");

                    // 获取当前页的手机数据
                    List<ProductInfo> onePageList = service.GetPhoneList(phoneHtml);
                    phoneList.AddRange(onePageList);
                    Console.WriteLine($"----------当前抓取总数{phoneList.Count}-----------");
                }
            }

            #endregion

            Console.ReadKey();
        }
    }
}
