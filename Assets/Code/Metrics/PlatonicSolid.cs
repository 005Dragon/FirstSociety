using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace Code.Metrics
{
    public static class PlatonicSolid
    {
        public static class Octahedron
        {
            public static IEnumerable<Triangle> GetTriangles()
            {
                yield return new Triangle(Vector3.up, Vector3.forward, Vector3.right);
                yield return new Triangle(Vector3.up, Vector3.right, Vector3.back);
                yield return new Triangle(Vector3.up, Vector3.back, Vector3.left);
                yield return new Triangle(Vector3.up, Vector3.left, Vector3.forward);
            
                yield return new Triangle(Vector3.down, Vector3.right, Vector3.forward);
                yield return new Triangle(Vector3.down, Vector3.back, Vector3.right);
                yield return new Triangle(Vector3.down, Vector3.left, Vector3.back);
                yield return new Triangle(Vector3.down, Vector3.forward, Vector3.left);
            }
        }
    }
}