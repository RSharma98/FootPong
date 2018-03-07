using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BallManager : MonoBehaviour {

	[SerializeField] private Vector2 launchSpeed;	//The launch speed of the ball
	public Text countdownText;	//The countdown text box

	private GameObject gm;		//The game master object
	private Timer timer;		//The timer component (on the game master object)
	private Vector2 vel; 		//The velocity of the ball

	void Start () {
		gm = GameObject.Find("GM");	//Find the GM game object
		timer = gm.GetComponent<Timer>();	//Get the timer component on the GM object
		ResetBall();	//Call the function to reset the ball
	}

	void Update(){
		vel = GetComponent<Rigidbody2D>().velocity;	//Assign the value of the velocity to the velocity variable
		if(vel != Vector2.zero){	//If the velocity is not zero
			//If the velocity of the ball is less than the launch speed on any axis, set the velocity to be equal to the launch speed (this prevents the ball from slowing down)
			if((vel.x < launchSpeed.x && vel.x > 0) || (vel.x > -launchSpeed.x && vel.x < 0)){
				int multiplier = vel.x >= 0 ? 1 : -1;
				vel.x = launchSpeed.x * multiplier;
			} 
			if((vel.y < launchSpeed.y && vel.y > 0) || (vel.y > -launchSpeed.y && vel.y < 0)){
				int multiplier = vel.y >= 0 ? 1 : -1;
				vel.y = launchSpeed.y * multiplier;
			} 
		}
		GetComponent<Rigidbody2D>().velocity = vel;	//Set the velocity of the ball equal to the value of vel
	}
	
	//Function to launch the ball
	IEnumerator LaunchBall(float waitTime){
		yield return new WaitForSeconds(waitTime);
		Vector2 launchDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));	//Create a Vector2 to randomise the direction the ball launches in
		vel = new Vector2(launchSpeed.x * launchDir.x, launchSpeed.y * launchDir.y);	//Set the velocity equal to the launch speed multiplied by the launch direction
		GetComponent<Rigidbody2D>().velocity = vel;		//Set the velocity 
	}

	//Function to stop the ball
	public void StopBall(){
		vel = Vector2.zero;		//Set the velocity of the ball equal to zero
		GetComponent<Rigidbody2D>().velocity = vel;		//Set the velocity
	}
	
	//Function to reset the ball
	public void ResetBall(){
		StopBall();		//Call the function to stop the ball
		transform.position = Vector2.zero;	//Set the position to be equal to zero (the centre of the pitch)
		StartCoroutine(timer.CountdownTimer(3));	//Start the countdown timer
		StartCoroutine(LaunchBall(3f));				//Start the coroutine to launch the ball
	}

	//Public function to allow other scripts to set the velocity
	public void SetVelocity(Vector2 newVel){
		GetComponent<Rigidbody2D>().velocity = newVel;
	}

	//Public Vector2 to allow other scripts to get ball's velocity
	public Vector2 GetVelocity(){
		return vel; 
	}
}
