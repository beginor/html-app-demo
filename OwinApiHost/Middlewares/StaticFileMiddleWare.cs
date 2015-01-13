using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;
using System.IO;

namespace OwinApiHost.Middlewares {

    public class StaticFileMiddleWare : OwinMiddleware {

        private readonly StaticFileMiddleWareOptions options;

        public StaticFileMiddleWare(OwinMiddleware next, StaticFileMiddleWareOptions options)
            : base(next) {
            this.options = options;
        }

        public override Task Invoke(IOwinContext context) {
            var requestPath = context.Request.Path.ToString();
            if (requestPath.EndsWith("/")) {
                requestPath += options.DefaultFile;
            }
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, options.RootDirectory, requestPath.Substring(1));
            if (File.Exists(filePath)) {
                var response = context.Response;
                var fileInfo = new FileInfo(filePath);
                response.ContentLength = fileInfo.Length;
                response.ContentType = options.GetMimeType(fileInfo.Extension);
                var buff = File.ReadAllBytes(filePath);
                return response.WriteAsync(buff);
            }
            return Next.Invoke(context);
        }

    }

    public class StaticFileMiddleWareOptions {

        protected IDictionary<string, string> MimeTypes;

        public string RootDirectory { get; }

        public string DefaultFile { get; }

        public string DefaultMimeType { get; }

        public StaticFileMiddleWareOptions(string rootDirectory, string defaultFile, string defaultMimeType) {
            RootDirectory = rootDirectory;
            DefaultFile = defaultFile;
            DefaultMimeType = defaultMimeType;
        }

        public virtual string GetMimeType(string extension) {
            var ext = extension;
            if (ext.StartsWith(".")) {
                ext = ext.Substring(1);
            }
            string contentType;
            switch (ext) {
            case "html":
                contentType = "text/html";
                break;
            case "js":
                contentType = "application/javascript";
                break;
            case "json":
                contentType = "application/json";
                break;
            case "css":
                contentType = "text/css";
                break;
            case "png":
                contentType = "image/png";
                break;
            case "jpg":
                contentType = "image/jpeg";
                break;
            case "gif":
                contentType = "image/gif";
                break;
            case "ico":
                contentType = "image/x-icon";
                break;
            case "woff":
                contentType = "application/x-font-woff";
                break;
            default:
                contentType = DefaultMimeType;//"application/octet-stream";
                break;
            }
            return contentType;
        }

    }
}