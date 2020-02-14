using System.Collections.Generic;

namespace FileManager.ViewModels
{
    public class FileManagerVM
    {
        public DirectoryVM rootNode { get; set; }
        public List<DirectoryVM> tree { get; set; }
    }
}
