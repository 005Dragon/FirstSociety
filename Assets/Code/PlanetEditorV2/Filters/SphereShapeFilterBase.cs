using System;
using Code.Metrics;
using UnityEngine;

namespace Code.PlanetEditorV2.Filters
{
    public abstract class SphereShapeFilterBase
    {
        public abstract bool Enabled { get; }
        
        public abstract Vector3 Evaluate(Vector3 value, Range<float> magnitude);
    }
}