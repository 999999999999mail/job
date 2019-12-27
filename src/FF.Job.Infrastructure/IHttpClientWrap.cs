using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FF.Job.Infrastructure
{
    public interface IHttpClientWrap
    {
        Task<string> GetStringAsync(string url, Encoding encoding = null);
        Task<string> PostStringAsync(string url, string postData, IDictionary<string, string> headers = null, string contentType = "application/x-www-form-urlencoded", int timeout = 0, Encoding encoding = null);
        Task<HttpResult> GetAsync(string url, Encoding encoding = null);
        Task<HttpResult> PostAsync(string url, string postData, IDictionary<string, string> headers = null, string contentType = "application/x-www-form-urlencoded", int timeout = 0, Encoding encoding = null);
    }

    public class HttpResult
    {
        public string Content { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }

    public class HttpClientWrap : IHttpClientWrap
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClientWrap(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetStringAsync(string url, Encoding encoding = null)
        {
            string resp;
            var client = _httpClientFactory.CreateClient();

            using (HttpResponseMessage responseMessage = await client.GetAsync(url))
            {
                byte[] resultBytes = await responseMessage.Content.ReadAsByteArrayAsync();
                resp = (encoding ?? Encoding.UTF8).GetString(resultBytes);
            }

            return resp;
        }

        public async Task<HttpResult> GetAsync(string url, Encoding encoding = null)
        {
            HttpResult resp = new HttpResult();
            var client = _httpClientFactory.CreateClient();

            using (HttpResponseMessage responseMessage = await client.GetAsync(url))
            {
                resp.StatusCode = responseMessage.StatusCode;
                byte[] resultBytes = await responseMessage.Content.ReadAsByteArrayAsync();
                resp.Content = (encoding ?? Encoding.UTF8).GetString(resultBytes);
            }

            return resp;
        }

        public async Task<string> PostStringAsync(string url, string postData, IDictionary<string, string> headers = null, string contentType = "application/x-www-form-urlencoded", int timeout = 0, Encoding encoding = null)
        {
            string resp;

            var client = _httpClientFactory.CreateClient();
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
            if (timeout > 0)
            {
                client.Timeout = new TimeSpan(0, 0, timeout);
            }
            using (HttpContent content = new StringContent(postData ?? "", encoding ?? Encoding.UTF8))
            {
                if (contentType != null)
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                }
                using (HttpResponseMessage responseMessage = await client.PostAsync(url, content))
                {
                    byte[] resultBytes = await responseMessage.Content.ReadAsByteArrayAsync();
                    resp = (encoding ?? Encoding.UTF8).GetString(resultBytes);
                }
            }

            return resp;
        }

        public async Task<HttpResult> PostAsync(string url, string postData, IDictionary<string, string> headers = null, string contentType = "application/x-www-form-urlencoded", int timeout = 0, Encoding encoding = null)
        {
            HttpResult resp = new HttpResult();

            var client = _httpClientFactory.CreateClient();
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
            if (timeout > 0)
            {
                client.Timeout = new TimeSpan(0, 0, timeout);
            }
            using (HttpContent content = new StringContent(postData ?? "", encoding ?? Encoding.UTF8))
            {
                if (contentType != null)
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                }
                using (HttpResponseMessage responseMessage = await client.PostAsync(url, content))
                {
                    resp.StatusCode = responseMessage.StatusCode;
                    byte[] resultBytes = await responseMessage.Content.ReadAsByteArrayAsync();
                    resp.Content = (encoding ?? Encoding.UTF8).GetString(resultBytes);
                }
            }

            return resp;
        }
    }

    public static class HttpClientWrapServiceCollectionExtensions
    {
        public static IServiceCollection AddHttpClientWrap(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddHttpClient();
            services.TryAddTransient<IHttpClientWrap, HttpClientWrap>();
            return services;
        }
    }
}
