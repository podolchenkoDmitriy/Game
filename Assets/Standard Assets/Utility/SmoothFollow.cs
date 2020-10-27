using System.Collections;
using UnityEngine;

#pragma warning disable 649
namespace UnityStandardAssets.Utility
{
	public class SmoothFollow : MonoBehaviour
	{

		// The target we are following
		[SerializeField]
		private Transform target;
		// The distance in the x-z plane to the target
		[SerializeField]
		private float distance = 10.0f;
		// the height we want the camera to be above the target
		public float height = 5.0f;

		[SerializeField]
		private float heightDamping;
		public float smoothTime = 0.8f;
		private bool canIncrease = false;

		// Use this for initialization
		private Vector3 velocity = Vector3.zero;
		public Vector2 limits;

		// Update is called once per frame
		private void FixedUpdate()
		{
			// Early out if we don't have a target
			Vector3 localPos = transform.localPosition;
			transform.localPosition = new Vector3(Mathf.Clamp(localPos.x, -limits.x, limits.x), Mathf.Clamp(localPos.y, -limits.y, limits.y), localPos.z);
			FollowTarget(target);

		}
		public void FollowTarget(Transform t)
		{
			if (t != null)
			{
				smoothTime = Mathf.Clamp(smoothTime, 0.15f, 1);
				Vector3 localPos = transform.localPosition;
				Vector3 targetLocalPos = t.transform.localPosition;

				float wantedHeight = target.position.y + height - localPos.y;

				float currentHeight = transform.position.y;


				// Damp the height
				currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.fixedDeltaTime);


				transform.localPosition = Vector3.SmoothDamp(localPos, new Vector3(targetLocalPos.x, currentHeight, targetLocalPos.z - distance), ref velocity, smoothTime);

			}

		}


		public void Shake(float duration, float magnitude)
		{
			StartCoroutine(ShakeRoutine(duration, magnitude));
		}

		private IEnumerator ShakeRoutine(float duration, float magnitude)
		{
			Vector3 origPos = transform.localPosition;
			float elapsed = 0.0f;
			while (elapsed < duration)
			{
				float x = Random.Range(-1f, 1f) * magnitude;
				float y = Random.Range(-1f, 1f) * magnitude;
				transform.localPosition += new Vector3(x, y, 0);
				elapsed += Time.fixedDeltaTime;
				yield return null;
			}
			transform.localPosition = origPos;
		}



		private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawLine(new Vector3(-limits.x, -limits.y, transform.position.z), new Vector3(limits.x, -limits.y, transform.position.z));
			Gizmos.DrawLine(new Vector3(-limits.x, limits.y, transform.position.z), new Vector3(limits.x, limits.y, transform.position.z));
			Gizmos.DrawLine(new Vector3(-limits.x, -limits.y, transform.position.z), new Vector3(-limits.x, limits.y, transform.position.z));
			Gizmos.DrawLine(new Vector3(limits.x, -limits.y, transform.position.z), new Vector3(limits.x, limits.y, transform.position.z));
		}
	}
}