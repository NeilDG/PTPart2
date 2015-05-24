using UnityEngine;
using System.Collections;

/// <summary>
/// Handles enemy animation as well as sounds.
/// </summary>
public class EnemyAnimation : MonoBehaviour {

	[SerializeField] private string[] walkClipNames;
	[SerializeField] private string[] runClipNames;
	[SerializeField] private string[] attackClipNames;
	[SerializeField] private string[] idleClipNames;

	[SerializeField] private Animation animComponent;

	private EnemyAI.EnemyActionType enemyActionType;

	private bool attacking = false;

	// Use this for initialization
	void Start () {
	
	}

	public void SetAnimationFromType(EnemyAI.EnemyActionType actionType) {
		if (this.enemyActionType == actionType || this.attacking == true) {
			return;
		}

		Debug.Log ("New action type: " + actionType);
		this.enemyActionType = actionType;

		switch (this.enemyActionType) {
		case EnemyAI.EnemyActionType.IDLE:
			this.PlayRandomAnimation(this.idleClipNames);
			break;
		case EnemyAI.EnemyActionType.PATROLLING:
			this.PlayRandomAnimation(this.walkClipNames);
			break;
		case EnemyAI.EnemyActionType.CHASING:
			this.PlayRandomAnimation(this.runClipNames);
			break;
		case EnemyAI.EnemyActionType.ATTACKING:
			break;
		}
	}

	private void PlayRandomAnimation(string[] clipList) {
		int randomIndex = Random.Range (0, clipList.Length);
		this.animComponent.Play (clipList [randomIndex]);
	}

	/// <summary>
	/// Plays the attack animation. Can be called on an update function
	/// </summary>
	public void PlayAttackAnim() {
		if (this.attacking == false) {
			this.attacking = true;
			this.enemyActionType = EnemyAI.EnemyActionType.ATTACKING;
			int randomIndex = Random.Range (0, this.attackClipNames.Length);
			this.animComponent.Play(this.attackClipNames[randomIndex]);
			this.StartCoroutine(this.HandleAttackAnim(this.attackClipNames[randomIndex]));
		}
	}

	private IEnumerator HandleAttackAnim(string attackClipName) {
		//Debug.LogWarning ("attack length: " + this.animComponent.GetClip (attackClipName).length);
		yield return new WaitForSeconds(this.animComponent.GetClip(attackClipName).length);
		this.attacking = false;
	}

	public bool IsAttacking() {
		return this.attacking;
	}

}
