using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Utils
{
    public static class PrefabHelper
    {
        public static void Save(GameObject source, string name = null)
        {
            FolderHelpers.CreateIfNotExists(FolderHelpers.PrefabsFolderPath);

            if (name == null)
            {
                name = $"{source.name}_{DateTime.Now.Ticks}";
            }

            string targetFolder = FolderHelpers.PathCombine(FolderHelpers.PrefabsFolderPath, name);
            
            FolderHelpers.Recreate(targetFolder);
            
            SaveMeshes(source, targetFolder);
            SaveMaterials(source, targetFolder);

            UnityEditor.PrefabUtility.SaveAsPrefabAsset(source, FolderHelpers.PathCombine(targetFolder, $"{name}.prefab"));
        }

        private static void SaveMeshes(GameObject source, string targetFolder)
        {
            IEnumerable<(string, Mesh)> meshes = GetPrepareComponents<MeshFilter, Mesh>(source, x => x.sharedMesh);
            
            SaveComponents(meshes, targetFolder, "Meshes", "asset");
        }
        
        private static void SaveMaterials(GameObject source, string targetFolder)
        {
            var meshes = GetPrepareComponents<MeshRenderer, Material>(source, x => x.sharedMaterial);
            
            SaveComponents(meshes, targetFolder, "Materials", "mat");
        }

        private static void SaveComponents<TValue>(
            IEnumerable<(string, TValue)> components, 
            string targetFolder, 
            string componentFolderName,
            string componentTypeExtension)
            where TValue : UnityEngine.Object
        {
            string componentsFolder = FolderHelpers.PathCombine(targetFolder, componentFolderName);

            FolderHelpers.Recreate(componentsFolder);

            foreach ((string, TValue) component in components)
            {
                string filePath = FolderHelpers.PathCombine(componentsFolder, $"{component.Item1}.{componentTypeExtension}");
                
                UnityEditor.AssetDatabase.CreateAsset(component.Item2, filePath);
            }
        }
        
        private static IEnumerable<(string, TValue)> GetPrepareComponents<TContainer, TValue>(
            GameObject source, 
            Func<TContainer, TValue> getValue)
            where TContainer : Component
            where TValue : UnityEngine.Object
        {
            HashSet<TValue> values = new HashSet<TValue>();
            
            TContainer[] containers = source.GetComponentsInChildren<TContainer>();

            foreach (TContainer container in containers)
            {
                TValue value = getValue(container);
                
                if (values.Add(value))
                {
                    yield return (container.gameObject.name, value);
                }
            }
        }
        //
        // // TODO Вынести в отделный класс
        // public void SavePrefab()
        // {
        //     // string folderPath = $"Assets/Components/Models/Planet{DateTime.Now.Ticks}";
        //     // UnityEngine.Windows.Directory.CreateDirectory(folderPath);
        //     //
        //     // string meshesPath = $"{folderPath}/Meshes";
        //     // UnityEngine.Windows.Directory.CreateDirectory(meshesPath);
        //     //
        //     // string filePath;
        //     // MeshFilter meshFilter;
        //     //
        //     // for (int i = 0; i < _planetGroundGenerator._planetGroundFaces.Length; i++)
        //     // {
        //     //     meshFilter = _planetGroundGenerator._planetGroundFaces[i].GetComponent<MeshFilter>();
        //     //
        //     //     filePath = $"{meshesPath}/GroundFace{i}.asset";
        //     //     
        //     //     UnityEditor.AssetDatabase.CreateAsset(meshFilter.sharedMesh, filePath);
        //     // }
        //     //
        //     // string materialsPath = $"{folderPath}/Materials";
        //     // UnityEngine.Windows.Directory.CreateDirectory(materialsPath);
        //     //
        //     // filePath = $"{materialsPath}/GroundMaterial.mat";
        //     // UnityEditor.AssetDatabase.CreateAsset(_planetGroundGenerator.GroundMaterial, filePath);
        //     //
        //     // filePath = $"{materialsPath}/WaterMaterial.mat";
        //     // MeshRenderer meshRenderer = _planetWaterGenerator.Water.GetComponent<MeshRenderer>();
        //     // UnityEditor.AssetDatabase.CreateAsset(meshRenderer.sharedMaterial, filePath);
        //     //
        //     // filePath = $"{meshesPath}/Water.asset";
        //     //
        //     // meshFilter = _planetWaterGenerator.Water.GetComponent<MeshFilter>();
        //     //
        //     // UnityEditor.AssetDatabase.CreateAsset(meshFilter.sharedMesh, filePath);
        //     //
        //     // UnityEditor.PrefabUtility.SaveAsPrefabAsset(Planet, $"{folderPath}/Planet.prefab");
        //     //
        //     // DestroyImmediate(Planet);
        //     //
        //     // Generate();
        // }
    }
}