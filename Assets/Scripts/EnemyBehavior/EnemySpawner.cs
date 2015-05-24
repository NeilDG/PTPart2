using UnityEngine;
using System.Collections;

/// <summary>
/// Spawns the enemy after some time.
/// </summary>
public class EnemySpawner : MonoBehaviour {

	[SerializeField] private AudioClip[] spawnSoundList;
	[SerializeField] private AudioSource monsterSpawnSource;
	[SerializeField] private GameObject enemyPrefab;

	private const float DELAY_BEFORE_SPAWN = 20.0f;
	private const float Y_OFFSET = 0.09f;

	// Use this for initialization
	void Start () {
		this.StartCoroutine (this.WaitForSpawn ());
	}

	private IEnumerator WaitForSpawn() {
		yield return new WaitForSeconds(DELAY_BEFORE_SPAWN);
		this.SpawnEnemy ();
	}

	private void SpawnEnemy() {
		GameObject spawnedEnemy = GameObject.Instantiate (this.enemyPrefab) as GameObject;

		Vector3 position = EnemyPatrolPointDirectory.Instance.GetRandomPatrolPoint().position;
		position.y = Y_OFFSET;

		spawnedEnemy.transform.position = position;
		spawnedEnemy.transform.parent = this.transform;

		this.monsterSpawnSource.clip = this.spawnSoundList [Random.Range (0, this.spawnSoundList.Length)];
		this.monsterSpawnSource.Play ();

		this.StartCoroutine (this.WaitForSpawn ());
	}
}
