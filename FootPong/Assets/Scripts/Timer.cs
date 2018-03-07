using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

	ScoreManager scoreManager;

	[SerializeField] private string mainMenuScene;	//The name of the main menu scene (this will be loaded when the timer runs out)
	[SerializeField] private int minutes;			//How many minutes should be set in the timer
	[SerializeField] private float seconds;			//How many seconds should be set in the timer
	private bool timerIsOn;							//Is the timer on? (i.e. should it count down?)

	public Text countdownText;		//The textbox for the countdown timer
	public Text timer;				//The textbox to display the timer

	void Start () {
		scoreManager = GetComponent<ScoreManager>();
	}
	
	void Update () {
		//If the timer is on, decrement the timer
		if (timerIsOn) seconds -= Time.deltaTime;

		if(minutes <= 0 &&  seconds <= 0){
			//If the timer has run out, start the coroutine to end the game
			StartCoroutine(EndGame());
		} else{
			//If the seconds are less than zero, decrement the minutes remaining and set the seconds equal to 59
			if(seconds < 0){
				minutes--;
				seconds = 59;
			}
		}

		//Set the minutes and seconds text to be shown as a 2 digit string at all times and with zero decimal points
		string minutesText = minutes < 10 ? "0" + minutes : minutes.ToString();	
		string secondsText = Mathf.Ceil(seconds) < 10 ? "0" + Mathf.Ceil(seconds).ToString() : Mathf.Ceil(seconds).ToString("F0");
		timer.text = minutesText + ":" + secondsText;	//Set the timer text to show the minutes and seconds in the correct format
	}

	//Coroutine to show the countdown timer (timer that shows before the ball is launched)
	public IEnumerator CountdownTimer(float time){
		SetTimerIsOn(false);	//Disable the timer
		countdownText.enabled = true;	//Enable the countdown text
		while (time > 0){	//A while loop
			countdownText.text = Mathf.Ceil(time).ToString("F0");	//Show the current value of time (rounded up) in the countdown text box
			time -= Time.deltaTime;		//Decrement the time
			yield return null;
		}
		SetTimerIsOn(true);	//Reactivate the timer
		countdownText.text = "GO!";	//Set the countdown timer text to say "GO!"
		yield return new WaitForSeconds(0.5f);	//Wait for 0.5 seconds
		countdownText.enabled = false;			//Disable the text box
	}

	//Coroutine to end the current game
	public IEnumerator EndGame(){
		SetTimerIsOn(false);	//Disable the timer
		GameObject ball = GameObject.FindGameObjectWithTag("Ball");		//Create a reference to the ball
		ball.GetComponent<BallManager>().SetVelocity(Vector2.zero);		//Set the ball's velocity to zero
		//Get the scores for both players and store them in local variables
		int p1Score = scoreManager.GetPlayerOneScore();
		int p2Score = scoreManager.GetPlayerTwoScore();
		string outputText;	//Text that should be displayed on game over
		Color textColor;	//Colour of the text
		if(p1Score == p2Score){	//If both scores are equal (i.e. a tie)
			//Set the output text to say that the game had tied and set the text colour to white
			outputText = "IT'S A TIE!";
			textColor = Color.white;
		} else {
			//Set the output text and text colour depending on which player has the highest score
			outputText = p1Score > p2Score ? "RED TEAM WINS!" : "BLUE TEAM WINS!";
			textColor = p1Score > p2Score ? Color.red : Color.blue;
		}
		//Assign the output text and text colour to the text box, then enable the text box
		countdownText.text = outputText;
		countdownText.color = textColor;
		countdownText.enabled = true;
		yield return new WaitForSeconds (2f);	//Wait for two seconds
		SceneManager.LoadScene(mainMenuScene);	//Load the main menu
	}

	//Public function to allow other scripts to set whether the timer is on
	public void SetTimerIsOn(bool newTimerState){
		timerIsOn = newTimerState;
	}

	//Public bool to allow other scripts to see whether the timer is on
	public bool GetTimerIsOn(){
		return timerIsOn;
	}
}
