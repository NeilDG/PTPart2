﻿using UnityEngine;
using System.Collections;

public class CameraShakeEvent : GameEvent {

	[SerializeField] private Camera camera;
	[SerializeField] private float shakeValue = 0;
	[SerializeField] private float shakeAmount = 0.7f;
	[SerializeField] private float decreaseFactor = 1.0f;

	// Use this for initialization
	public override void Start () {
		base.Start ();

		this.gameEventName = GameEventNames.CAMERA_SHAKE_EVENT_NAME;
	}

	
	// Update is called once per frame
	void Update () {
		if (shakeValue > 0.0f) {
			this.camera.transform.localPosition = Random.insideUnitSphere * shakeAmount;
			shakeValue -= Time.deltaTime * decreaseFactor;
			
		} else {
			shakeValue = 0.0f;
			this.gameObject.SetActive(false);
		}	
	}
}