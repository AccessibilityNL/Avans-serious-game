using Assets.Scripts.Overworld;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private Intersection currentIntersection;
    [SerializeField]
    private float movementSpeed = 2.0f;

    private int destinationNode = 0;
    private Transform currentDestination = null;
    private DestinationWithPath destination = null;
    private Transform[] children;
    private SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    void Update()
    {
        if (destination != null)
        {
            if (children == null) {
                children = destination.path.transform.Cast<Transform>().ToArray();
                if (destination.reverseDirection)
                {
                    Array.Reverse(children, 0, children.Length);
                }
                currentDestination = children[destinationNode];
            }
            if(Vector3.Distance(currentDestination.position, transform.position) < 0.01)
            {
                if(destinationNode + 1 < children.Length)
                {
                    destinationNode++;
                    currentDestination = children[destinationNode];
                }
                else if(destinationNode < children.Length)
                {
                    destinationNode++;
                    currentDestination = destination.destination.transform;
                }
                else
                {
                    currentIntersection = destination.destination;
                    currentDestination = null;
                    children = null;
                    destinationNode = 0;
                    destination = null;
                }
            } 
            if (children != null)
            {
                float step = movementSpeed * Time.deltaTime; // calculate distance to move
                transform.position = Vector3.MoveTowards(transform.position, currentDestination.position, step);
            }
        }
        else
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            destination = currentIntersection.GetDestinationForDirection(EnumDirection.UP);
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            destination = currentIntersection.GetDestinationForDirection(EnumDirection.LEFT);
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            destination = currentIntersection.GetDestinationForDirection(EnumDirection.DOWN);
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            destination = currentIntersection.GetDestinationForDirection(EnumDirection.RIGT);
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            var level = currentIntersection.GetLevel();
            if (level != null && !(level.Length == 0))
            {
                sceneLoader.ChangeToScene(level, "hearing", 0);   
            }
        }
            
    }


}
