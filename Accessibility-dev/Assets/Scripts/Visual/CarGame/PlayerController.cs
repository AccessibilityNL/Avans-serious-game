using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /* GameController */
    [SerializeField] private GameController gameController;
    private int points = 20;

    /* Player */
    Rigidbody carRigidBody;
    private int health;
    [SerializeField] private GameObject[] healthIcons;

    /* Movement */
    [SerializeField] private float forwardSpeed = 50.0f;
    [SerializeField] private float sidewaysSpeed = 15.0f;
    private int desiredLane = 1;
    private const float LANE_DISTANCE = 3.5f;

    /* Start is called on the very first frame */
    private void Start()
    {
        health = healthIcons.Length;
        carRigidBody = GetComponent<Rigidbody>();
    }

    /* Update is called every consecutive frame */
    private void Update()
    {
        //Gather inputs on which lane the car should be
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            MoveLane(false);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            MoveLane(true);
        }
    }

    /* Update is called every consecutive frame */
    private void FixedUpdate()
    {
        //Calculate which lane the car should be going to
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (desiredLane == 0){
            targetPosition += Vector3.left * LANE_DISTANCE;
        }
        else if (desiredLane == 2){
            targetPosition += Vector3.right * LANE_DISTANCE;
        }

        //Calculate move delta
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).normalized.x * sidewaysSpeed; //Target position - current position
        moveVector.y = -0;
        moveVector.z = forwardSpeed;

        //Move the car
        carRigidBody.MovePosition(transform.position + moveVector * Time.fixedDeltaTime);
    }

    private void MoveLane(bool goingRight)
    {
        desiredLane += (goingRight ? 1 : -1); //Moving right means + 1, moving left means -1
        desiredLane = Mathf.Clamp(desiredLane, 0, 2); //Setting a minimum & maximum lane
    }

    public void ReceiveDamage(int damage)
    {
        if (health > 0){
            health -= damage;

            if (points >= 5) 
            {
                points -= 5;
            }
            
            healthIcons[health].SetActive(false);

            if (health <= 0){
                gameController.triggerGameOver();
            }
        }
    }

    public Rigidbody GetRigidbody()
    {
        return carRigidBody;
    }

    public int GetPoints()
    {
        return points;
    }
}