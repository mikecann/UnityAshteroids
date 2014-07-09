using UnityEngine;
using System.Collections;

public static class ExtensionMethods 
{
#if UNITY_4_3
	public static void AddRelativeForce(this Rigidbody2D body, Vector2 force)
	{
		body.AddForce(body.transform.TransformDirection(force));
	}
#endif
}
