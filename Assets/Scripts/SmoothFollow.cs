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

	private void Start()
	{
		parent = transform.parent;
	}

	public float _maxAngleDelta = 10f;
	private void LateUpdate()
	{
		// Early out if we don't have a target
		FollowTarget(target);
		
		transform.LookAt(target);
		
	}
	public void FollowTarget(Transform t)
	{
		if (t != null)
		{
			Vector3 localPos = parent.localPosition;

			Vector3 targetLocalPos = t.transform.localPosition;

			parent.localPosition = Vector3.Lerp(localPos, new Vector3(targetLocalPos.x , targetLocalPos.y, targetLocalPos.z - distance) - new Vector3(targetLocalPos.x + Mathf.Abs(t.GetComponent<PlayerController>().dir.x),0, 0)*smoothTime,  smoothTime);

			parent.rotation = Quaternion.Lerp(parent.rotation, Quaternion.Euler(target.eulerAngles - new Vector3(0, target.eulerAngles.x , 0)), _speed * Time.fixedDeltaTime);
			
		}

	}



	Transform parent;

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

	
}
