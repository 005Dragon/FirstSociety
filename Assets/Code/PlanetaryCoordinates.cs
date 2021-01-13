using Code.Metrics;
using Code.Utils;
using UnityEngine;

namespace Code
{
    public class PlanetaryCoordinates : MonoBehaviour
    {
        public Transform Planet;

        [ReadOnly]
        public float PlanetRadius;

        [ReadOnly] 
        public PlanetaryPosition Position;

        /// <summary>
        /// Получает <see cref="PlanetaryPosition"/> даже если поле <see cref="Position"/> еще не рассчитано.
        /// </summary>
        public PlanetaryPosition GetPlanetaryPosition()
        {
            return new PlanetaryPosition(GetLatitude(), GetLongitude(), GetAltitude(GetPlanetRadius()));
        }

        public Quaternion GetRotationAroundPlanet(float longitude, float latitude, Vector3 planetPoleDirection)
        {
            return
                Quaternion.AngleAxis(longitude, planetPoleDirection) *
                Quaternion.AngleAxis(latitude, Planet.right);
        }
        
        // Start is called before the first frame update
        private void Start()
        {
            PlanetRadius = GetPlanetRadius();
            Position = new PlanetaryPosition(GetLatitude(), GetLongitude(), GetAltitude(PlanetRadius));

            Vector3 planetPoleDirection = Planet.up;
            Quaternion rotationAroundPlanet = GetRotationAroundPlanet(Position.Longitude, Position.Latitude, planetPoleDirection); 

            transform.position = rotationAroundPlanet * planetPoleDirection * (PlanetRadius + Position.Altitude);
        }
        
        // Update is called once per frame
        private void Update()
        {
            PlanetRadius = GetPlanetRadius();
            Position = new PlanetaryPosition(GetLatitude(), GetLongitude(), GetAltitude(PlanetRadius));
        }

        private float GetLongitude()
        {
            return Vector3.Angle(Planet.forward, Vector3.ProjectOnPlane(transform.position, Planet.up));
        }
        
        private float GetLatitude()
        {
            return Vector3.Angle(Planet.up, transform.position);
        }

        private float GetAltitude(float planetRadius)
        {
            return Vector3.Distance(transform.position, Planet.position) - planetRadius;
        }
        
        private float GetPlanetRadius()
        {
            Vector3 currentPosition = transform.position;
            Vector3 planetPosition = Planet.transform.position;
                
            Vector3 directionToPlanet = planetPosition - currentPosition;
            Ray groundRay = new Ray(currentPosition, directionToPlanet);

            Physics.Raycast(groundRay, out RaycastHit groundHit);
            
            return Vector3.Distance(planetPosition, groundHit.point);
        }
    }
}
