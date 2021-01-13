using Code.Metrics;
using UnityEngine;

namespace Code
{
	public class WalkOnSphere : MonoBehaviour
	{
		#region vars
		
		public PlanetaryCoordinates planetaryCoordinates;

		public float rotSpeed = 50;
		public float moveSpeed;
		public float rotDamp;
		public float moveDamp;
		public float height;
		public float jumpHeight;
		[Range(.1f, 10f)] public float gravity;
		public float radius;

		public Transform planet;
		protected Transform trans;
		protected Transform parent;

		public float angle;
		protected float curJumpHeight = 0;
		protected float jumpTimer;
		protected bool jumping;
		
		protected Quaternion rotation = Quaternion.identity;

		#endregion

		#region Unity methods

		void Start()
		{
			trans = transform;
			parent = transform.parent;
			
			PlanetaryPosition planetaryPosition = planetaryCoordinates.GetPlanetaryPosition();
			
			Debug.Log(planetaryPosition.Latitude + ":" + planetaryPosition.Longitude);

			rotation = Quaternion.identity *
			           Quaternion.AngleAxis(planetaryPosition.Longitude, Vector3.up) *
			           Quaternion.AngleAxis(planetaryPosition.Latitude, Vector3.right);


		}

		void Update()
		{
			//parent.position = planet.position; // If you want to have a moving planet

			

			if (Input.GetKey(KeyCode.LeftShift))
				Position(Input.GetAxis("Horizontal") * -moveSpeed, 0);
			else
				Rotation(Input.GetAxis("Horizontal") * -rotSpeed);

			if (Input.GetButtonDown("Jump") && !jumping)
			{
				jumping = true;
				jumpTimer = Time.time;
			}

			if (jumping)
			{
				curJumpHeight = Mathf.Sin((Time.time - jumpTimer) * gravity) * jumpHeight;
				if (curJumpHeight <= -.01f)
				{
					curJumpHeight = 0;
					jumping = false;
				}
			}

			Movement(Position(0, Input.GetAxis("Vertical") * moveSpeed));
		}

		#endregion

		#region Actions

		protected void Rotation(float amt)
		{
			angle += amt * Mathf.Deg2Rad * Time.deltaTime;
		}

		protected Vector3 Position(float x, float y)
		{
			Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
			
			Vector2 perpendicular = new Vector3(Mathf.Cos(angle - 90), 0, Mathf.Sin(angle - 90));
			Quaternion vRot = Quaternion.AngleAxis(-y, perpendicular);
			Quaternion hRot = Quaternion.AngleAxis(x, direction);
			rotation *= hRot * vRot;

			return direction;
		}

		protected void Movement(Vector3 direction)
		{
			trans.localPosition = rotation * Vector3.up * GetHeight();
				//Vector3.Lerp(trans.localPosition, , Time.fixedDeltaTime * moveDamp);
			trans.rotation = Quaternion.Lerp(trans.rotation, rotation * Quaternion.LookRotation(direction, Vector3.up),
				Time.fixedDeltaTime * rotDamp);
		}

		protected float GetHeight()
		{
			Ray ray = new Ray(trans.position, planet.position - trans.position);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit))
				radius = Vector3.Distance(planet.position, hit.point) + height + curJumpHeight;

			return radius;
		}

		#endregion
	}
}
