using System;
using System.Collections.Generic;
using System.Linq;
using Code.Metrics;
using Code.PlanetEditorV2.Filters;
using Code.PlanetEditorV2.Settings;
using Code.Utils;
using UnityEngine;

namespace Code.PlanetEditorV2.Generators
{
    internal class SphereGenerator : GeneratorBase<SphereSettings>
    {
        public SphereShapeFilterBase[] SphereFilters;
        
        public GameObject Sphere;
        
        public Range<float> Magnitude;

        private GameObject[] _faces;

        public void Initialize(Action<GameObject> destroyImmediate)
        {
            DestroyImmediate = destroyImmediate;
        }

        protected override GameObject GenerateCore()
        {
            if (Sphere == null)
            {
                Sphere = new GameObject(Settings.SphereName);
            }
            
            Mesh[] meshes = Settings.SingleMesh
                ? GenerateMesh().ToSingleElementEnumerable().ToArray()
                : GenerateMeshes().ToArray();
            
            if (_faces != null)
            {
                if (_faces.Length != meshes.Length || _faces.Any(x => x == null))
                {
                    for (int i = 0; i < _faces.Length; i++)
                    {
                        DestroyImmediate(_faces[i]);
                    }

                    _faces = null;
                }
            }

            if (_faces == null)
            {
                _faces = new GameObject[meshes.Length];
                for (int i = 0; i < _faces.Length; i++)
                {
                    _faces[i] = new GameObject($"{Settings.SphereFaceName}{i}");
                    _faces[i].transform.parent = Sphere.transform;
                }
            }

            for (int i = 0; i < _faces.Length; i++)
            {
                UpdateFace(_faces[i], meshes[i]);
            }

            return Sphere;
        }

        private void UpdateFace(GameObject face, Mesh mesh)
        {
            var meshRender = face.GetComponent<MeshRenderer>();
            if (meshRender == null)
            {
                meshRender = face.AddComponent<MeshRenderer>();
            }
            meshRender.sharedMaterial = Settings.Material;

            var meshFilter = face.GetComponent<MeshFilter>();
            if (meshFilter == null)
            {
                meshFilter = face.AddComponent<MeshFilter>();
            }

            meshFilter.sharedMesh = mesh;
        }

        private Mesh GenerateMesh()
        {
            var unitSphereCalculator = new UnitOctahedronSphereCalculator(Settings.Resolution);
            List<Vector3> vertices = unitSphereCalculator.CalculateTriangles().SelectMany(x => x.GetVertexes()).ToList();
            
            ApplyFilters(vertices);
            
            return GenerateMesh(vertices);
        }
        
        private Mesh[] GenerateMeshes()
        {
            var unitSphereCalculator = new UnitOctahedronSphereCalculator(Settings.Resolution);

            IEnumerable<Triangle>[] trianglesByFaces = unitSphereCalculator.CalculateTrianglesByFaces().ToArray();

            List<Vector3> vertices = new List<Vector3>();

            foreach (IEnumerable<Triangle> triangles in trianglesByFaces)
            {
                vertices.AddRange(triangles.SelectMany(x => x.GetVertexes()));
            }
            
            ApplyFilters(vertices);

            var meshes = new Mesh[trianglesByFaces.Length];

            int countVerticesInMesh = trianglesByFaces.First().SelectMany(x => x.GetVertexes()).Count();
            
            for (int i = 0; i < meshes.Length; i++)
            {
                meshes[i] = GenerateMesh(vertices.Skip(i * countVerticesInMesh).Take(countVerticesInMesh));
            }

            return meshes;
        }

        private Mesh GenerateMesh(IEnumerable<Vector3> vertices)
        {
            Vector3[] verticesArray = vertices.ToArray();
            
            var mesh = new Mesh();
            
            mesh.Clear();
            mesh.vertices = verticesArray;
            mesh.triangles = Enumerable.Range(0, verticesArray.Length).ToArray();
            mesh.RecalculateNormals();

            return mesh;
        }

        private void ApplyFilters(List<Vector3> vertices)
        {
            Magnitude = new Range<float>(float.MaxValue, float.MinValue);
            
            for (int i = 0; i < vertices.Count; i++)
            {
                vertices[i] *= Settings.Radius;
                Magnitude.Expand(vertices[i].magnitude);
            }
            
            if (CollectionHelpers.IsNullOrEmpty(SphereFilters))
            {
                return;
            }
            
            foreach (SphereShapeFilterBase filter in SphereFilters.Where(x => x != null && x.Enabled))
            {
                Range<float> currentMagnitudeRange = new Range<float>(float.MaxValue, float.MinValue);
                
                for (int i = 0; i < vertices.Count; i++)
                {
                    vertices[i] = filter.Evaluate(vertices[i], Magnitude);
                    currentMagnitudeRange.Expand(vertices[i].magnitude);
                }

                Magnitude = currentMagnitudeRange;
            }
        }
    }
}