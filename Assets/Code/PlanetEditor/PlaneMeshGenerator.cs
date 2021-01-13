using System.Collections.Generic;
using Code.Metrics;
using UnityEngine;

namespace Code.PlanetEditor
{
    public class PlaneMeshGenerator
    {
        private readonly ShapeCalculator _shapeCalculator;
        private readonly Mesh _mesh;
        private readonly int _resolution;
        private readonly Vector3 _normalVector;

        private readonly Vector3 _axisX;
        private readonly Vector3 _axisY;

        public PlaneMeshGenerator(ShapeCalculator shapeCalculator, Mesh mesh, int resolution, Vector3 normalVector)
        {
            _shapeCalculator = shapeCalculator;
            _mesh = mesh;
            _resolution = resolution;
            _normalVector = normalVector;
            
            _axisX = new Vector3(normalVector.y, normalVector.z, normalVector.x);
            _axisY = Vector3.Cross(normalVector, _axisX);
        }

        public void Construct(out Range<float> altitude)
        {
            var vertices = new Vector3[_resolution * _resolution];
            var triangles = new int[(_resolution - 1) * (_resolution - 1) * 6];

            int verticesIndex = 0;
            int triangleIndex = 0;
            
            altitude = new Range<float>(float.MaxValue, float.MinValue);
            
            for (int y = 0; y < _resolution; y++)
            {
                for (int x = 0; x < _resolution; x++)
                {
                    var percent = new Vector2(x, y) / (_resolution - 1);

                    Vector3 pointOnUnitCube = _normalVector + (percent.x - 0.5f) * 2 * _axisX + (percent.y - 0.5f) * 2 * _axisY;
                    Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                    Vector3 pointOnSphere = _shapeCalculator.CalculatePointOnSphere(pointOnUnitSphere);
                    altitude.Expand(pointOnSphere.magnitude);
                    
                    vertices[verticesIndex] = pointOnSphere;

                    if (x != _resolution - 1 && y != _resolution - 1)
                    {
                        triangles[triangleIndex++] = verticesIndex;
                        triangles[triangleIndex++] = verticesIndex + _resolution + 1;
                        triangles[triangleIndex++] = verticesIndex + _resolution;

                        triangles[triangleIndex++] = verticesIndex;
                        triangles[triangleIndex++] = verticesIndex + 1;
                        triangles[triangleIndex++] = verticesIndex + _resolution + 1;
                    }
                    
                    verticesIndex++;
                }
            }

            _mesh.Clear();
            _mesh.vertices = vertices;
            _mesh.triangles = triangles;
            _mesh.RecalculateNormals();
        }
    }
}
