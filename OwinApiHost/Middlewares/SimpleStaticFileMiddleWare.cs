using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using System.IO;

namespace OwinApiHost.Middlewares {

    public class SimpleStaticFileMiddleWare : OwinMiddleware {

        private string rootDirectory;

        public SimpleStaticFileMiddleWare(OwinMiddleware next, string rootDirectory)
            : base(next) {
            this.rootDirectory = rootDirectory;
        }

        public override Task Invoke(IOwinContext context) {
            var requestPath = context.Request.Path.ToString();
            var filePath = Path.Combine(rootDirectory, requestPath.Substring(1));
            if (File.Exists(filePath)) {
                var response = context.Response;
                var fileInfo = new FileInfo(filePath);
                response.ContentLength = fileInfo.Length;
                response.ContentType = GetContentType(fileInfo.Extension);
                var buff = File.ReadAllBytes(filePath);
                return response.WriteAsync(buff);
            }
            return base.Next.Invoke(context);
        }

        private static string GetContentType(string extension) {
            var ext = extension;
            if (ext.StartsWith(".")) {
                ext = ext.Substring(1);
            }
            var contentType = "";
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
                contentType = "octet-stream";
                break;
            }
            return contentType;
        }
    }
}