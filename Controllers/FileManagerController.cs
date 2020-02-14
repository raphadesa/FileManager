using FileManager.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace FileManager.Controllers
{
    [ApiController]
    [Route("fileManager")]
    public class FileManagerController : Controller
    {
        [HttpGet]
        [Route("download")]
        public FileResult Download(string fileName)
        {           
            var fi = new FileInfo(fileName);
            return File(System.IO.File.OpenRead(fileName), "application/octet", fi.Name);
        }
    }
}
