using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string accountName = "blobstoragevarun";
            string accountKey = "2O5M378JsdGgMpQbLQ0/W6Fdb3diBe4q3aoS2DNbRsWSrWQMp6zlGi48SPPrN0s6Juj8rzQCSTk4zEtQqr4S3Q==";

            try
            {
                StorageCredentials creds = new StorageCredentials(accountName, accountKey);
                CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);

                CloudBlobClient client = account.CreateCloudBlobClient();

                CloudBlobContainer sampleContainer = client.GetContainerReference("samples");
                sampleContainer.CreateIfNotExists();

                //CloudBlockBlob blob = sampleContainer.GetBlockBlobReference("APictureFile.jpg");
                //using (Stream file = System.IO.File.OpenRead("MiniAzure.jpg"))
                //{
                //    blob.UploadFromStream(file);
                //}

                // Loop over items within the container and output the length and URI.
                foreach (IListBlobItem item in sampleContainer.ListBlobs(null, false))
                {
                    if (item.GetType() == typeof(CloudBlockBlob))
                    {
                        CloudBlockBlob blob = (CloudBlockBlob)item;

                        Console.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);

                    }
                    else if (item.GetType() == typeof(CloudPageBlob))
                    {
                        CloudPageBlob pageBlob = (CloudPageBlob)item;

                        Console.WriteLine("Page blob of length {0}: {1}", pageBlob.Properties.Length, pageBlob.Uri);

                    }
                    else if (item.GetType() == typeof(CloudBlobDirectory))
                    {
                        CloudBlobDirectory directory = (CloudBlobDirectory)item;

                        Console.WriteLine("Directory: {0}", directory.Uri);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
