using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Net;
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
        public static Bitmap GetBitmap([NotNull] this Uri url)
        {
            WebRequest request = WebRequest.Create(url.AbsoluteUri);
            WebResponse response = request.GetResponse();
            System.IO.Stream responseStream = response.GetResponseStream();
            return new Bitmap(responseStream);
        }
        public static HttpStatusCode GetStatusCode([NotNull] this Uri url)
        {
            HttpStatusCode response; 
            HttpWebRequest httpReq;
            HttpWebResponse httpRes=default;
            
            try
            {
                httpReq = (HttpWebRequest)WebRequest.Create(url.AbsoluteUri);

                httpReq.AllowAutoRedirect = false;
                httpRes = (HttpWebResponse)httpReq.GetResponse();

                response = httpRes.StatusCode;
               
            }
            catch
            {
                response = HttpStatusCode.NotFound;
            }
            finally
            {
                if(!Equals(httpRes, default))
                    httpRes.Close();
            }

            return response;
        }
       
        public static async Task<string> DownloadStringAsync(this Uri url)
        {
            string result = string.Empty;
            Task download = new Task(new Action(() => result = url.DownloadString()));
            await download;
            return result;
        }
        public static string DownloadString(this Uri url)
        {
            string html;
            WebClient wbClient;

            try
            {
                smDownloading.WaitOne();
                wbClient = new WebClient();
                wbClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                html = wbClient.DownloadString(url);
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
