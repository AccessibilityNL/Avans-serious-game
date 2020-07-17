using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField]
    private int destroyed = 0;
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        destroyed ++;
    }

    public int GetAmountDestroyed()
    {
        return destroyed;
    }
}
