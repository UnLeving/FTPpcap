using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.WebRequestMethods;

namespace FtpClient
{
    public class Client
    {
        readonly Uri IPUri;
        readonly NetworkCredential networkCredential;
        List<string> listDirectoryDetails;
        readonly string localPath;

        public Client(Uri IPUri, NetworkCredential networkCredential)
        {
            this.IPUri = IPUri;
            this.networkCredential = networkCredential;
            listDirectoryDetails = new List<string>();
            localPath = "D:\\Downloads\\";
        }

        public async Task<List<string>> GetListDirectoryDetailsAsync()
        {
            var request = (FtpWebRequest)WebRequest.Create(IPUri);
            request.Credentials = networkCredential;
            request.Method = Ftp.ListDirectory;

            try
            {
                using (var response = (FtpWebResponse)await request.GetResponseAsync())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            while (!reader.EndOfStream)
                                listDirectoryDetails.Add(await reader.ReadLineAsync());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return listDirectoryDetails;
        }

        public async void DownloadFromFTPAsync(string fileName)
        {
            var request = (FtpWebRequest)WebRequest.Create(new Uri(IPUri, fileName));
            request.Credentials = networkCredential;
            request.Method = Ftp.DownloadFile;

            try
            {
                using (var response = (FtpWebResponse)await request.GetResponseAsync())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (var fileStream = new FileStream(localPath + fileName, FileMode.Create))
                        {
                            int bufSize = 2048;
                            byte[] buf = new byte[bufSize];
                            int readCount = await stream.ReadAsync(buf, 0, bufSize);
                            while (readCount > 0)
                            {
                                await fileStream.WriteAsync(buf, 0, readCount);
                                readCount = await stream.ReadAsync(buf, 0, bufSize);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}