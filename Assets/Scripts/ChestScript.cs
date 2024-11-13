using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public GameObject[] adventurers { get; private set; }
    private AdventurerBrain adventurer;
    private Minotaur minotaur;

    private void Awake()
    {
        adventurers = GameObject.FindGameObjectsWithTag("Adventurer");
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(adventurers.Contains(collision.gameObject))
        {

        }
    }
}
