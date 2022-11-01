using Microsoft.AspNetCore.Mvc.ModelBinding;
using Relive.Server.API.DTOs.ErrorDTOs;
using Relive.Server.Core.Entities.Errors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Relive.Server.API.Utilities
{
    public static class Utilities
    {
        /// <summary>
        /// This generates a error repsonse for validation error
        /// </summary>
        /// <param name="ModelState"></param>
        /// <returns>Error Response</returns>
        public static ErrorResponse GenerateValidationErrorResponse(ModelStateDictionary ModelState)
        {
            ErrorResponse errorResponse = new ErrorResponse
            {
                Message = "Data Validation Failed",
                ErrorList = new List<Error>()
            };
            var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            foreach (var error in errors)
            {
                errorResponse.ErrorList.Add(new Error { Message = error });
            }
            return errorResponse;
        }

        public static ErrorResponse GenerateGeneralErrorResponse(string[] errors)
        {
            ErrorResponse errorResponse = new ErrorResponse
            {
                Message = "There were errors",
                ErrorList = new List<Error>()
            };
            foreach (var error in errors)
            {
                errorResponse.ErrorList.Add(new Error { Message = error });
;           }
            return errorResponse;
        }

        /// <summary>
        /// This method saves the image and returns the url of the image
        public static string SaveImage(Guid userId, string imageType, string bas64Image)
        {
            try
            {
                string imageName = Guid.NewGuid().ToString();
                string currentDir = Directory.GetCurrentDirectory();
                string dirPath = currentDir + $"\\Images\\{userId}\\{imageType}";
                Directory.CreateDirectory(dirPath);
                string imagePath = Path.Combine(dirPath, $"{imageName}.jpg");
                byte[] imgBytes = Convert.FromBase64String(bas64Image);
                using (FileStream fs = new FileStream(imagePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read))
                {
                    fs.Write(imgBytes, 0, imgBytes.Length);
                }
                return $"https://localhost:5001/image/{userId}/{imageType}/{imageName}";
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// This function trims and capitalizes the first letter
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string TrimAndCapitalize(string data)
        {
            string res = "";
            data = data.Trim();
            var words = data.Split(" ");
            foreach (string word in words)
            {
                string firstLetter = word[0].ToString().ToUpper();
                string rest = word.Substring(1).ToLower();
                res = res + firstLetter + rest + " ";
            }
            res = res.Trim();
            return res;
        }
    }
}
