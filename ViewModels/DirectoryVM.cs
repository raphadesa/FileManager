using System;
using System.Collections.Generic;

namespace FileManager.ViewModels
{
    public class DirectoryVM
    {
        public bool isDeleted { get; set; }
        public bool expanded = true;
        public List<FileVM> files;
        public List<DirectoryVM> directories;
        public string name;
        public string oldLocation;
        public string renameDir;
        public string createDir;
        public string fullName;        
        public bool isRename = false;
        public bool isCreate = false;
        public bool isRootNode = false;
        public bool showMenu = true;
        public DateTime dateCreated { get; set; }
        public string ID
        {
            get
            {
                return $"D{fullName.GetHashCode().ToString("x")}";
            }
        }
        public DirectoryVM() { }

        public DirectoryVM(string name, string fullName, List<DirectoryVM> directories, List<FileVM> files)
        {
            this.name = name;
            this.fullName = fullName;
            this.directories = directories;
            this.files = files;
            this.oldLocation = fullName;           
            
        }        

        public void toggle()
        {         
            expanded = !expanded;
        }                        

        public string getIcon()
        {
            if (expanded)            
                return "/img/FileManager/FileIcons/folder_blue.png";            
            return "/img/FileManager/FileIcons/folder_blue_parent.png"; ;
        }
    }
}

