using Code.Metrics;
using Code.PlanetEditor.Filters;
using Code.PlanetEditor.Settings;
using Code.Utils;
using UnityEngine;

namespace Code.PlanetEditor
{
    public class PlanetGenerator : MonoBehaviour
    {
        [ReadOnly] 
        public Range<float> Altitude;
        
        [Range(2, 256)]
        public int Resolution = 10;

        public bool AutoApply = true;

        public ShapeSettings ShapeSettings;
        public ColourSettings ColourSettings;

        [HideInInspector]
        public bool ShapeSettingsFoldout;
        
        [HideInInspector]
        public bool ColourSettingsFoldout;
        
        [SerializeField, HideInInspector]
        private MeshFilter[] _meshFilters;
        private PlaneMeshGenerator[] _planeMeshes;

        private ShapeCalculator _shapeCalculator;

        public void Generate()
        {
            Initialize();
            GenerateMesh();
            GenerateColours();
        }
        
        public void OnColourSettingsUpdated()
        {
            if (AutoApply)
            {
                Initialize();
                GenerateColours();
            }
        }

        public void OnShapeSettingsUpdate()
        {
            if (AutoApply)
            {
                Initialize();
                GenerateMesh();
            }
        }

        private void Initialize()
        {
            _shapeCalculator = new ShapeCalculator(ShapeSettings);
            
            Vector3[] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};

            if (CollectionHelpers.IsNullOrEmpty(_meshFilters))
            {
                _meshFilters = new MeshFilter[directions.Length];    
            }
            
            _planeMeshes = new PlaneMeshGenerator[directions.Length];

            for (int i = 0; i < directions.Length; i++)
            {
                if (_meshFilters[i] == null)
                {
                    var meshObject = new GameObject("mesh");
                    meshObject.transform.parent = transform;

                    meshObject.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                    _meshFilters[i] = meshObject.AddComponent<MeshFilter>();
                    _meshFilters[i].sharedMesh = new Mesh();
                }
                
                _planeMeshes[i] = new PlaneMeshGenerator(_shapeCalculator, _meshFilters[i].sharedMesh, Resolution, directions[i]);
            }
        }

        private void GenerateMesh()
        {
            Altitude = new Range<float>(float.MaxValue, float.MinValue);
            
            foreach (PlaneMeshGenerator planeMesh in _planeMeshes)
            {
                planeMesh.Construct(out Range<float> altitude);
                
                Altitude.Expand(altitude);
            }
        }

        private void GenerateColours()
        {
            foreach (MeshFilter meshFilter in _meshFilters)
            {
                meshFilter.GetComponent<MeshRenderer>().sharedMaterial.color = ColourSettings.PlanetColor;
            }
        }
    }
}
