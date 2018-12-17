﻿using DataGate.Com.DB;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGate.App.DataService
{
    /// <summary>
    /// 访问数据库的配置
    /// </summary>
    public class DataGateKey
    {
        /// <summary>
        /// 配置名称
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// where后面的查询子句
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Order By 后面的排序子句
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// 忽略Model的设定直接指定Sql语句
        /// </summary>
        public string Sql { get; set; }

        /// <summary>
        /// 数据模型名称
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// 操作的类型，决定调用哪个方法
        /// GetArray, GetObject, GetPage, Insert, Update,Delete
        /// </summary>
        public DataOpType OpType { get; set; }

        /// <summary>
        /// 注入服务，在操作前和操作后提供切入点
        /// </summary>
        [JsonIgnore]
        public IDataGate DataGate { get; set; }

        [JsonIgnore]
        internal JoinInfo[] TableJoins { get; set; }

        /// <summary>
        /// 明确指定的查询字段
        /// </summary>
        public string QueryFields { get; set; }

        /// <summary>
        /// 已经先期构造的join子句
        /// </summary>
        [JsonIgnore]
        internal string JoinSubTerm { get; set; }

        /// <summary>
        /// 已经先期构造的查询字段列表
        /// </summary>
        [JsonIgnore]
        internal string QueryFieldsTerm { get; set; }

        /// <summary>
        /// 连接串名称，默认为Default
        /// </summary>
        public string ConnName { get; set; }

        /// <summary>
        /// 用于缓存数据，或提供测试数据，当此属性不为空时
        /// 将直接返回此数据
        /// </summary>
        public JToken Data { get; set; }
    }

    internal class JoinInfo
    {
        public TableMeta Table { get; set; }

        public string JoinFlag { get; set; }

        public string Alias { get; set; }

        public string Name { get; set; }
    }

    public enum DataOpType
    {
        None,
        GetArray,
        GetObject,
        GetPage,
        Save
    }
}
