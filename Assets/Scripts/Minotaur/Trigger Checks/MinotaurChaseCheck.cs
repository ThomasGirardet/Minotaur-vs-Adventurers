using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurChaseCheck : MonoBehaviour
{
    private Minotaur minotaur;

    private void Awake()
    {
        minotaur = GetComponentInParent<Minotaur>();
    }

    private void Update()
    {
        if(minotaur.GetSomeoneHasChest())
        {
            minotaur.SetAggro(true);
        }

        for (int i = 0; i<minotaur.GetAdventurerManager().getAdventurerList().Count; i++)
        {
            if (minotaur.GetAdventurerManager().getAdventurerList()[i].GetComponent<AdventurerBrain>().GetAggroValue() > 0f)
                minotaur.SetAggro(true);
        }
    }
}
