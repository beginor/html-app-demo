using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Cfg;
using Env = NHibernate.Cfg.Environment;

namespace WebApi {

    /// <summary>
    /// 静态的 NHibernate 配置工厂， 简化 NHibernate 配置
    /// </summary>
    public static class HibernateFactory {

        private static ConcurrentDictionary<string, ISessionFactory> sessionFactories = new ConcurrentDictionary<string, ISessionFactory>();

        public static bool ShowSql { get; set; } = true;

        public static bool FormatSql { get; set; } = true;

        public static int BatchSize { get; set; } = 10;

        public static void Clear() {
            foreach (var sessionFactory in sessionFactories) {
                sessionFactory.Value.Dispose();
            }
            sessionFactories.Clear();
            sessionFactories = null;
        }

        /// <summary>
        /// 从 NHibernate 配置文件创建 ISessionFactory ;
        /// </summary>
        /// <param name="filePath">nhibernate 配置文件路径</param>
        /// <returns></returns>
        public static ISessionFactory CreateSessionFactory(string filePath) {
            return sessionFactories.GetOrAdd(filePath, key => {
                var cfg = new Configuration();
                cfg.Configure(filePath);
                return cfg.BuildSessionFactory();
            });
        }

        /// <summary>
        /// 用指定的连接串和组件 (dll) 名称创建 ISessionFactory ， 针对 MySQL Server;
        /// Create ISessionFactory with connectionString and assemblyName with default properties;
        /// </summary>
        /// <param name="connectionString">MySQL Server 链接字符串</param>
        /// <param name="assemblyName">.Net 组件名称</param>
        /// <returns></returns>
        public static ISessionFactory CreateSessionFactory(string connectionString, string assemblyName) {
            return CreateSessionFactory(connectionString, assemblyName, CreateDefaultProperties());
        }

        /// <summary>
        /// 用指定的连接串、组件 (dll) 名称和属性字典
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="assemblyName"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static ISessionFactory CreateSessionFactory(
            string connectionString,
            string assemblyName,
            IDictionary<string, string> properties
            ) {
            var key = $"connectionString:{connectionString}|assemblyName:{assemblyName}";
            return sessionFactories.GetOrAdd(key, k => {
                var cfg = new Configuration();
                cfg.SetProperties(properties);
                cfg.SetProperty(Env.ConnectionString, connectionString);
                cfg.AddAssembly(assemblyName);
                return cfg.BuildSessionFactory();
            });
        }

        public static IDictionary<string, string> CreateDefaultProperties() {
            var props = new Dictionary<string, string> {
                [Env.ConnectionProvider] = "NHibernate.Connection.DriverConnectionProvider",
                [Env.ConnectionDriver] = "NHibernate.Driver.MySqlDataDriver",
                [Env.Dialect] = "NHibernate.Dialect.MySQLDialect",
                [Env.ShowSql] = ShowSql ? bool.TrueString : bool.FalseString,
                [Env.FormatSql] = FormatSql ? bool.TrueString : bool.FalseString,
                [Env.BatchSize] = CheckBatchSize().ToString()
            };
            return props;
        }

        private static int CheckBatchSize() {
            return System.Environment.OSVersion.ToString().IndexOf("windows", StringComparison.OrdinalIgnoreCase) > 0 ? BatchSize : 0;
        }

    }

}