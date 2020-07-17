using Assets.Scripts.Overworld;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intersection : MonoBehaviour
{
    [SerializeField]
    private DestinationWithPath[] destinations;
    [SerializeField]
    private string startLevel;


    private void Start()
    {
        if (destinations.Length != 4)
        {
            Debug.LogError("Destinations size needs to be 4, is ", this);
        }
    }

    public DestinationWithPath GetDestinationForDirection(EnumDirection direction)
    {
        DestinationWithPath destination;
        switch (direction)
        {
            case EnumDirection.UP:
                destination = destinations[0];
                break;
            case EnumDirection.DOWN:
                destination = destinations[1];
                break;
            case EnumDirection.LEFT:
                destination = destinations[2];
                break;
            default:
                destination = destinations[3];
                break;
        }

        if (destination.destination == null || destination.path == null)
            return null;
        
        return destination;
    }

    public string GetLevel()
    {
        return startLevel;
    }
}