using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FtpClient
{
    class RunFTP
    {
        readonly NetworkCredential networkCredential;
        readonly Uri IPUri;
        readonly string phoneNumber;
        readonly Client client;
        List<string> resultsList;

        public RunFTP(string key, string datePath, string phoneNumber)
        {

            this.phoneNumber = phoneNumber;
            resultsList = new List<string>();
            networkCredential = new NetworkCredential
            {
                UserName = "UserName",
                Password = "Password"
            };

            switch (key)
            {
                case "key5":
                    IPUri = new Uri("ftp://0.0.0.0/");
                    break;
                case "key8":
                    IPUri = new Uri("ftp://0.0.0.0/");
                    break;
                case "key9":
                    IPUri = new Uri("ftp://0.0.0.0/");
                    break;
            }

            client = new Client(new Uri(IPUri, datePath), networkCredential);
        }

        public async Task<List<string>> GetFileList()
        {
            foreach (var stringValue in await client.GetListDirectoryDetailsAsync())
                if (stringValue.Contains(phoneNumber))
                    resultsList.Add(stringValue);
            
            return resultsList;
        }

        public void Downloading(string fileName)
        {
            client.DownloadFromFTPAsync(fileName);
        }
    }
}