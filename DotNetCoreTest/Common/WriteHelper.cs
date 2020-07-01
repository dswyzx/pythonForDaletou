using DotNetCoreTest.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DotNetCoreTest
{
    public class WriteHelper
    {

        private string _txtPath = Path.Combine(ConstsConf.WWWRootPath, "DLT.txt");
        private string _jsonPath = Path.Combine(ConstsConf.WWWRootPath, ConstsConf.txtName);
        private ConcurrentDictionary<string, DLTInfo> _cdDLTInfo = new ConcurrentDictionary<string, DLTInfo>();

        private ConcurrentBag<string> _bagDLTInfo = new ConcurrentBag<string>();

        /// <summary>
        /// 初始化列表
        /// </summary>
        /// <param name="jsonFilePath">Json文件存放位置</param>
        public WriteHelper(string jsonFilePath = "")
        {
            if (!string.IsNullOrEmpty(jsonFilePath))
            {
                _jsonPath = jsonFilePath;
            }
            if (!File.Exists(jsonFilePath))
            {
                var pvFile = File.Create(jsonFilePath);
                pvFile.Flush();
                pvFile.Dispose();
                return;

            }
            using (var stream = new FileStream(jsonFilePath, FileMode.OpenOrCreate))
            {
                try
                {
                    StreamReader sr = new StreamReader(stream);
                    JsonSerializer serializer = new JsonSerializer
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        Converters = { new JavaScriptDateTimeConverter() }
                    };
                    //构建Json.net的读取流  
                    using (var reader = new JsonTextReader(sr))
                    {
                        var lstDLT = serializer.Deserialize<List<DLTInfo>>(reader);
                        foreach (var DLTinfo in lstDLT.GroupBy(m => m.DLTNum))
                        {
                            if (!_cdDLTInfo.ContainsKey(DLTinfo.Key))
                                _cdDLTInfo.TryAdd(DLTinfo.Key, DLTinfo.FirstOrDefault());
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Error("WriteHelper Exception", ex);

                }
            }

        }

        /// <summary>
        /// 添加到字典（线程安全）
        /// </summary>
        /// <param name="dltInfo"></param>
        /// <returns></returns>
        public bool AddToDLTDic(DLTInfo dltInfo)
        {
            if (dltInfo != null && !_cdDLTInfo.ContainsKey(dltInfo.DLTNum))
            {
                LogHelper.Info("Add DLT Success!");
                _cdDLTInfo.TryAdd(dltInfo.DLTNum, dltInfo);
                WriteToJsonFile(dltInfo);
            }
            return true;
        }

        /// <summary>
        /// 每多10条重写一次
        /// </summary>
        /// <param name="dltInfo"></param>
        /// <param name="isWriteNow"></param>
        private void WriteToJsonFile(DLTInfo dltInfo, bool isWriteNow = false)
        {
            if (_cdDLTInfo.Count % 10 == 0 || isWriteNow)
            {
                using (var stream = new FileStream(_jsonPath, FileMode.OpenOrCreate))
                {
                    StreamWriter sw = new StreamWriter(stream);
                    JsonSerializer serializer = new JsonSerializer
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        Converters = { new JavaScriptDateTimeConverter() }
                    };
                    //构建Json.net的写入流  
                    JsonWriter writer = new JsonTextWriter(sw);
                    //把模型数据序列化并写入Json.net的JsonWriter流中  
                    serializer.Serialize(writer, _cdDLTInfo.Values.OrderBy(m => m.DLTNum).ToList());
                    sw.Flush();
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// 是否包含此项
        /// </summary>
        /// <param name="DLTNum"></param>
        /// <returns></returns>
        public bool IsContainsMoive(string DLTNum)
        {
            return _cdDLTInfo.ContainsKey(DLTNum);
        }

        /// <summary>
        /// 写入txt文件
        /// </summary>
        public void WriteToTxtFile(string path = "", bool isWriteNow = false)
        {
            using (var stream = new FileStream(_txtPath, FileMode.OpenOrCreate))
            {
                StreamWriter sw = new StreamWriter(stream);
                JsonSerializer serializer = new JsonSerializer
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Converters = { new JavaScriptDateTimeConverter() }
                };
                //构建Json.net的写入流  
                JsonWriter writer = new JsonTextWriter(sw);
                //把模型数据序列化并写入Json.net的JsonWriter流中  

                StringBuilder res = new StringBuilder();

                if (_cdDLTInfo.Count > 1)
                {
                    foreach (var info in _cdDLTInfo)
                    {
                        res.Append(info.Key + "|" + info.Value.BeforeNum1 + "/r/n");
                    }
                }

                serializer.Serialize(writer, res);
                sw.Flush();
                writer.Close();

            }
        }
    }
}

