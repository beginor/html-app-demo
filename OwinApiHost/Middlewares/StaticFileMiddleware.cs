using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Net;

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
            var requestPath = (string)env["owin.RequestPath"];
            if (requestPath.EndsWith("/")) {
                requestPath += options.DefaultFile;
            }

            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, options.RootDirectory, requestPath.Substring(1));
            var fileInfo = new FileInfo(filePath);
            var ticks = fileInfo.LastWriteTimeUtc.Ticks;
            //
            var requestHeaders = (IDictionary<string, string[]>)env["owin.RequestHeaders"];
            if (requestHeaders.ContainsKey("If-None-Match")) {
                var tagValue = requestHeaders["If-None-Match"];
                if (tagValue != null && tagValue.Length > 0) {
                    long tag;
                    if (long.TryParse(tagValue[0], out tag)) {
                        if (ticks == tag) {
                            env["owin.ResponseStatusCode"] = (int)HttpStatusCode.NotModified;
                            env["owin.ResponseReasonPhrase"] = "Not Modified";
                            return Task.FromResult(0);
                        }
                    }
                }
            }
            if (File.Exists(filePath)) {
                var responseBody = (Stream)env["owin.ResponseBody"];

                var buff = File.ReadAllBytes(filePath);
                var headers = (IDictionary<string, string[]>)env["owin.ResponseHeaders"];
                headers["Content-Type"] = new[] { options.GetMimeType(fileInfo.Extension) };
                headers["Content-Length"] = new[] { buff.Length.ToString() };
                headers["ETag"] = new [] { ticks.ToString() };
                return responseBody.WriteAsync(buff, 0, buff.Length);
            }
            return next.Invoke(env);
        }

    }

    public class StaticFileMiddlewareOptions {

        public string RootDirectory { get; private set; }

        public string DefaultFile { get; private set; }

        public string DefaultMimeType { get; private set; }

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