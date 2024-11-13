using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MinotaurStrikingDistanceCheck : MonoBehaviour
{
    private Minotaur minotaur;

    private void Awake()
    {
        minotaur = GetComponentInParent<Minotaur>();
    }

    private void Update()
    {
        if (minotaur.GetAttackTimer() == 0f)
        {
            foreach (GameObject adventurer in minotaur.GetAdventurerManager().getAdventurerList())
            {
                //If an adventurer is holding the chest, enter attack state only if that adventurer is within striking distance
                //If no adventurer is holding the chest, enter attack state when within attack range of the first adventurer
                if(minotaur.GetSomeoneHasChest())
                {
                    if (adventurer.GetComponent<AdventurerBrain>().GetHoldingChest() && Vector3.SqrMagnitude(minotaur.transform.position - adventurer.transform.position) < (minotaur.attackRange) * (minotaur.attackRange))
                    {
                        minotaur.SetWithinStrikingDistance(true);
                        break;
                    }
                }
                else
                {
                    if (Vector3.SqrMagnitude(minotaur.transform.position - adventurer.transform.position) < (minotaur.attackRange) * (minotaur.attackRange))
                    {
                        minotaur.SetWithinStrikingDistance(true);
                        break;
                    }
                }
            }
        }
        else
        {
            if(minotaur.GetAttackTimer() > 1f)
            {
                minotaur.SetAttackTimer(0f);
            }
            else
            {
                float newTime = minotaur.GetAttackTimer() + Time.deltaTime;
                minotaur.SetAttackTimer(newTime);
            }
        }
    }
}