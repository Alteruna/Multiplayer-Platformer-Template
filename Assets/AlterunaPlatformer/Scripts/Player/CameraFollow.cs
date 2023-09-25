using System;
using UnityEngine;

namespace AlterunaPlatfromer
{
	public class CameraFollow : MonoBehaviour
	{
		public Transform Target;

		public Vector3 Offset = new Vector3(0, 1.5f, -10);

		// store transform in a field for a slight performance increase.
		private Transform _t;

		private void Start()
		{
			_t = transform;
			enabled = Target;
		}

		private void Update()
		{
			_t.position = Target.position + Offset;
		}


		public static void NewTarget(Transform t)
		{
			// get all instances of CameraFollow
			CameraFollow[] cameraFollows = FindObjectsOfType<CameraFollow>();
			foreach (var cameraFollow in cameraFollows)
			{
				// check if same scene
				if (cameraFollow.gameObject.scene == t.gameObject.scene)
				{
					// set target
					CameraFollow targetScript = cameraFollow;
					targetScript.Target = t;
					targetScript.enabled = t;
					return;
				}
			}

			throw new Exception("No CameraFollow found in scene");
		}
	}
}
