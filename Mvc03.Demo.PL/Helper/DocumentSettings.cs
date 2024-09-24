namespace Mvc03.Demo.PL.Helper
{
    public static class DocumentSettings
    {
        //1.Upload
        public static string Upload(IFormFile file, string folderName)
        {
            //1.Get location of folder
            //string FolderPath =$"C:\\Users\\precision\\Desktop\\.Net\\MVC\\Mvc03.Demo.Solution\\Mvc03.Demo.PL\\wwwroot\\Files\\{FolderName}";
            //string FolderPath = Directory.GetCurrentDirectory() + $"\\wwwroot\\Files\\{FolderName}";
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Files\\{folderName}");
            //2.GetFileName and make sure it is unique
            string fileName = $"{Guid.NewGuid()}{file.FileName}";
            //3.Get FilePath
            string filePath = Path.Combine(folderPath, fileName);
            //4.File Stream data per sec
            using var FileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(FileStream);

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
