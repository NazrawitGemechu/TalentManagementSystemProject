﻿//namespace TalentManagement.UI.Models.ViewModels
//{
//    public class FileUploadHelper
//    {
//        public async Task<string> SaveFileAsync(IFormFile file, string pathToUplaod)
//        {
//            string fileUrl = string.Empty;
//            if (!Directory.Exists(pathToUplaod))
//                System.IO.Directory.CreateDirectory(pathToUplaod); //Create Path of not exists
//            string pathwithfileName = pathToUplaod + "\\" + GetFileName(file, true);
//            using (var fileStream = new FileStream(pathwithfileName, FileMode.Create))
//            {
//                await file.CopyToAsync(fileStream);
//            }
//            fileUrl = pathwithfileName;
//            return fileUrl;
//        }
//        public string SaveFile(IFormFile file, string pathToUplaod)
//        {
//            string fileUrl = string.Empty;
//            string pathwithfileName = pathToUplaod + "\\" + GetFileName(file, true);
//            using (var fileStream = new FileStream(pathwithfileName, FileMode.Create))
//            {
//                file.CopyTo(fileStream);
//            }
//            return fileUrl;
//        }
//        //public string GetFileName(IFormFile file, bool BuildUniqeName)
//        //{
//        //    //string fileName = string.Empty;
//        //    //string strFileName = file.FileName.Substring(
//        //    //  file.FileName.LastIndexOf("\\"))
//        //    // .Replace("\\", string.Empty);
//        //    //string fileExtension = GetFileExtension(file);
//        //    //if (BuildUniqeName)
//        //    //{
//        //    //    string strUniqName = GetUniqueName("file");
//        //    //    fileName = strUniqName + fileExtension;
//        //    //}
//        //    //else
//        //    //{
//        //    //    fileName = strFileName;
//        //    //}
//        //    //return fileName;
//        //}
//        public string GetUniqueName(string preFix)
//        {
//            string uName = preFix + DateTime.Now.ToString()
//             .Replace("/", "-")
//             .Replace(":", "-")
//             .Replace(" ", string.Empty)
//             .Replace("PM", string.Empty)
//             .Replace("AM", string.Empty);
//            return uName;
//        }
//        public string GetFileExtension(IFormFile file)
//        {
//            string fileExtension;
//            fileExtension = (file != null) ?
//             file.FileName.Substring(file.FileName.LastIndexOf('.')).ToLower() :
//             string.Empty;
//            return fileExtension;
//        }
//    }
//}
