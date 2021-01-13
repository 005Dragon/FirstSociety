using System.Collections.Generic;
using System.Linq;
using Code.Metrics;
using UnityEngine;

namespace Code.PlanetEditorV2.Generators
{
    public class UnitUvSphereCalculator : IUnitSphereCalculator
    {
        public int LatitudeLinesCount { get; set; }
        public int LongitudeLinesCount { get; set; }
        
        public UnitUvSphereCalculator(int latitudeLinesCount, int longitudeLinesCount)
        {
            LatitudeLinesCount = latitudeLinesCount;
            LongitudeLinesCount = longitudeLinesCount;
        }
        
        public IEnumerable<Triangle> CalculateTriangles()
        {
            Vector3[] vertexes = GetVertices(LatitudeLinesCount, LongitudeLinesCount).ToArray();
            
            foreach (Triangle triangle in GetTopTriangles(vertexes, LatitudeLinesCount, LongitudeLinesCount))
            {
                yield return triangle;
            }

            foreach (Triangle triangle in GetBodyTriangles(vertexes, LatitudeLinesCount, LongitudeLinesCount))
            {
                yield return triangle;
            }

            foreach (Triangle triangle in GetBottomTriangles(vertexes, LatitudeLinesCount, LongitudeLinesCount))
            {
                yield return triangle;
            }
        }

        private static IEnumerable<Triangle> GetTopTriangles(Vector3[] vertexes, int latitudeLinesCount, int longitudeLinesCount)
        {
            int bodyLatitudeLinesCount = latitudeLinesCount - 2;
            
            for (int longitudeIndex = 0; longitudeIndex < longitudeLinesCount; longitudeIndex++)
            {
                int nextLongitudeIndex = longitudeIndex == longitudeLinesCount - 1 ? 0 : longitudeIndex + 1;

                yield return new Triangle(
                    vertexes[0],
                    vertexes[1 + longitudeIndex * bodyLatitudeLinesCount],
                    vertexes[1 + nextLongitudeIndex * bodyLatitudeLinesCount]
                );
            }
        }

        private static IEnumerable<Triangle> GetBodyTriangles(Vector3[] vertexes, int latitudeLinesCount, int longitudeLinesCount)
        {
            int bodyLatitudeLinesCount = latitudeLinesCount - 2;
            
            for (int longitudeIndex = 0; longitudeIndex < longitudeLinesCount; longitudeIndex++)
            {
                int nextLongitudeIndex = longitudeIndex == longitudeLinesCount - 1 ? 0 : longitudeIndex + 1;
                
                for (int latitudeIndex = 0; latitudeIndex < bodyLatitudeLinesCount - 1; latitudeIndex++)
                {
                    yield return new Triangle(
                        vertexes[1 + latitudeIndex + longitudeIndex * bodyLatitudeLinesCount],
                        vertexes[2 + latitudeIndex + longitudeIndex * bodyLatitudeLinesCount],
                        vertexes[1 + latitudeIndex + nextLongitudeIndex * bodyLatitudeLinesCount]
                    );

                    yield return new Triangle(
                        vertexes[2 + latitudeIndex + longitudeIndex * bodyLatitudeLinesCount],
                        vertexes[2 + latitudeIndex + nextLongitudeIndex * bodyLatitudeLinesCount],
                        vertexes[1 + latitudeIndex + nextLongitudeIndex * bodyLatitudeLinesCount]
                    );
                }
            }
        }

        private static IEnumerable<Triangle> GetBottomTriangles(Vector3[] vertexes, int latitudeLinesCount, int longitudeLinesCount)
        {
            int bodyLatitudeLinesCount = latitudeLinesCount - 2;
            int lastIndex = 1 + longitudeLinesCount * bodyLatitudeLinesCount;
            
            for (int longitudeIndex = 0; longitudeIndex < longitudeLinesCount; longitudeIndex++)
            {
                int nextLongitudeIndex = longitudeIndex == longitudeLinesCount - 1 ? 0 : longitudeIndex + 1;

                yield return new Triangle(
                    vertexes[bodyLatitudeLinesCount + longitudeIndex * bodyLatitudeLinesCount],
                    vertexes[lastIndex],
                    vertexes[bodyLatitudeLinesCount + nextLongitudeIndex * bodyLatitudeLinesCount]
                );
            }
        }
        
        private static IEnumerable<Vector3> GetVertices(int latitudeLinesCount, int longitudeLinesCount)
        {
            yield return Vector3.up;
            
            float stepLatitude = GetLatitudeStepAngle(latitudeLinesCount);
            float stepLongitude = GetLongitudeStepAngle(longitudeLinesCount);

            float longitude = 0.0f;

            for (int longitudeIndex = 0; longitudeIndex < longitudeLinesCount; longitudeIndex++)
            {
                float latitude = stepLatitude; 
                
                for (int latitudeIndex = 1; latitudeIndex < latitudeLinesCount - 1; latitudeIndex++)
                {
                    Vector3 pointOnUnitSphere = Quaternion.AngleAxis(latitude, Vector3.forward) * Vector3.up;
                    pointOnUnitSphere = Quaternion.AngleAxis(longitude, Vector3.up) * pointOnUnitSphere;

                    yield return pointOnUnitSphere;
                    
                    latitude += stepLatitude;
                }

                longitude += stepLongitude;
            }
            
            yield return Vector3.down;
        }
        
        private static float GetLatitudeStepAngle(int latitudeLinesCount) => 180.0f / (latitudeLinesCount - 1);

        private static float GetLongitudeStepAngle(int longitudeLinesCount) => 360.0f / longitudeLinesCount;
    }
}