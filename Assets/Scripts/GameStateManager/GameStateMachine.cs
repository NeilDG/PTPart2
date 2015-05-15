using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manages the game states properly
/// </summary>
public class GameStateMachine : MonoBehaviour {

	private static GameStateMachine sharedInstance = null;

	public static GameStateMachine Instance {
		get {
			return sharedInstance;
		}
	}

	//define custom game states here
	public enum StateType {
		INITIALIZE,
		MAIN_GAME,
		POST_GAME,
		END,
	}

	[SerializeField] private float delayUntilStart = 1.0f;

	private Dictionary<StateType, GameState> stateTable = new Dictionary<StateType, GameState>();
	private GameState currentState;

	void Awake() {
		sharedInstance = this;

		this.InitializeStateMachine ();
	}

	// Use this for initialization
	void Start () {
		this.StartCoroutine (this.DelayStart ());
	}
	
	// Update is called once per frame
	void Update () {
		if (this.currentState != null) {
			this.currentState.OnUpdate();
		}
	}

	void OnDestroy() {
		this.currentState = null;
		this.stateTable.Clear ();
	}

	/// <summary>
	/// Delays the start of the machine, to give chance for other components to properly initialize.
	/// </summary>
	private IEnumerator DelayStart() {
		yield return new WaitForSeconds (this.delayUntilStart);

		//set start state
		this.currentState = this.stateTable [StateType.INITIALIZE];
		this.currentState.OnStart ();

		LoadManager.ReportLoadComplete ();
	}

	private void InitializeStateMachine() {
		//fill up state table
	}

	public void ChangeState(GameStateMachine.StateType newStateType) {
		if (this.currentState != null) {
			this.currentState.OnEnd();
		}

		if (this.stateTable.ContainsKey (newStateType)) {
			this.currentState = this.stateTable [newStateType];
			this.currentState.OnStart();
		} else {
			Debug.LogError(newStateType + " does not exist in state machine. Please make sure you have added it in InitializeStateMachine().");
		}

	}
}
