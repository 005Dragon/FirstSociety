using Code.Utils;
using UnityEngine;

namespace Code
{
    public class TargetCamera : MonoBehaviour
    {
        public Transform Target;
        
        public float AltitudeSpeed;
        public float LongitudeSpeed;
        public float LatitudeSpeed;
        
        public float Altitude;
        public float Longitude;
        public float Latitude;
        
        private Camera _camera;
        private Transform _anchor;

        // Start is called before the first frame update
        void Start()
        {
            _camera = GetComponent<Camera>();
            _anchor = transform.parent;
        }

        // Update is called once per frame
        void Update()
        {
            UpdateCameraRotation();
            UpdateCameraPosition();
            UpdateAnchorRotation();
            UpdateAnchorPosition();
        }
        
        private void UpdateCameraRotation()
        {
            Quaternion targetDirection = Quaternion.LookRotation(Target.position - _camera.transform.position);
            
            _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation, targetDirection, Time.deltaTime);
        }

        private void UpdateAnchorRotation()
        {
            Latitude = Mathf.Lerp(Latitude, GetDestinationLatitude(), Time.deltaTime);

            RangeHelpers.ToRange(ref Latitude, minValue: -89.9f, maxValue: 89.9f);
            
            Longitude = Mathf.Lerp(Longitude, GetDestinationLongitude(), Time.deltaTime);
            
            _anchor.eulerAngles = new Vector3(Latitude, Longitude, 0);
        }

        private void UpdateCameraPosition()
        {
            Altitude = Mathf.Lerp(Altitude, GetDestinationAltitude(), Time.deltaTime);

            RangeHelpers.ToRange(ref Altitude, minValue: 0);
            
            _camera.transform.localPosition = new Vector3(0, 0, -Altitude);
        }

        private void UpdateAnchorPosition()
        {
            _anchor.transform.position = Vector3.Lerp(_anchor.transform.position, Target.transform.position, Time.deltaTime);
        }

        private float GetDestinationLongitude()
        {
            return Longitude - Input.GetAxis("Horizontal") * LongitudeSpeed;
        }

        private float GetDestinationLatitude()
        {
            return Latitude + Input.GetAxis("Vertical") * LatitudeSpeed;
        }

        private float GetDestinationAltitude()
        {
            return Altitude - Input.GetAxis("Mouse ScrollWheel") * AltitudeSpeed;
        }
    }
}
