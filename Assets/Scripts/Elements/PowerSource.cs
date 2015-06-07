using UnityEngine;
using System.Collections;

/// <summary>
/// Represents a power source
/// </summary>
public class PowerSource : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("Power Collided with: " + other.gameObject.name);
	}
}
