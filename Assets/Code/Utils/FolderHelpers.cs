namespace Code.Utils
{
    public static class FolderHelpers
    {
        public const string PrefabsFolderPath = "Assets/Components/Prefabs";
        
        public static void CreateIfNotExists(string path)
        {
            if (!UnityEngine.Windows.Directory.Exists(path))
            {
                UnityEngine.Windows.Directory.CreateDirectory(path);
            }
        }

        public static void Recreate(string path)
        {
            if (UnityEngine.Windows.Directory.Exists(path))
            {
                UnityEngine.Windows.Directory.Delete(path);
            }
            
            UnityEngine.Windows.Directory.CreateDirectory(path);
        }

        public static string PathCombine(string path, string otherPath)
        {
            return $"{path}/{otherPath}";
        }
    }
}