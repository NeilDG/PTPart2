using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum ObjectBehaveType
{
	AddRigidBody,
	MakeSound,
	PlayAnimation
}

public class ObjectManager : MonoBehaviour 
{
	private static ObjectManager instance;

	public static ObjectManager Instance
	{
		get 
		{
			if(instance == null)
			{
				GameObject g = new GameObject();
				instance = g.AddComponent<ObjectManager>();
			}
			return instance;
		}
	}

	private Dictionary<string, GameObject> objectPool;

	void Awake()
	{
		instance = this;
		this.objectPool = new Dictionary<string, GameObject>();
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.F5))
		{
			this.SetObjectBehaviour(ObjectConstants.STEEL_FALLING_NEAR_DOOR, ObjectBehaveType.AddRigidBody);
		}
	}

	public void AddObjectToPool(string objectKey, GameObject obs)
	{
		if(!this.objectPool.ContainsKey(objectKey))
		{
			this.objectPool.Add(objectKey, obs);
		}
		else
		{
			Debug.LogError("This object is already added!");
		}
	}

	public void RemoveObjectFromPool(string objectKey)
	{
		if(this.objectPool.ContainsKey(objectKey))
		{
			this.objectPool.Remove(objectKey);
		}
		else
		{
			Debug.LogError("Objectkey:"+ objectKey +" not found!");
		}
	}

	public void SetObjectBehaviour(string objectKey, ObjectBehaveType type, float delay = 0)
	{
		this.StartCoroutine(this.PlayObjectBehaviour(objectKey, type, delay));
	}

	private IEnumerator PlayObjectBehaviour(string objectKey, ObjectBehaveType type, float delay = 0)
	{
		yield return new WaitForSeconds(delay);

		if(this.objectPool.ContainsKey(objectKey))
		{
			this.DoObjectBehaviour(this.objectPool[objectKey], type);
		}
		else
		{
			Debug.LogError("Objectkey:"+ objectKey +" not found!");
		}
	}

	private void DoObjectBehaviour(GameObject targetObject, ObjectBehaveType type)
	{
		switch(type)
		{
		case ObjectBehaveType.AddRigidBody:
			targetObject.AddComponent<Rigidbody>();
			ConstantForce cf = targetObject.AddComponent<ConstantForce>();
			cf.constantForce.force = new Vector3(0,-50,0);
			break;
		case ObjectBehaveType.MakeSound:
			//Sounds
			break;
		case ObjectBehaveType.PlayAnimation:
			//Play animation
			break;
		}
	}
}
