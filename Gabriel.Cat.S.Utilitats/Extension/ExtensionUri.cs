using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gabriel.Cat.S.Extension
{
    public static class ExtensionUri
    {
        static Semaphore smDownloading = new Semaphore(1, 1);
        public static Process Abrir([NotNull]this Uri url)
        {//source https://github.com/dotnet/wpf/issues/2566
            return Process.Start(new ProcessStartInfo
           {
               FileName = "cmd",
               WindowStyle = ProcessWindowStyle.Hidden,
               UseShellExecute = false,
               CreateNoWindow = true,
               Arguments = $"/c start \"{url.AbsoluteUri}\""
           });
        }
        public static async Task<Bitmap> GetBitmap([NotNull] this Uri url)
        {
            return new Bitmap(await (await url.GetResponse()).Content.ReadAsStreamAsync());
        }
        public static async Task<HttpResponseMessage> GetResponse([NotNull]this Uri url)
        {
            System.Net.Http.HttpClient httpClient;
            httpClient = new System.Net.Http.HttpClient();
            httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            return await httpClient.GetAsync(url);
        }
        public static async Task<HttpStatusCode> GetStatusCode([NotNull] this Uri url)
        {
            HttpStatusCode response;
           

            try
            {
     
                response = (await url.GetResponse()).StatusCode;

            }
            catch
            {
                response = HttpStatusCode.NotFound;
            }
            finally
            {

            }

            return response;
        }
       
        public static async Task<string> DownloadString(this Uri url)
        {
            string html;

            try
            {
                smDownloading.WaitOne();
     
                html = await (await url.GetResponse()).Content.ReadAsStringAsync();
            }
            catch
            {
                throw;
            }
            finally
            {
                smDownloading.Release();
            }
            return html;
        }

    }

}
