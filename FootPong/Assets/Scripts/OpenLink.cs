using UnityEngine;

public class OpenLink : MonoBehaviour {

	//The URL that should be opened when the button is clicked
	[SerializeField] private string targetURL;

	//When the button is pressed, execute this code
	public void OnClick(){
		//A console log statement to say that the button has been pressed and what URL should open
		Debug.Log("Button has been pressed to open the following URL: " + targetURL);
		//Open the URL (should open in new tab in default browser)
		Application.OpenURL(targetURL);
	}
}
