using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundInstanceManager : MonoBehaviour {
	
	public void Play(AudioClip audioClip, bool loop, float delay, float pitch)
	{
		if (audioClip == null)
		{
			SoundManager.Instance.StoreAudioObject(gameObject);
			return;
		}
		if (audio.ignoreListenerVolume && audio.clip == audioClip) return; // do nothing

		audio.playOnAwake = false;
		audio.pitch = pitch;
		audio.clip = audioClip;
		audio.loop = loop;
		audio.PlayDelayed(delay);
		if (delay >= 0 && !loop) StartCoroutine(StoreDelay((delay + audioClip.length) * 2.0f));
		else if (loop) DontDestroyOnLoad(gameObject);
	}

	public void Stop() {
		this.audio.Stop ();
		if (audio.loop) {
			Destroy(this.gameObject);
		}
	}

	IEnumerator StoreDelay(float duration)
	{
		yield return new WaitForSeconds(duration);
		SoundManager.Instance.StoreAudioObject(gameObject);
	}
}
