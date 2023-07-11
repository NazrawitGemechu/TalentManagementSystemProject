using System.ComponentModel.DataAnnotations;

namespace TalentManagement.UI.Models.ViewModels
{
    public class ValidateFileAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int MaxContentLength = 1024 * 1024 * 2; //Max 2 MB file
            string[] AllowedFileExtensions = new
            string[] {
    ".docx",
    ".pdf"
    
            };
            var file = value as IFormFile;
            if (file == null)
                return false;
            else if (!AllowedFileExtensions.Contains((file != null) ?
              file.FileName.Substring(file.FileName.LastIndexOf('.')).ToLower() :
              string.Empty))
            {
                ErrorMessage = "Please upload Your CV of type: " +
                 string.Join(", ", AllowedFileExtensions);
                return false;
            }
            else if (file.Length > MaxContentLength)
            {
                ErrorMessage = "Your File is too large, maximum allowed size is : " +
                 (MaxContentLength / 1024).ToString() + "MB";
                return false;
            }
            else
                return true;
        }
    }
}