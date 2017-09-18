using UnityEngine;

public class Gear : MonoBehaviour
{
	public float turnSpeed = 50f;
	public float ToothCount;

	public Gear[] children;

	private bool parented = false;

	private void Start()
	{
		for (int i = 0; i < children.Length; i++)
			children[i].parented = true;
	}

	private void Update()
	{
		if (parented)
			return;

		float rotation = 0;
		if (Input.GetKey(KeyCode.LeftArrow))
			rotation = turnSpeed * Time.deltaTime;

		if (Input.GetKey(KeyCode.RightArrow))
			rotation = -turnSpeed * Time.deltaTime;

		transform.Rotate(transform.forward, rotation);

		for (int i = 0; i < children.Length; i++)
			children[i].Rotate(-rotation * ToothCount);
	}

	public void Rotate(float rotation)
	{
		rotation /= ToothCount;
		transform.Rotate(transform.forward * rotation);

		for (int i = 0; i < children.Length; i++)
			children[i].Rotate(-rotation * ToothCount);
	}
}

