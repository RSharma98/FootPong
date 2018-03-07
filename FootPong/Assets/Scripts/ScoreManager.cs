using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

	private int p1Score, p2Score;	//Variables for the score of both players
	public Text p1ScoreText, p2ScoreText;	//The UI text to show each player's score
	public Text playerScoredText;	//The text to show when a player scores

	void Start () {
		//Set the scores of both players to be zero
		p1Score = 0;
		p2Score = 0;
		playerScoredText.enabled = false;	//Disable the text that should appear when the player scores
	}
	
	void Update () {
		//Set the score text equal to the score
		p1ScoreText.text = p1Score.ToString();
		p2ScoreText.text = p2Score.ToString();
	}

	public void AddGoal(int player) {
		switch (player){
			case 1: 
				p1Score++;	//Increment player one's score
				//Start the coroutine to set the text values and enable the text
				StartCoroutine(ShowPlayerScoredText("RED TEAM SCORED!", Color.red));
				break;
			case 2:
				p2Score++;	//Increment player two's score
				//Start the coroutine to set the text values and enable the text
				StartCoroutine(ShowPlayerScoredText("BLUE TEAM SCORED!", Color.blue));
				break;
			default:
				//If a value other than 1 or 2 is used, show an error message
				Debug.LogAssertion("CANNOT ADD GOAL. INCORRECT PLAYER NUMBER USED");
				break;
		}
	}

	IEnumerator ShowPlayerScoredText(string outputText, Color textColour){
		playerScoredText.text = outputText;		//Set the text to show to be shown when the player scores
		playerScoredText.color = textColour;	//Set the colour of the text
		Debug.Log(outputText);					//Output the text to the console
		playerScoredText.enabled = true;		//Enable the text box
		yield return new WaitForSeconds (1f);	//Wait for one second
		playerScoredText.enabled = false;		//Disable the text box
	}

	//Public ints to access player score from other scripts
	public int GetPlayerOneScore(){
		return p1Score;
	}

	public int GetPlayerTwoScore(){
		return p2Score;
	}
}
