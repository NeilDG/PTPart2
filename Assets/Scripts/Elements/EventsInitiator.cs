using UnityEngine;
using System.Collections;

public class EventsInitiator : MonoBehaviour {
	private static EventsInitiator sharedInstance = null;

	public static EventsInitiator Instance {
		get {
			return sharedInstance;
		}
	}


	[SerializeField] private AudioClip[] eventShakeClip;

	void Awake() {
		sharedInstance = this;

		RenderSettings.fogDensity = 0.0f; //let ceiling lights change the fog density
	}

	public void InitiateTremorEvent() {
		int randomIndex = Random.Range(0 , this.eventShakeClip.Length);

		AtmosphereHandler.Instance.PlayAmbientEventSound(this.eventShakeClip[randomIndex]);
	}
}
