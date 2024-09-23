namespace Mvc03.Demo.PL.Helper
{
    public class DocumentSettings
    {
        //1.Upload
        public static string Upload(IFormFile formFile, string FolderName)
        {
            //1.Get location of folder
            //string FolderPath =$"C:\\Users\\precision\\Desktop\\.Net\\MVC\\Mvc03.Demo.Solution\\Mvc03.Demo.PL\\wwwroot\\Files\\{FolderName}";
            //string FolderPath = Directory.GetCurrentDirectory() + $"\\wwwroot\\Files\\{FolderName}";
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), $"\\wwwroot\\Files\\{FolderName}");

            //2.GetFileName and make sure it is unique
            string fileName = $"{Guid.NewGuid()}{formFile.FileName}";
            //3.Get FilePath
            string FilePath = Path.Combine(FolderPath, fileName);
            //4.File Stream data per sec
            using var FileStream = new FileStream(FilePath, FileMode.Create);
            formFile.CopyTo(FileStream);

            return fileName;
        }
        
        //1.Delete
        public static void Delete(string fileName, string FolderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"\\wwwroot\\Files\\{FolderName}", fileName);
            if (File.Exists(filePath)) {
                File.Delete(filePath);
            }

        }
    }
}
