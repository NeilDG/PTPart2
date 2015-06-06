using UnityEngine;
using System.Collections;

public class ObjectController : MonoBehaviour 
{
	[SerializeField] private GameObject targetObject;
	[SerializeField] private string targetObjectKey;
	[SerializeField] private ObjectBehaveType behaveType;
	[SerializeField] private float triggerDelay = 0;

	// Use this for initialization
	void Start () {
		ObjectManager.Instance.AddObjectToPool(this.targetObjectKey, targetObject);
	}

	void OnTriggerEnter(Collider c)
	{
		if(c.tag == "Player")
		{
			ObjectManager.Instance.SetObjectBehaviour(this.targetObjectKey, this.behaveType, this.triggerDelay);
			Debug.Log("Collided with:"+ this.gameObject.name);
		}
	}
}
