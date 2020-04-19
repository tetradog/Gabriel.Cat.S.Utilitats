using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
    public static class ExtensionUri
    {
        public static Process Abrir(this Uri url)
        {//source https://github.com/dotnet/wpf/issues/2566
            return Process.Start(new ProcessStartInfo
           {
               FileName = "cmd",
               WindowStyle = ProcessWindowStyle.Hidden,
               UseShellExecute = false,
               CreateNoWindow = true,
               Arguments = $"/c start {url.AbsoluteUri}"
           });
        }
    }
}
