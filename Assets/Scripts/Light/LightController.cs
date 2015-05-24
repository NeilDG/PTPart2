using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour 
{
	[System.Serializable]
	private struct LightRange
	{
		public float updateAmount;
		public float minValue;
		public float maxValue;
	}

	[System.Serializable]
	private struct LightIntensity
	{
	    public float updateAmount;
		public float minValue;
		public float maxValue;
	}

	private Light lightSource;

	[SerializeField] private CharacterController charControl;
	[SerializeField] private LightRange lightRange;
	[SerializeField] private LightIntensity lightIntensity;

	// Use this for initialization
	void Start () {
		this.lightSource = this.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		bool isMoving = directionVector != Vector3.zero;
		this.UpdateLightSourceRange(lightRange.updateAmount, isMoving);
		this.UpdateLightIntensity(lightIntensity.updateAmount, isMoving);
	}

	private void UpdateLightSourceRange(float amount, bool decrease = false)
	{
		amount = decrease == true ? amount * -1 : amount;
		this.lightSource.range += amount;
		this.lightSource.range = Mathf.Clamp(lightSource.range, 
		                                     lightRange.minValue, 
		                                     lightRange.maxValue);
	}

	private void UpdateLightIntensity(float amount, bool decrease = false)
	{
		amount = decrease == true ? amount * -1 : amount;
		this.lightSource.intensity += amount;
		this.lightSource.intensity = Mathf.Clamp(lightSource.intensity, 
		                                         lightIntensity.minValue, 
		                                         lightIntensity.maxValue);
	}
}
