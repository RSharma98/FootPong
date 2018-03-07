using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour {

	//Scene that should be loaded when button is pressed
	[SerializeField] private string sceneToLoad;

	//When the button is pressed execute this code
	public void OnClick(){
		//A console log statement to say the button has been pressed and what scene should be loaded
		Debug.Log("Button has been pressed to load " + sceneToLoad);
		//Load the scene that needs to be loaded
		SceneManager.LoadScene(sceneToLoad);
	}
}
