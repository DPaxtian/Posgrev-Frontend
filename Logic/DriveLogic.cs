using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using static Google.Apis.Drive.v3.DriveService;
using Google.Apis.Upload;

namespace Posgrev_Frontend.Logic
{
    public class DriveLogic
    {
        private static DriveService GetService()
        {
            var tokenResponse = new TokenResponse
            {
                AccessToken = "ya29.a0AfB_byCoe_Sl8FTdGcjA4GQq1HIz5XHCF3KiE-QNovg5kh6cQoGWRwILf8gEdKx8sKYqAb3ld5kc5EF-p9D1P5DUmVo6mw_GVcQCFKqQT1-ky5QwwfPELavefuRhB90EUKBf7Rmi5qW7piEbqqlYD-ZjSStafGZ5sTjkaCgYKAT0SARMSFQHGX2MitkE-ZIs-F7lWg_qzU3G2fQ0171",
                RefreshToken = "1//04MAXn1_QkCXGCgYIARAAGAQSNwF-L9IrQ39sBDBBgWx48e-V84ny3c41qvc-Qt28kHEHDWpmCaqWAqZKJ6bY6wB0QU7o-SBtwb4",
            };


            var applicationName = "Posgrev Files"; // Use the name of the project in Google Cloud
            var username = "posgrev@gmail.com"; // Use your email


            var apiCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = "799200777765-d2hrcp5r48u157r4grnfb96b10e22huu.apps.googleusercontent.com",
                    ClientSecret = "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"
                },
                Scopes = new[] { Scope.Drive },
                DataStore = new FileDataStore(applicationName)
            });


            var credential = new UserCredential(apiCodeFlow, username, tokenResponse);


            var service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });
            return service;
        }


        public string CreateFolder(string parentFolderName, string nestedFolderName)
        {
            var service = GetService();

            var parentFolder = GetFolderByName(service, parentFolderName);
            if (parentFolder == null)
            {
                var parentDriveFolder = new Google.Apis.Drive.v3.Data.File();
                parentDriveFolder.Name = parentFolderName;
                parentDriveFolder.MimeType = "application/vnd.google-apps.folder";
                var parentCommand = service.Files.Create(parentDriveFolder);
                var parentFile = parentCommand.Execute();

                var nestedFolder = GetFolderByName(service, nestedFolderName, parentFile.Id);
                if (nestedFolder == null)
                {
                    var nestedDriveFolder = new Google.Apis.Drive.v3.Data.File();
                    nestedDriveFolder.Name = nestedFolderName;
                    nestedDriveFolder.MimeType = "application/vnd.google-apps.folder";
                    nestedDriveFolder.Parents = new List<string> { parentFile.Id };
                    var nestedCommand = service.Files.Create(nestedDriveFolder);
                    var nestedFile = nestedCommand.Execute();

                    return nestedFile.Id;
                }
                else
                {
                    Console.WriteLine($"La carpeta anidada '{nestedFolderName}' ya existe en la carpeta padre '{parentFolderName}'.");
                    return nestedFolder.Id;
                }
            }
            else
            {
                var nestedFolder = GetFolderByName(service, nestedFolderName, parentFolder.Id);
                if (nestedFolder == null)
                {
                    var nestedDriveFolder = new Google.Apis.Drive.v3.Data.File();
                    nestedDriveFolder.Name = nestedFolderName;
                    nestedDriveFolder.MimeType = "application/vnd.google-apps.folder";
                    nestedDriveFolder.Parents = new List<string> { parentFolder.Id };
                    var nestedCommand = service.Files.Create(nestedDriveFolder);
                    var nestedFile = nestedCommand.Execute();

                    return nestedFile.Id;
                }
                else
                {
                    Console.WriteLine($"La carpeta anidada '{nestedFolderName}' ya existe en la carpeta padre '{parentFolderName}'.");
                    return nestedFolder.Id;
                }
            }
        }


        public string CreateIndicatorFolder(string parentFolderId, string nestedFolderName)
        {
            var service = GetService();

            var nestedFolder = GetFolderByName(service, nestedFolderName, parentFolderId);
            if (nestedFolder == null)
            {
                var nestedDriveFolder = new Google.Apis.Drive.v3.Data.File();
                nestedDriveFolder.Name = nestedFolderName;
                nestedDriveFolder.MimeType = "application/vnd.google-apps.folder";
                nestedDriveFolder.Parents = new List<string> { parentFolderId };
                var nestedCommand = service.Files.Create(nestedDriveFolder);
                var nestedFile = nestedCommand.Execute();

                return nestedFile.Id;
            }
            else
            {
                Console.WriteLine($"La carpeta anidada '{nestedFolderName}' ya existe en la carpeta padre con ID '{parentFolderId}'.");
                return nestedFolder.Id;
            }
        }



        private Google.Apis.Drive.v3.Data.File GetFolderByName(DriveService service, string folderName, string parentFolderId = null)
        {
            var request = service.Files.List();
            request.Q = $"name = '{folderName}' and mimeType = 'application/vnd.google-apps.folder'";
            if (!string.IsNullOrEmpty(parentFolderId))
            {
                request.Q += $" and '{parentFolderId}' in parents";
            }

            var result = request.Execute();

            return result.Files?.FirstOrDefault();
        }


        public async Task<string> UploadFile(Stream fileStream, string fileName, string mimeType, string folderId)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = fileName,
                Parents = new[] { folderId }
            };

            var driveService = GetService();
            var request = driveService.Files.Create(fileMetadata, fileStream, mimeType);
            request.Fields = "id";

            var uploadProgress = await request.UploadAsync();

            if (uploadProgress.Status == UploadStatus.Failed)
            {
                Console.WriteLine("Error al subir el archivo a Google Drive");
                return null;
            }

            Console.WriteLine("Archivo subido con éxito. ID: " + request.ResponseBody?.Id);
            return request.ResponseBody?.Id;
        }


    }
}
