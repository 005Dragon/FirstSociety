using System.Collections.Generic;
using System.Linq;
using Code.Metrics;
using Code.Utils;
using UnityEngine;

namespace Code.PlanetEditorV2.Generators
{
    public class UnitOctahedronSphereCalculator : IUnitSphereCalculator
    {
        public int Resolution { get; set; }

        public UnitOctahedronSphereCalculator(int resolution)
        {
            Resolution = resolution;
        }

        public IEnumerable<Triangle> CalculateTriangles()
        {
            return Split(PlatonicSolid.Octahedron.GetTriangles(), Resolution);
        }

        public IEnumerable<IEnumerable<Triangle>> CalculateTrianglesByFaces()
        {
            IEnumerable<Triangle> triangles = PlatonicSolid.Octahedron.GetTriangles();

            foreach (Triangle triangle in triangles)
            {
                yield return Split(triangle.ToSingleElementEnumerable(), Resolution);
            }
        }

        private static IEnumerable<Triangle> Split(IEnumerable<Triangle> triangles, int resolution)
        {
            for (int i = 0; i < resolution; i++)
            {
                triangles = triangles.SelectMany(Split);
            }

            return triangles;
        }
        
        private static IEnumerable<Triangle> Split(Triangle triangle)
        {
            Vector3 vertex12 = Vector3.Lerp(triangle.Vertex1, triangle.Vertex2, 0.5f).normalized;
            Vector3 vertex23 = Vector3.Lerp(triangle.Vertex2, triangle.Vertex3, 0.5f).normalized;
            Vector3 vertex13 = Vector3.Lerp(triangle.Vertex1, triangle.Vertex3, 0.5f).normalized;

            yield return new Triangle(triangle.Vertex1, vertex12, vertex13);
            yield return new Triangle(vertex12, vertex23, vertex13);
            yield return new Triangle(vertex12, triangle.Vertex2, vertex23);
            yield return new Triangle(vertex13, vertex23, triangle.Vertex3);
        }
    }
}