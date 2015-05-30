using UnityEngine;
using System.Collections;

/// <summary>
/// Responsible for setting the feel of the game. 
/// </summary>
public class AtmosphereHandler : MonoBehaviour {
	private static AtmosphereHandler sharedInstance = null;
	public static AtmosphereHandler Instance {
		get {
			return sharedInstance;
		}
	}

	[SerializeField] private AudioClip[] ambientSFXList;
	[SerializeField] private AudioClip[] bgmList;

	[SerializeField] private AudioSource ambientSource;
	[SerializeField] private AudioSource bgmSource;

	public enum BGMState {
		FADE_IN,
		PLAYING,
		FADE_OUT,
		NONE
	}

	private BGMState bgmState = BGMState.NONE;
	private float bgmMeasureTime = 0.0f;
	private float startTime = 0.0f;

	private const int MIN_AMBIENCE_DELAY = 4;
	private const int MAX_AMBIENCE_DELAY = 10;

	private const float BGM_FADEIN_TIME = 2.0f;
	private const float BGM_FADEOUT_TIME = 8.0f;

	private const float AMBIENT_START_DELAY = 20.0f;

	private const float AMBIENT_PLAY_SOUND_VOLUME = 1.0f;
	private const float AMBIENT_FEEL_SOUND_VOLUME = 0.4f;

	void Awake () {
		sharedInstance = this;
	}

	// Use this for initialization
	void Start () {
		this.startTime = Time.time;

		EventBroadcaster.Instance.AddObserver(EventNames.ON_MAIN_EVENT_GAME_STARTED, this.CreateAmbientFeel);
	}

	void OnDestroy() {
		EventBroadcaster.Instance.RemoveActionAtObserver(EventNames.ON_MAIN_EVENT_GAME_STARTED, this.CreateAmbientFeel);
	}

	void Update() {
		switch (this.bgmState) {
		case BGMState.FADE_IN:
			this.bgmMeasureTime = Time.time - this.startTime;
			float volume = this.bgmMeasureTime / BGM_FADEIN_TIME;
			this.bgmSource.volume = volume;

			if(this.bgmSource.volume >= 1.0f) {
				this.bgmMeasureTime = 0.0f;
				this.startTime = Time.time;
				this.bgmState = BGMState.PLAYING;
			}

			break;
		case BGMState.PLAYING:
			this.bgmMeasureTime = Time.time - this.startTime;

			//Debug.Log ("Measure time: " +this.bgmMeasureTime+ " Clip length: " +this.bgmSource.clip.length);
			if(this.bgmMeasureTime >= this.bgmSource.clip.length - BGM_FADEOUT_TIME) {
				this.startTime = Time.time;
				this.bgmMeasureTime = 0.0f;
				this.bgmState = BGMState.FADE_OUT;
			}

			break;
		case BGMState.FADE_OUT:

			this.bgmMeasureTime = Time.time - this.startTime;
			volume = 1.0f - (this.bgmMeasureTime / BGM_FADEOUT_TIME);
			this.bgmSource.volume = volume;
			
			if(this.bgmSource.volume <= 0.0f) {
				this.bgmMeasureTime = 0.0f;
				this.startTime = 0.0f;
				this.bgmState = BGMState.NONE;
			}
			break;
		case BGMState.NONE:
			this.CreateBGM();
			this.bgmState = BGMState.FADE_IN;
			break;
		}

		//Debug.Log ("BGM Volume: " + this.bgmSource.volume);
	}

	private void CreateAmbientFeel() {
		this.ambientSource.volume = AMBIENT_FEEL_SOUND_VOLUME;
		this.StartCoroutine (this.DelayAmbientFeel ());
	}

	private void CreateBGM() {
		int randomIndex = Random.Range (0, this.bgmList.Length);

		this.bgmSource.clip = this.bgmList [randomIndex];
		this.bgmSource.Play ();

		this.bgmSource.volume = 0.0f;
	}

	private IEnumerator DelayAmbientFeel() {
		yield return new WaitForSeconds (AMBIENT_START_DELAY);
		this.StartCoroutine (this.PlayRandomAmbienceAndWait ());
	}

	private IEnumerator PlayRandomAmbienceAndWait() {
		int randomIndex = Random.Range (0, this.ambientSFXList.Length);

		int randomDelay = Random.Range (MIN_AMBIENCE_DELAY, MAX_AMBIENCE_DELAY);
		yield return new WaitForSeconds(randomDelay);

		this.ambientSource.clip = ambientSFXList [randomIndex];
		this.ambientSource.Play ();

		yield return new WaitForSeconds(this.ambientSource.clip.length);

		this.CreateAmbientFeel ();
	}

	public void PlayAmbientEventSound(AudioClip audioClip) {
		this.ambientSource.volume = AMBIENT_PLAY_SOUND_VOLUME;
		this.ambientSource.clip = audioClip;
		this.ambientSource.Play();
	}
	
}
