using UnityEngine;
using System.Collections;

public class ObjectController : MonoBehaviour 
{
	[SerializeField] private GameObject targetObject;
	[SerializeField] private string targetObjectKey;
	[SerializeField] private ObjectBehaveType behaveType;

	// Use this for initialization
	void Start () {
		ObjectManager.Instance.AddObjectToPool(ObjectConstants.STEEL_FALLING_NEAR_DOOR, targetObject);
	}

	void OnTriggerEnter(Collider c)
	{
		if(c.tag == "Player")
		{
			ObjectManager.Instance.SetObjectBehaviour(this.targetObjectKey, this.behaveType);
			Debug.Log("Collided with:"+ this.gameObject.name);
		}
	}
}
