using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class AdventurerManager : MonoBehaviour
{
    private static AdventurerManager instance;
    List<GameObject> adventurerList = new List<GameObject>();

    public static AdventurerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AdventurerManager>();
                if (instance == null)
                {
                    GameObject managerObject = new GameObject("AdventurerManager");
                    instance = managerObject.AddComponent<AdventurerManager>();
                }
            }
            return instance;
        }
    }

    public void addAdventurer(GameObject adventurer)
    {
        if (adventurer != null)
        {
            adventurerList.Add(adventurer);
        }
    }

    public void removeAdventurer(GameObject adventurer)
    {
        adventurerList.Remove(adventurer);
    }

    public List<GameObject> getAdventurerList()
    {
        return adventurerList;
    }

    public void printAdventurerList()
    {
        for (int i = 0; i < adventurerList.Count; i++)
        {
            print(adventurerList[i]);
        }
    }

    private void Start()
    {
        GameObject[] adventurers = GameObject.FindGameObjectsWithTag("Adventurer");
        foreach (GameObject adventurer in adventurers)
        {
            addAdventurer(adventurer);
        }
        int adventurerCarryingChest = Random.Range(0, 4);
        Sensors.Instance.chosenChestCarryingAdventurer = adventurerList[adventurerCarryingChest].GetComponent<AdventurerBrain>();
    }
}
