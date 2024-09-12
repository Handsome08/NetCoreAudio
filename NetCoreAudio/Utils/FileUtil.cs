using System.IO;
using System.Text;

namespace NetCoreAudio.Utils
{
    internal static class FileUtil
    {
        private const string TempDirName = "temp";
        private const int MaxFileNameLength = 255;
        
        public static string CheckFileToPlay(string originalFileName)
        {
            var fileNameToReturn = originalFileName;
            if (originalFileName.Contains(" ") || originalFileName.Length > MaxFileNameLength / 2)
            {
                var shortPath = new StringBuilder(MaxFileNameLength);
                if (WindowsUtil.GetShortPathName(originalFileName, shortPath, MaxFileNameLength) > 0)
                {
                    fileNameToReturn = shortPath.ToString();
                }
                else
                {
                    Directory.CreateDirectory(TempDirName);
                    fileNameToReturn = TempDirName + Path.DirectorySeparatorChar +
                                       Path.GetFileName(originalFileName).Replace(" ", "");
                    File.Copy(originalFileName, fileNameToReturn);
                }
            }
            return fileNameToReturn;
        }

        public static void ClearTempFiles()
        {
            if (Directory.Exists(TempDirName))
                Directory.Delete(TempDirName, true);
        }
    }
}
