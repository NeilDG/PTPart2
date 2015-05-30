using UnityEngine;
using System.Collections;

public class GameFlowConstants {

	public const float MINIMUM_PREPARATION_TIME = 5.0f;//30.0f;
	public const float MAXIMUM_PREPARATION_TIME = 7.0f;//60.0f;

	public const float MIN_MONSTER_DELAY_APPEAR = 15.0f;
	public const float MAX_MONSTER_DELAY_APPEAR = 60.0f;

	public const float MIN_TREMOR_DURATION = 7.0f;
	public const float MAX_TREMOR_DURATION = 14.0f;

	public const float LIGHTS_OUT_DELAY = 0.45f;

	public const float FOG_DENSITY = 0.3f;


	public static float RandomizePreparationTime() {
		float value = Random.Range(MINIMUM_PREPARATION_TIME, MAXIMUM_PREPARATION_TIME);

		return value;
	}

	public static float RandomizeMonsterDelay() {
		float value = Random.Range(MIN_MONSTER_DELAY_APPEAR, MAX_MONSTER_DELAY_APPEAR);

		return value;
	}

	public static float RandomizeTrevorDuration() {
		float value = Random.Range(MIN_TREMOR_DURATION, MAX_TREMOR_DURATION);
		
		return value;
	}
}
