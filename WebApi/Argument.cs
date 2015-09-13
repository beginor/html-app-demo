using System;
using System.IO;

namespace WebApi {

    /// <summary>
    /// 参数检查类
    /// </summary>
    public static class Argument {

        /// <summary>
        /// 检查一个对象参数不能为空， 否则抛出 ArgumentNullException 异常
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        public static void NotNull(object argument, string argumentName) {
            if (argument == null) {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// 检查一个字符串参数不能为 null 或者空字符串， 否则抛出 ArgumentNullException 异常
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        public static void NotNullOrEmpty(string argument, string argumentName) {
            if (string.IsNullOrEmpty(argument)) {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// 文件必须存在， 否则抛出 FileNotFoundException 异常
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        public static void ExistFile(string argument, string argumentName) {
            NotNullOrEmpty(argument, argumentName);
            if (!File.Exists(argument)) {
                throw new FileNotFoundException($"{argumentName} file {argument} does not exist.");
            }
        }

        /// <summary>
        /// 目录必须存在， 否则抛出 DirectoryNotFoundException 异常
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        public static void ExistDirectory(string argument, string argumentName) {
            NotNullOrEmpty(argument, argumentName);
            if (!Directory.Exists(argument)) {
                throw new DirectoryNotFoundException($"{argumentName} directory {argument} does not exist.");
            }
        }

    }

}