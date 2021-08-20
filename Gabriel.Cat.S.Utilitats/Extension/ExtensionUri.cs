using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gabriel.Cat.S.Extension
{
    public static class ExtensionUri
    {
        static Semaphore smDownloading = new Semaphore(1, 1);
        public static Process Abrir([NotNull]this Uri uri)
        {
            Process process;
            string url = uri.ToString();
            try
            {
                process=Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    process = Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    process = Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    process = Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
            return process;
        }
        public static Bitmap DownloadBitmap([NotNull] this Uri url)
        {
            Stream sr=new MemoryStream(IGetWebClient(url).DownloadData(url));
            return new Bitmap(sr);
        }
        public static byte[] DownloadData([NotNull] this Uri url)
        {
            return IGetWebClient(url).DownloadData(url);
        }
        static WebClient IGetWebClient( Uri url)
        {
            WebClient client = new WebClient();
            //client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 10.0;     Windows NT 6.2; WOW64; Trident/6.0)");
            if (url.ToString().Contains("https"))
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            }
            else
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault;
            }
            return  client;
        }

       
        public static string DownloadString(this Uri url, IWebProxy proxy = default)
        {
            string html;
            WebClient client;
            try
            {
                smDownloading.WaitOne();
                client = IGetWebClient(url);
                html = client.DownloadString(url);
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



