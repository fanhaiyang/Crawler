using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Framework
{
    /// <summary>
    /// 请求帮助类
    /// </summary>
    public class RequestHelper
    {
        /// <summary>
        /// HttpWebRequest获取网页Html代码
        /// </summary>
        /// <param name="urlPath"></param>
        /// <returns></returns>
        public static string HttpGetPageHtml(string urlPath)
        {
            string pageHtml = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlPath);

            request.ContentType = "text/html; charset=utf-8";
            request.Method = "GET";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36";
            //request.Headers.Add("Cookie", @"newUserFlag=1; guid=YFT7C9E6TMFU93FKFVEN7TEA5HTCF5DQ26HZ; gray=959782; cid=av9kKvNkAPJ10JGqM_rB_vDhKxKM62PfyjkB4kdFgFY5y5VO; abtest=31; _ga=GA1.2.334889819.1425524072; grouponAreaId=37; provinceId=20; search_showFreeShipping=1; rURL=http%3A%2F%2Fsearch.yhd.com%2Fc0-0%2Fkiphone%2F20%2F%3Ftp%3D1.1.12.0.73.Ko3mjRR-11-FH7eo; aut=5GTM45VFJZ3RCTU21MHT4YCG1QTYXERWBBUFS4; ac=57265177%40qq.com; msessionid=H5ACCUBNPHMJY3HCK4DRF5VD5VA9MYQW; gc=84358431%2C102362736%2C20001585%2C73387122; tma=40580330.95741028.1425524063040.1430288358914.1430790348439.9; tmd=23.40580330.95741028.1425524063040.; search_browse_history=998435%2C1092925%2C32116683%2C1013204%2C6486125%2C38022757%2C36224528%2C24281304%2C22691497%2C26029325; detail_yhdareas=""; cart_cookie_uuid=b64b04b6-fca7-423b-b2d1-ff091d17e5e5; gla=20.237_0_0; JSESSIONID=14F1F4D714C4EE1DD9E11D11DDCD8EBA; wide_screen=1; linkPosition=search");

            Encoding enc = Encoding.UTF8;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Console.WriteLine("请求出错！");
                }
                else
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), enc);
                    pageHtml = reader.ReadToEnd();
                    reader.Close();
                }
            }
            return pageHtml;
        }

        /// <summary>
        /// WebClient获取网页Html代码
        /// </summary>
        /// <param name="urlPath"></param>
        /// <returns></returns>
        public static string WebClientDownloadHtml(string urlPath)
        {
            string pageHtml = string.Empty;
            WebClient wc = new WebClient();

            wc.Headers.Add("ContentType", "text/html; charset=utf-8");
            wc.Headers.Add("Method", "GET");
            wc.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36");

            Encoding enc = Encoding.UTF8;

            // 方法一
            byte[] buffer = wc.DownloadData(urlPath);
            pageHtml = enc.GetString(buffer);

            //// 方法二
            //Stream stream = wc.OpenRead(urlPath);
            //StreamReader reader = new StreamReader(stream, enc);
            //pageHtml = reader.ReadToEnd();
            //reader.Close();

            return pageHtml;
        }
        
        /// <summary>
        /// HttpWebRequest获取Post接口数据
        /// </summary>
        /// <param name="urlPath"></param>
        /// <returns></returns>
        public static string HttpGetPageHtml(string urlPath)
        {
            var request = (HttpWebRequest)WebRequest.Create("xxxxx");
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "Post";
            try
            {
                string param1 = "1111";
                string param2 = "2222";
                string paramStr = $"param1={param1}&param2={param2}";
                request.ContentLength = paramStr.Length;
                
                byte[] data = Encoding.UTF8.GetBytes(paramStr);
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                string result = string.Empty;
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    result = reader.ReadToEnd();

                    File.WriteAllText($"{AppDomain.CurrentDomain.BaseDirectory}\\resule.txt", result); // 文本存储返回结果
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey();
            }
            Console.ReadKey();
        }
    }
}
