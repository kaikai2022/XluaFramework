using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;

namespace AssetBundleServer
{
    public class MainClass
    {
        public static void WatchDog(object processID)
        {
            Console.WriteLine("Watching parent processID: {0}!", processID);
            Process processById = Process.GetProcessById((int) processID);
            while (processById == null || !processById.HasExited)
            {
                Thread.Sleep(1000);
            }

            Console.WriteLine("Exiting because parent process has exited!");
            Environment.Exit(0);
        }

        // Token: 0x06000003 RID: 3 RVA: 0x000020B0 File Offset: 0x000002B0
        public static int Run(string[] args)
        {
            string text = "";
            if (args.Length == 0)
            {
                Console.WriteLine("No commandline arguments, harcoded debug mode...");
            }
            else
            {
                text = args[0];
            }

            int num;
            if (args.Length >= 2)
            {
                num = int.Parse(args[1]);
            }
            else
            {
                num = 0;
            }

            if (num != 0)
            {
                if (cache0 == null)
                {
                    cache0 = new ParameterizedThreadStart(MainClass.WatchDog);
                }

                Thread thread = new Thread(cache0);
                thread.Start(num);
            }

            bool detailedLogging = false;
            int num2 = 7888;
            Console.WriteLine("Starting up asset bundle server.", num2);
            Console.WriteLine("Port: {0}", num2);
            Console.WriteLine("Directory: {0}", text);
            HttpListener httpListener = new HttpListener();
            httpListener.Prefixes.Add(string.Format("http://*:{0}/", num2));
            httpListener.Start();
            for (;;)
            {
                Console.WriteLine("Waiting for request...");
                HttpListenerContext context = httpListener.GetContext();
                MainClass.WriteFile(context, text, detailedLogging);
            }

            return cache0.GetHashCode();
        }

        // Token: 0x06000004 RID: 4 RVA: 0x000021C0 File Offset: 0x000003C0
        private static void WriteFile(HttpListenerContext ctx, string basePath, bool detailedLogging)
        {
            HttpListenerRequest request = ctx.Request;
            string rawUrl = request.RawUrl;
            string text = basePath + rawUrl;
            if (detailedLogging)
            {
                Console.WriteLine(
                    "Requesting file: '{0}'. Relative url: {1} Full url: '{2} AssetBundleDirectory: '{3}''",
                    new object[]
                    {
                        text,
                        request.RawUrl,
                        request.Url,
                        basePath
                    });
            }
            else
            {
                Console.Write("Requesting file: '{0}' ... ", request.RawUrl);
            }

            HttpListenerResponse response = ctx.Response;
            try
            {
                using (FileStream fileStream = File.OpenRead(text))
                {
                    string fileName = Path.GetFileName(text);
                    response.ContentLength64 = fileStream.Length;
                    response.SendChunked = false;
                    response.ContentType = "application/octet-stream";
                    response.AddHeader("Content-disposition", "attachment; filename=" + fileName);
                    byte[] array = new byte[65536];
                    using (BinaryWriter binaryWriter = new BinaryWriter(response.OutputStream))
                    {
                        int count;
                        while ((count = fileStream.Read(array, 0, array.Length)) > 0)
                        {
                            binaryWriter.Write(array, 0, count);
                            binaryWriter.Flush();
                        }

                        binaryWriter.Close();
                    }

                    Console.WriteLine("completed.");
                    response.OutputStream.Close();
                    response.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" failed.");
                Console.WriteLine(
                    "Requested file failed: '{0}'. Relative url: {1} Full url: '{2} AssetBundleDirectory: '{3}''",
                    new object[]
                    {
                        text,
                        request.RawUrl,
                        request.Url,
                        basePath
                    });
                Console.WriteLine("Exception {0}: {1}'", ex.GetType(), ex.Message);
                response.Abort();
            }
        }

        // Token: 0x04000001 RID: 1
        [CompilerGenerated] private static ParameterizedThreadStart cache0;
    }
}