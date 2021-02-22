using System;
using System.Collections.Generic;
using System.Linq;
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
            List<(string, Material)> materials = GetPrepareComponents<MeshRenderer, Material>(source, x => x.sharedMaterial).ToList();

            IEnumerable<(string, Texture)> textures = GetPrepareTextures(materials);

            SaveComponents(textures, targetFolder, "Textures", "asset");
            SaveComponents(materials, targetFolder, "Materials", "mat");
        }

        private static IEnumerable<(string, Texture)> GetPrepareTextures(IEnumerable<(string, Material)> materials)
        {
            HashSet<Texture> textures = new HashSet<Texture>();

            foreach ((string, Material) material in materials)
            {
                string[] texturePropertyNames = material.Item2.GetTexturePropertyNames();

                foreach (var texturePropertyName in texturePropertyNames)
                {
                    Texture texture = material.Item2.GetTexture(texturePropertyName);
                    
                    if (texture == null)
                    {
                        continue;
                    }

                    if (textures.Add(texture))
                    {
                        yield return ($"{material.Item1}{texturePropertyName}", texture);
                    }
                }
            }
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

                if (value == null)
                {
                    continue;
                }
                
                if (values.Add(value))
                {
                    yield return (container.gameObject.name, value);
                }
            }
        }
    }
}