using UnityEngine;
using System.Collections;

public class ObjectController : MonoBehaviour 
{
	[SerializeField] private GameObject targetObject;

	// Use this for initialization
	void Start () {
		ObjectManager.Instance.AddObjectToPool(ObjectConstants.STEEL_FALLING_NEAR_DOOR, targetObject);
	}

	void OnTriggerEnter(Collider c)
	{
		if(c.tag == "Player")
		{
			ObjectManager.Instance.SetObjectBehaviour(ObjectConstants.STEEL_FALLING_NEAR_DOOR, ObjectBehaveType.AddRigidBody);
			Debug.Log("Collided with:"+ this.gameObject.name);
		}
	}
}
