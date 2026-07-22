using System.Data;

namespace GlobalApi.GlobalClasses
{
    public class FileUpload
    {
        public string ProcessUploadedFile(string FilePath,IFormFile? model)
        {
            string uniqueFileName = string.Empty;

            if (model != null)
            {
                string uploadsFolder = Path.Combine(FilePath);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.CopyTo(fileStream);
                }
                return uniqueFileName;
            }
            return "default_user.png";
            
        }
    }
}
