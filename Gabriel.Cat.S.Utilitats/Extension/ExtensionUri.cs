using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Gabriel.Cat.S.Extension
{
    public static class ExtensionUri
    {
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
    }

}
