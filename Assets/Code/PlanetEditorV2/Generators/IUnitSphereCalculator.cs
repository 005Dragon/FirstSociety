using System.Collections.Generic;
using Code.Metrics;

namespace Code.PlanetEditorV2.Generators
{
    public interface IUnitSphereCalculator
    {
        IEnumerable<Triangle> CalculateTriangles();
    }
}