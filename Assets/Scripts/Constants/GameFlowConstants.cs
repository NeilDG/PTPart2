using UnityEngine;
using System.Collections;

public class GameFlowConstants {

	public const float MINIMUM_PREPARATION_TIME = 5.0f;
	public const float MAXIMUM_PREPARATION_TIME = 10.0f;

	public const float MIN_MONSTER_DELAY_APPEAR = 15.0f;
	public const float MAX_MONSTER_DELAY_APPEAR = 60.0f;

	public const float LIGHTS_OUT_DELAY = 0.45f;

	public static float RandomizePreparationTime() {
		float value = Random.Range(MINIMUM_PREPARATION_TIME, MAXIMUM_PREPARATION_TIME);

		return value;
	}

	public static float RandomizeMonsterDelay() {
		float value = Random.Range(MIN_MONSTER_DELAY_APPEAR, MAX_MONSTER_DELAY_APPEAR);

		return value;
	}
}
