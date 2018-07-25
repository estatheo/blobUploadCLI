using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;

namespace blob_image_uploader
{
    public class BlobService
    {
        public async Task<string> Upload(string file, string connectionString, string reference)
        {
            var blobClient = CloudStorageAccount.Parse(connectionString).CreateCloudBlobClient();
            var containerReference = blobClient.GetContainerReference(reference);
            var blobReference = containerReference.GetBlockBlobReference(file.Split('\\').Last());
            blobReference.Properties.ContentType = "image/jpeg";
            using (var fileStream = File.OpenRead(file))
            {
                await blobReference.UploadFromStreamAsync(fileStream);
            }

            return blobReference.Uri.AbsoluteUri;

        }
    }
}
