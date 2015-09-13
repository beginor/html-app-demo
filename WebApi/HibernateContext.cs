using System;
using System.Collections.Generic;
using NHibernate;

namespace WebApi {

    /// <summary>
    /// 对 ISessionFactory 的简单封装， 延迟初始化一个 ISessionFactory
    /// </summary>
    public class HibernateContext : Disposable {

        private Lazy<ISessionFactory> sessionFactory;

        /// <summary>
        /// 获取 ISessionFactory 实例
        /// </summary>
        public ISessionFactory SessionFactory => sessionFactory.Value;

        /// <summary>
        /// 初始化 HibernateContext 对象
        /// </summary>
        /// <param name="filePath">nhibernate xml 配置文件路径</param>
        public HibernateContext(string filePath) {
            sessionFactory = new Lazy<ISessionFactory>(() => HibernateFactory.CreateSessionFactory(filePath));
        }

        /// <summary>
        /// 初始化 HibernateContext 对象
        /// </summary>
        /// <param name="connectionString">数据库连接串</param>
        /// <param name="assemblyName">包含 hbm 映射的 .Net 组件</param>
        public HibernateContext(string connectionString, string assemblyName) {
            Argument.NotNullOrEmpty(connectionString, nameof(connectionString));
            Argument.NotNullOrEmpty(assemblyName, nameof(assemblyName));
            sessionFactory = new Lazy<ISessionFactory>(() => HibernateFactory.CreateSessionFactory(connectionString, assemblyName));
        }

        /// <summary>
        /// 初始化 HibernateContext 对象
        /// </summary>
        /// <param name="connectionString">数据库连接串</param>
        /// <param name="assemblyName">包含 hbm 映射的 .Net 组件</param>
        /// <param name="properties">NHibernate 配置属性字典</param>
        public HibernateContext(string connectionString, string assemblyName, IDictionary<string, string> properties) {
            sessionFactory = new Lazy<ISessionFactory>(() => HibernateFactory.CreateSessionFactory(connectionString, assemblyName, properties));
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                if (sessionFactory.IsValueCreated) {
                    sessionFactory.Value.Dispose();
                    sessionFactory = null;
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Create a database connection and open a <c>ISession</c> on it
        /// </summary>
        /// <returns></returns>
        public ISession OpenSession() {
            return SessionFactory.OpenSession();
        }

        /// <summary>
        /// Get a new stateless session
        /// </summary>
        /// <returns></returns>
        public IStatelessSession OpenStatelessSession() {
            return SessionFactory.OpenStatelessSession();
        }

    }

}