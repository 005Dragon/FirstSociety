using Code.Metrics;
using Code.Utils;
using UnityEngine;

namespace Code
{
    public class PlanetWalker : MonoBehaviour
    {
        public Transform Planet;

        public PlanetaryCoordinates planetaryCoordinates;

        public float Height;

        public float LerpSmoothing;
        
        private Quaternion _rotation;
        
        private float _angleAroundPole;
        
        // Start is called before the first frame update
        void Start()
        {
            if (planetaryCoordinates == null)
            {
                planetaryCoordinates = GetComponent<PlanetaryCoordinates>();
            }

            PlanetaryPosition planetaryPosition = planetaryCoordinates.GetPlanetaryPosition();
            
            _rotation = planetaryCoordinates.GetRotationAroundPlanet(planetaryPosition.Longitude, planetaryPosition.Latitude, Planet.up);
        }
        
        // Update is called once per frame
        void Update()
        {
            RangeHelpers.ToRange(ref Height, minValue: 0);

            UpdatePosition();
            UpdateRotation();
        }

        public void Move(float angle)
        {
            _rotation *= Quaternion.AngleAxis(angle, Vector3.back);
        }
        
        public void Rotate(float angle)
        {
            _rotation *= Quaternion.AngleAxis(angle, Vector3.up);
        }
        
        private void UpdatePosition()
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, GetTargetPosition(), Time.deltaTime * LerpSmoothing);
        }

        private void UpdateRotation()
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, GetTargetRotation(), Time.deltaTime * LerpSmoothing);
        }

        private Vector3 GetTargetPosition()
        {
            return _rotation * Planet.up * GetHeight();
        }

        private Quaternion GetTargetRotation()
        {
            Vector3 direction = GetDirection(_angleAroundPole);
            
            return _rotation * Quaternion.LookRotation(direction, Planet.up);
        }
        
        private Vector3 GetDirection(float angle)
        {
            var relationDirection = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));

            return Planet.rotation * relationDirection;
        }

        private float GetHeight()
        {
            Vector3 currentPosition = transform.position;
            Vector3 planetPosition = Planet.transform.position;
                
            Vector3 directionToPlanet = planetPosition - currentPosition;
            Ray groundRay = new Ray(currentPosition, directionToPlanet);

            Physics.Raycast(groundRay, out RaycastHit groundHit);
            
            return Vector3.Distance(planetPosition, groundHit.point) + Height;
        }
    }

    public class PlayerPlanetWalker : PlanetWalker
    {
        
    }
}
