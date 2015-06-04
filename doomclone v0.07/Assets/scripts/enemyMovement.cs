using UnityEngine;
using System.Collections;

public class enemyMovement : MonoBehaviour {

	public float speed = 1;
	public int howLongStrafe=2000;
	public int moveOffset = 100; //delay between switching FB and RL movements
	public float gravityAmount = 5;
	
	public int chanceToPause = 50; //in percent (/100)

	public bool isMelee = false;
	public bool strafes = true;
	public int awake = 0;

	private int toggleDirCounter = 0; //counter for switching directions counts from 0 to howLongStrafe
	private int toggleDirCounterFB = 0;
	private int pause = 1;




	private Vector3 moveDirection; //make random
	private int moveRL;//for switching strafe directions

	private int moveFB;



	// Use this for initialization
	void Start () 
	{

		moveRL = 1;
		moveFB = 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
		CharacterController controller = GetComponent<CharacterController>();

		if (!isMelee) 
		{
			toggleDirCounter++;
			toggleDirCounterFB++;

			if (toggleDirCounter > howLongStrafe) {
				toggleDirCounter = 0;
				moveRL = -moveRL;


				if (Random.Range (0, 100) < chanceToPause) {
					pause = 0;
				}

			}
			if (toggleDirCounterFB > howLongStrafe - (moveOffset)) 
			{
				toggleDirCounterFB = 0;
				moveFB = -moveFB;

				pause = 1;
				moveRL = -moveRL;
			}

			if (controller.isGrounded) 
			{
				moveDirection = Vector3.right*moveRL;
				
				
				moveDirection.z = moveFB;
				
				
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= speed*pause*awake;	
				
				
				
			}
			moveDirection.y -= gravityAmount * Time.deltaTime;
			controller.Move(moveDirection * Time.deltaTime);


		}
		if (isMelee) 
		{
			toggleDirCounter++;
			if (toggleDirCounter > howLongStrafe) 
			{
				toggleDirCounter = 0;
				moveRL = -moveRL;
			}

			if (!strafes)
				moveRL = 0;

			if (controller.isGrounded) 
			{
				moveDirection = Vector3.right *moveRL;
				moveDirection.z = -1;
				moveDirection = transform.TransformDirection(moveDirection);

				moveDirection *= speed *awake;	
			}
			moveDirection.y -= gravityAmount * Time.deltaTime;
			controller.Move(moveDirection * Time.deltaTime);
		}











	}
	void setAwake(bool awakeIn)
	{
		if (awakeIn)
			awake = 1;
		else 
			awake = 0;
	}
}
