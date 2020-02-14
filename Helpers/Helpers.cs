using System.IO;

namespace FileManager.Helpers
{
    public static class Helpers
    {
        public static bool IsDirectory(this string path)
        {
            if (path == null) return true;
            return ((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory);
        }        

    }
}
