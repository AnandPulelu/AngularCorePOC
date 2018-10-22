using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace smb.Controllers
{
    [Route("api/[controller]")]
    public class UploadController:Controller
    {
        IConfiguration _configuration;
        public UploadController(IConfiguration configuration)
        {
            _configuration=configuration;
        }

        [HttpPost("[Action]")]
       // async public Task<IActionResult> SaveFile(IFormFile files)
       // public async Task<IHttpActionResult> SaveFile()
       async public Task<IActionResult> SaveFile(IFormFile files)
        {
            var filees = Request.Form.Files;
            String containername = "smbvideolibrary";
            String connectionstring =_configuration.GetConnectionString("azurestorage");
            CloudStorageAccount storageAccount=CloudStorageAccount.Parse(connectionstring);
           /* 
           CloudStorageAccount storageAccount = new CloudStorageAccount(
                new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
                    "azfuntest11a307",
                    "sNmFc3Nv8t6EP9+hVZCUSmAFLwfMQvYqG8E2t2PWnbsqr+vpKeAGomZNUtWfjZ/vvMEvX2v7wgZJeBVPbNRHcA=="),true
            ); 
             */

            CloudBlobClient blobClient=storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container=blobClient.GetContainerReference(containername);
            await container.CreateIfNotExistsAsync();

            BlobContainerPermissions permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Container
            };
            await container.SetPermissionsAsync(permissions);

            CloudBlockBlob blockBlob =container.GetBlockBlobReference(files.FileName);            
            using(var filestream=files.OpenReadStream())
            {
                await blockBlob.UploadFromStreamAsync(filestream);
            }
                        
            return null;
       /* return Json (new
        {
            name = blockBlob.Name,
            Uri=blockBlob.Uri,
            size = blockBlob.Properties.Length
        } 
        );*/
        }
    }
}