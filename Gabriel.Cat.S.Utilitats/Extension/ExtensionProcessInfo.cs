using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
    public static class ExtensionProcessInfo
    {
        public static void Hide(this ProcessStartInfo startInfo)
        {
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;//hacer metodo que lo encapsule!! y el que lo muestre tambien
        }
        public static void Show(this ProcessStartInfo startInfo)
        {
            startInfo.RedirectStandardOutput = false;
            startInfo.UseShellExecute = true;
            startInfo.CreateNoWindow = false;
            startInfo.WindowStyle = ProcessWindowStyle.Normal;//hacer metodo que lo encapsule!! y el que lo muestre tambien
        }
        public static string ExecuteBat(this System.Diagnostics.Process process, string batToExecute)
        {
            string path = Path.GetRandomFileName() + ".bat";
            StreamWriter swBat = File.CreateText(path);
            StringBuilder informacion = new StringBuilder();
            StreamReader str;
            swBat.Write(batToExecute);
            swBat.Close();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = path;
            startInfo.Hide();
            process.StartInfo = startInfo;
            process.Start();
            str = process.StandardOutput;
            while (!str.EndOfStream)
                informacion.Append( (char)str.Read());
            str.Close();
            File.Delete(path);
            return informacion.ToString().Substring(informacion.ToString().IndexOf('>') + batToExecute.Length + 4);
        }
    }
}
