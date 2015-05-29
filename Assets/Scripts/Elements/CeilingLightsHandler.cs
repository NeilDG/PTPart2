using UnityEngine;
using System.Collections;

public class CeilingLightsHandler : MonoBehaviour {
	private static CeilingLightsHandler sharedInstance = null;

	public static CeilingLightsHandler Instance {
		get {
			return sharedInstance;
		}
	}

	[SerializeField] private AudioSource[] lightsRowList;

	void Awake() {
		sharedInstance = this;
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void InitiateLightsOutEvent() {
		this.StartCoroutine(this.LightsOutCouroutine());
	}

	private IEnumerator LightsOutCouroutine() {
		for(int i = 0; i < this.lightsRowList.Length; i++) {
			this.lightsRowList[i].Play();
			this.DeactivateLights(this.lightsRowList[i].transform);

			yield return new WaitForSeconds(GameFlowConstants.LIGHTS_OUT_DELAY);
		}

		EventBroadcaster.Instance.PostEvent(EventNames.ON_LIGHTS_OUT_EVENT_FINISHED);
	}

	private void DeactivateLights(Transform lightsContainer) {
		foreach(Transform child in lightsContainer) {
			child.gameObject.SetActive(false);
		}
	}
}
