using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using FileManager.ViewModels;
using FileManager.Helpers;
using Microsoft.Extensions.Options;

namespace FileManager.Services
{
    public class FileManagerService
    {
       IOptions<Configuration> config;
       public FileManagerService(IOptions<Configuration> _config)
        {
            config = _config;
        }
        /// <summary>
        /// A method to populate a TreeView with directories, subdirectories, etc
        /// </summary>
        /// <param name="dir">The path of the directory</param>
        /// <param name="node">The "master" node, to populate</param>
        public void PopulateTree(string dir, DirectoryVM node)
        {
            // get the information of the directory
            DirectoryInfo di = new DirectoryInfo(dir);
            if (node.name == null && node.fullName == null)
            {
                var newNode = addDirectory(di, addFiles(di));
                newNode.isRootNode = true;
                newNode.dateCreated = di.CreationTime;
                if (node.directories == null)
                    node.directories = new List<DirectoryVM>();
                node.directories.Add(newNode);
                PopulateTree(di.FullName, newNode);
            }
            else
            {

                // loop through each subdirectory
                foreach (DirectoryInfo d in di.GetDirectories())
                {
                    var filesVM = addFiles(d);
                    var newNode = addDirectory(d, filesVM);
                    PopulateTree(d.FullName, newNode);
                    node.directories.Add(newNode);

                }
            }
        }

        public List<FileVM> addFiles(DirectoryInfo di)
        {
            var filesVM = new List<FileVM>();
            foreach (var f in di.GetFiles())
            {
                var ext = f.Extension.Replace(".", "");
                var fileVM = new FileVM { name = f.Name, fullName = f.FullName, dateCreated = f.CreationTime, Icon = $"/img/FileManager/FileIcons/{ext}.png" };
                filesVM.Add(fileVM);
            }
            return filesVM;
        }

        public DirectoryVM addDirectory(DirectoryInfo di, List<FileVM> files)
        {
            var vm = new DirectoryVM(di.Name, di.FullName, new List<DirectoryVM>(), files);
            vm.dateCreated = di.CreationTime;
            return vm;
        }


        public FileManagerVM getTreeData()
        {           
            var dirVM = new DirectoryVM();
            dirVM.isRootNode = true;
            var di = new DirectoryInfo(config.Value.urlBase);
            if (!di.Exists)
                di.Create();
            PopulateTree(di.FullName, dirVM);
            var rootNode = addDirectory(di, new List<FileVM>());
            rootNode.isRootNode = true;
            return new FileManagerVM { rootNode = rootNode, tree = dirVM.directories };
        }
        public void deleteFile(string fileName)
        {
            File.Delete(fileName);
        }
        public void renameFile(FileVM file)
        {
            var fi = new FileInfo(file.fullName);
            var renameFile = Path.Combine(fi.Directory.FullName, file.renameFile);
            MoveFile(file.fullName, renameFile);
        }

        public void MoveFile(string oldfile, string newFile)
        {
            if (oldfile != newFile)
            {
                if (!File.Exists(newFile))
                {
                    File.Move(oldfile, newFile);
                }
            }
        }
        
        public void deleteDirectory(string dir)
        {
            Directory.Delete(dir, true);
        }
        public void createDir(DirectoryVM dir)
        {
            var currentDir = new DirectoryInfo(dir.fullName);
            var createDir = Path.Combine(currentDir.FullName, dir.createDir);
            if (!Directory.Exists(createDir))
                Directory.CreateDirectory(createDir);
        }
        public void renameDir(DirectoryVM dir)
        {
            var currentDir = new DirectoryInfo(dir.fullName);
            var renameDir = Path.Combine(currentDir.Parent.FullName, dir.renameDir);
            MoveFolder(currentDir.FullName, renameDir);
        }

        public void dragAndDropDir(DirectoriesVM vm)
        {
            if (vm.origDir.IsDirectory())
            {
                var di = new DirectoryInfo(vm.origDir);
                var target = Path.Combine(vm.destDir, di.Name);
                MoveFolder(vm.origDir, target);
            }
            else
            {
                var fi = new FileInfo(vm.origDir);
                var destDir = Path.Combine(vm.destDir, fi.Name);
                MoveFile(vm.origDir, destDir);
            }
        }
        public void MoveFolder(string sourceDir, string destDir)
        {
            if (sourceDir != destDir)
            {
                try
                {
                    Directory.Move(sourceDir, destDir);
                }
                catch { }
            }
        }       
        
    }

}
