using System.Collections;
using UnityEngine;

#pragma warning disable 649

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

	// Use this for initialization
	private Vector3 velocity = Vector3.zero;

	public float _speed = 5f;
	// Update is called once per frame


	private void FixedUpdate()
	{
		// Early out if we don't have a target
		FollowTarget(target);

	}
	public void FollowTarget(Transform t)
	{
		if (t != null)
		{
			smoothTime = Mathf.Clamp(smoothTime, 0.15f, 1);
			Vector3 localPos = transform.position;
			Vector3 targetLocalPos = t.transform.position;

			float wantedHeight = t.position.y + height - localPos.y;

			float currentHeight = transform.position.y;


			// Damp the height
			currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.fixedDeltaTime);


			transform.position = Vector3.SmoothDamp(localPos, new Vector3(targetLocalPos.x + Mathf.Abs(target.GetComponent<PlayerController>()._angles.y) * 0.1f, currentHeight, targetLocalPos.z - distance + Mathf.Abs(target.GetComponent<PlayerController>()._angles.y * 0.1f)), ref velocity, smoothTime);


			Vector3 dir = -transform.position + direction;
			Quaternion toRotation = Quaternion.FromToRotation(transform.forward, dir);
			transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, _speed * Time.fixedDeltaTime);
		}

	}
	Vector3 direction;
	void DetectAngle(Vector3 point)
	{

		direction = Vector3.one * Vector3.Distance(transform.position, point);

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

	Vector3 colPoint;

	private void OnTriggerEnter(Collider other)
	{
		colPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
		DetectAngle(colPoint);

	}
	private void OnTriggerExit(Collider other)
	{
		colPoint = Vector3.zero;
	}
}
