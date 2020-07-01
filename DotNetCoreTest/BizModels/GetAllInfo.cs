using AngleSharp.Parser.Html;
using DotNetCoreTest.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace DotNetCoreTest
{
    /// <summary>
    /// 最新电影数据
    /// </summary>
    public class GetAllInfo
    {
        private static WriteHelper writeHelper = new WriteHelper(Path.Combine(ConstsConf.WWWRootPath, ConstsConf.txtName));



        private static HtmlParser htmlParser = new HtmlParser();

        /// <summary>
        /// 爬取数据
        /// </summary>
        /// <param name="indexPageCount"></param>
        public static void CrawlLatestDLTInfo(int indexPageCount = 0)
        {
            // Task.Factory.StartNew(() =>
            //  {
            try
            {
                LogHelper.Info("CrawlLatestDLTInfo Start...");
                indexPageCount = indexPageCount == 0 ? 2 : indexPageCount;

                for (var i = 1; i < indexPageCount; i++)
                {
                    try
                    {
                        var indexURL = $"http://www.lottery.gov.cn/historykj/history_{i}.jspx?_ltype=dlt";
                        //var indexURL = $"http://www.demo.com/asd.html";
                        FillDLTFromOnline(indexURL);
                        Thread.Sleep(2000);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error("CrawlLatestDLTInfo Exception", ex);
                    }
                }

                LogHelper.Info("CrawlLatestDLTInfo Finish!");
            }
            catch (Exception ex)
            {
                LogHelper.Error("CrawlLatestDLTInfo Exception", ex);
            }
            //  });
        }

        /// <summary>
        /// 从在线网页提取数据
        /// </summary>
        /// <param name="i"></param>
        private static void FillDLTFromOnline(string indexURL)
        {
            var htmlDoc = HTTPHelper.GetHTMLByURL(indexURL);
            var dom = htmlParser.Parse(htmlDoc);
            var lstDivInfo = dom.QuerySelectorAll("div.result table tbody");
            if (lstDivInfo != null)
            {
                lstDivInfo.FirstOrDefault().QuerySelectorAll("tr").ToList()
                .ForEach(tr =>
                {
                    var info = new DLTInfo()
                    {
                        DLTNum = tr.Children[0].InnerHtml,
                        BeforeNum1 = tr.Children[1].InnerHtml,
                        BeforeNum2 = tr.Children[2].InnerHtml,
                        BeforeNum3 = tr.Children[3].InnerHtml,
                        BeforeNum4 = tr.Children[4].InnerHtml,
                        BeforeNum5 = tr.Children[5].InnerHtml,

                        AfterNum1 = tr.Children[6].InnerHtml,
                        AfterNum2 = tr.Children[7].InnerHtml,
                        TheDate = tr.LastElementChild.InnerHtml,
                    };

                    if (info != null && info.DLTNum != null)
                        writeHelper.AddToDLTDic(info);
                });
            }
        }




        #region 获取近期数据
        /// <summary>
        /// 获取近期数据
        /// </summary>
        /// <param name="cou">获取的条数</param>
        /// <returns></returns>
        internal static List<DLTInfo> GetNewList(int cou)
        {
            List<DLTInfo> list = GetAllDLTNUM();
            return list.Take(cou).ToList();
        }

        #endregion


        public static List<DLTInfo> GetAllDLTNUM()
        {
            string path = Path.Combine(ConstsConf.WWWRootPath, ConstsConf.txtName);

            List<DLTInfo> list = new List<DLTInfo>();

            if (!File.Exists(path))
            {
                var pvFile = File.Create(path);
                pvFile.Flush();
                pvFile.Dispose();
                return null;
            }

            using (var stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                try
                {
                    StreamReader sr = new StreamReader(stream);
                    // string strLine = sr.ReadLine();
                    JsonSerializer serializer = new JsonSerializer
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        Converters = { new JavaScriptDateTimeConverter() }
                    };
                    //构建Json.net的读取流
                    using (var reader = new JsonTextReader(sr))
                    {
                        var lstDLT = serializer.Deserialize<List<DLTInfo>>(reader);
                        foreach (var item in lstDLT.Distinct().ToList())
                        {
                            list.Add(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Error("GetAllDLTNUM Exception", ex);
                }
            }
            return list.OrderByDescending(m => m.TheDate).ToList();

        }

    }
}
