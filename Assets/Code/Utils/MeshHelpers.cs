using UnityEngine;

namespace Code.Utils
{
    #if UNITY_EDITOR
    public static class MeshHelpers {

        [UnityEditor.MenuItem("CONTEXT/MeshFilter/Save Mesh...")]
        public static void SaveMeshInPlace (UnityEditor.MenuCommand menuCommand) 
        {
            if (menuCommand.context is MeshFilter meshFilter)
            {
                Mesh mesh = meshFilter.sharedMesh;
                SaveMesh(mesh, mesh.name, false, true);
            }
        }

        [UnityEditor.MenuItem("CONTEXT/MeshFilter/Save Mesh As New Instance...")]
        public static void SaveMeshNewInstanceItem (UnityEditor.MenuCommand menuCommand) 
        {
            if (menuCommand.context is MeshFilter meshFilter)
            {
                Mesh mesh = meshFilter.sharedMesh;
                SaveMesh(mesh, mesh.name, true, true);
            }
        }

        public static void SaveMesh (Mesh mesh, string name, bool makeNewInstance, bool optimizeMesh) 
        {
            string path = UnityEditor.EditorUtility.SaveFilePanel("Save Separate Mesh Asset", "Assets/", name, "asset");
            
            if (string.IsNullOrEmpty(path))
            {
                return;
            }
        
            path = UnityEditor.FileUtil.GetProjectRelativePath(path);

            Mesh meshToSave = (makeNewInstance) ? Object.Instantiate(mesh) : mesh;
		
            if (optimizeMesh)
            {
                UnityEditor.MeshUtility.Optimize(meshToSave);
            }
        
            UnityEditor.AssetDatabase.CreateAsset(meshToSave, path);
            UnityEditor.AssetDatabase.SaveAssets();
        }
    }
    #endif
}