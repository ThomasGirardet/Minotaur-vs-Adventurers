using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensors : MonoBehaviour
{
    public List<AdventurerBrain> adventurers = new();
    public Transform chestTransform;
    public AdventurerBrain chosenChestCarryingAdventurer;
    public AdventurerBrain adventurerHoldingChest;
    public AdventurerBrain adventurerAttacking;

    public static Sensors Instance;

    //Only want one instance of sensors active => Singleton
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
