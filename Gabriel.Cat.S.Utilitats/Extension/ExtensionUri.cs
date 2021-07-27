using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
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
            Stream sr=(await url.GetResponse()).GetResponseStream();
            return new Bitmap(sr);
        }
        public static async Task<WebResponse> GetResponse([NotNull] this Uri url, IWebProxy proxy = default)
        {
            HttpWebRequest httpWebRequest;
            httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!Equals(proxy, default))
                httpWebRequest.Proxy = proxy;
            return await httpWebRequest.GetResponseAsync();
        }

       
        public static async Task<string> DownloadString(this Uri url, IWebProxy proxy = default)
        {
            string html;
            StreamReader srHtml;
            WebResponse response;
            try
            {
                smDownloading.WaitOne();
                response = await url.GetResponse(proxy);
                srHtml =new StreamReader(response.GetResponseStream());
                html = await srHtml.ReadToEndAsync();
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



