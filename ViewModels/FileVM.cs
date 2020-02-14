using System;

namespace FileManager.ViewModels
{
    public class FileVM
    {   
        public bool isRename = false;
        public bool isDeleted { get; set; }
        public string renameFile;
        public bool showMenu = true;
        public DateTime dateCreated { get; set; }
        public string ID
        {
            get
            {
                return $"F{fullName.GetHashCode().ToString("x")}";
            }
        }
    
        public string name { get; set; }
        public string fullName { get; set; }    
        public string Icon { get; set; }        
        
    }
}
