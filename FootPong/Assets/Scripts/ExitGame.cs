using UnityEngine;

public class ExitGame : MonoBehaviour {

	//Execute this code when the button is pressed
	public void OnClick(){
		Debug.Log("Quit button has been pressed.");	//Output a statement saying that the button to quit the game has been pressed
		Application.Quit();	//Quit the game
	}
}
