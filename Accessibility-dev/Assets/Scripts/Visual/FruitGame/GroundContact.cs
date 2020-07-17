using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundContact : MonoBehaviour
{
    [SerializeField]private AudioSource groundContact;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            groundContact.Play();
        }
    }
}
