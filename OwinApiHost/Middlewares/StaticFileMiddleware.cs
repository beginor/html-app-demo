using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;
using System.IO;

namespace OwinApiHost.Middlewares {

    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class StaticFileMiddleware {

        private AppFunc next;
        private readonly StaticFileMiddlewareOptions options;

        public void Initialize(AppFunc next) {
            this.next = next;
        }

        public StaticFileMiddleware(StaticFileMiddlewareOptions options) {
            this.options = options;
        }

        public Task Invoke(IDictionary<string, object> env) {
            var key = Nowin.OwinKeys.ResponseBody;
            var requestPath = (string)env["owin.RequestPath"];
            if (requestPath.EndsWith("/")) {
                requestPath += options.DefaultFile;
            }
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, options.RootDirectory, requestPath.Substring(1));
            if (File.Exists(filePath)) {
                var response = (Stream)env["owin.ResponseBody"];
                var fileInfo = new FileInfo(filePath);
                //response.ContentLength = fileInfo.Length;
                //response.ContentType = options.GetMimeType(fileInfo.Extension);
                var buff = File.ReadAllBytes(filePath);
                var headers = (IDictionary<string, string[]>)env["owin.ResponseHeaders"];
                headers["Content-Type"] = new[] {options.GetMimeType(fileInfo.Extension)};
                headers["Content-Length"] = new[] {buff.Length.ToString()};
                return response.WriteAsync(buff, 0, buff.Length);
            }
            return next.Invoke(env);
        }

    }

    public class StaticFileMiddlewareOptions {

        protected IDictionary<string, string> MimeTypes;

        public string RootDirectory { get; }

        public string DefaultFile { get; }

        public string DefaultMimeType { get; }

        public StaticFileMiddlewareOptions(string rootDirectory, string defaultFile, string defaultMimeType) {
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