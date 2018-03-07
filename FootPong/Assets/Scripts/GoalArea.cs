using UnityEngine;

public class GoalArea : MonoBehaviour {

	GameObject gm;	//The game master object
	[SerializeField] private int playerNumber;	//Which player will score at this goal

	void Start(){
		//Find the first object in the scene which has the name "GM"
		gm = GameObject.Find("GM");
	}

	//If an object enters the trigger area of the goal execute this code
	void OnTriggerEnter2D(Collider2D col){
		if(col.tag == "Ball"){	//If the colliding object's tag is "Ball"
			GameObject ball = col.gameObject;	//Create a reference to the ball
			//Add a goal for the player who scored to the score manager script
			gm.GetComponent<ScoreManager>().AddGoal(playerNumber);	
			//Reset the ball using the ball manager
			ball.GetComponent<BallManager>().ResetBall();
		}
	}
}
