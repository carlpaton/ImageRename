using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ImageRename.Ca
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Oh snap time to rename things!");
            Console.WriteLine("------------------------------");

            var baseDirectory = @"C:\Dev\blog2\carlpaton.github.io\public\d\";
            var imageDirectorys = Directory.GetDirectories(baseDirectory);
            var report = new List<ReportModel>();

            foreach (var imageDirectory in imageDirectorys)
            {
                var folderName = GetFolderName(imageDirectory);

                report.Add(new ReportModel()
                {
                    FolderName = folderName,
                    Images = GetImages(baseDirectory, folderName)
                });
            }

            var serializedReport = JsonSerializer
                .Serialize(report);

            Console.WriteLine($"Found {report.Count} folders with a ... Im too tired to try figure out the image count :D");
            File.WriteAllText($"{Guid.NewGuid()}.txt", serializedReport);

            RenameImages(baseDirectory, report);
            Console.ReadLine();
        }

        private static void RenameImages(string baseDirectory, List<ReportModel> reports)
        {
            foreach (var report in reports)
            {
                foreach (var image in report.Images)
                {
                    var sourceFileName = Path.Combine(baseDirectory, report.FolderName, image.BeforeName);
                    var destinationFileName = Path.Combine(baseDirectory, report.FolderName, image.AfterName);

                    if (!File.Exists(destinationFileName))
                    {
                        File.Copy(sourceFileName, destinationFileName);
                        File.Delete(sourceFileName);
                    }

                    // windows is case sensitive
                    if (!image.AfterName.Equals(image.BeforeName, StringComparison.Ordinal)) 
                    {
                        var guid = Guid.NewGuid();
                        var destinationFileNameUnique = Path.Combine(baseDirectory, report.FolderName, guid + "_" + image.AfterName);

                        if (!File.Exists(destinationFileNameUnique))
                        {
                            File.Copy(sourceFileName, destinationFileNameUnique);
                            File.Delete(sourceFileName);

                            var destinationFileNameFinal = destinationFileNameUnique.Replace(guid.ToString() + "_", "");
                            File.Copy(destinationFileNameUnique, destinationFileNameFinal);
                            File.Delete(destinationFileNameUnique);
                        }
                    }
                }
            }
        }

        private static List<Image> GetImages(string baseDirectory, string folderName)
        {
            var reponseCollection = new List<Image>();
            var images = Directory.GetFiles(Path.Combine(baseDirectory, folderName));

            foreach (var image in images)
            {
                var imageName = image
                    .Split('\\')
                    .ToList()
                    .Last();

                //if (imageName.Contains(" ") || imageName.Contains("_")) 
                //{
                    reponseCollection.Add(new Image() 
                    {
                        BeforeName = imageName,
                        AfterName = GetNewImageName(imageName)
                    });
                //}
            }

            return reponseCollection;
        }

        private static string GetNewImageName(string imageName)
        {
            return imageName
                .ToLower()
                .Replace(" ", "-")
                .Replace("_", "-");
        }

        private static string GetFolderName(string imageDirectory)
        {
            var imageDirectoryArray = imageDirectory.Split('\\');
            return imageDirectoryArray
                .ToList()
                .Last();
        }
    }
}
