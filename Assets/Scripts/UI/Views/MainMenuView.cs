using UnityEngine;
using System.Collections;

/// <summary>
/// Represents the main menu view
/// </summary>
public class MainMenuView : View {

	// Use this for initialization
	void Start () {
	
	}

	public void OnStartClicked() {
		Screen.showCursor = false;
		Application.LoadLevel(SceneNames.IN_GAME_SCENE);
	}

	public void OnQuitClicked(){
		Application.Quit();
	}
}
