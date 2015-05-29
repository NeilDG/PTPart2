using UnityEngine;
using System.Collections;

/// <summary>
/// Handles playing of footsteps
/// </summary>
public class FootstepPlayer : MonoBehaviour {
	[SerializeField] private AudioSource[] audioSteps;

	private const float FOOTSTEP_PLAY_DELAY = 0.5f;
	private float nextPlayTime = 0.0f;
	private float currentPlayTime = 0.0f;

	// Use this for initialization
	void Start () {
		this.nextPlayTime += FOOTSTEP_PLAY_DELAY;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		
		if (directionVector != Vector3.zero) {
			this.PlayFootstep();
		}
		else {
			this.currentPlayTime = 0.0f;
		}
	}

	private void PlayFootstep() {
		this.currentPlayTime += Time.deltaTime;

		if(this.currentPlayTime >= FOOTSTEP_PLAY_DELAY) {
			this.currentPlayTime = 0.0f;

			this.audioSteps[Random.Range(0, this.audioSteps.Length)].Play();

		}
	}
}
