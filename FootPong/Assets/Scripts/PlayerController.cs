using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float moveSpeed;		//The move speed of the player paddle
	[SerializeField] private string verticalInput;	//The input axis for vertical movement of the player paddle

	private Vector2 pos, startPos;	//The current position (pos) and starting position (startPos) of the player paddle
	private Vector2 vel;			//Velocity of the player paddle

	void Start(){
		startPos = transform.position;	//Set the starting position equal to the position of the player paddle when the scene loads
	}
	
	void Update () {
		pos.x = startPos.x;				//Set the current x position equal to the starting x position
		pos.y = transform.position.y;	//Set the current y position equal to the current y position (i.e. do not change it)
		transform.position = pos;		//Assign the value of pos to the actual position

		vel.y = moveSpeed * Input.GetAxisRaw(verticalInput);	//Set the y velocity equal to the move speed multiplied by the vertical input
		GetComponent<Rigidbody2D>().velocity = vel;				//Assign the value of vel to the actual velocity
	}

	void OnCollisionEnter2D(Collision2D col){
		//If the ball collides with the player paddle
		if(col.collider.gameObject.tag == "Ball"){
			if(vel.y != 0){	//If the player's velocity is not zero
				GameObject ball = col.collider.gameObject;	//Create a reference to the ball
				BallManager ballManager = ball.GetComponent<BallManager>();	//Create a reference to the ball manager component of the ball
				Vector2 ballVel = ballManager.GetVelocity();	//Get the ball's velocity
				if(vel.y > 0){
					//If the player's velocity is greater than zero, set the ball's y velocity to it's absolute (positive) value
					ballManager.SetVelocity(new Vector2(ballVel.x, Mathf.Abs(ballVel.y)));
				} else{
					//If the player's velocity is less than zero, set the ball's y velocity to it's negative value
					ballManager.SetVelocity(new Vector2(ballVel.x, Mathf.Abs(ballVel.y) * -1f));
				}
			}
		}
	}
}
