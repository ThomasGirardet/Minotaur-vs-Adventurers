using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurFieldOfView : MonoBehaviour
{
    public float radius;
    public float angle;

    public GameObject[] adventurerRefList;
    public int adventurerSeen;
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask terrainMask;

    public bool canSeeAdventurer = false;

    private void Start()
    {
        adventurerRefList = GameObject.FindGameObjectsWithTag("Adventurer");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        
        if(rangeChecks.Length != 0)
        {
            Transform target;
            float shortestDistance = float.MaxValue;

            //Find closest adventurer in the FOV that isn't obstructed and set that to the target
            for (int i = 0; i < rangeChecks.Length; i++)
            {
                Vector3 directionToAdventurer = (rangeChecks[i].transform.position - transform.position).normalized;

                //If within the FOV cone
                if (Vector3.Angle(transform.forward, directionToAdventurer) < angle / 2)
                {
                    float distanceToAdventurer = Vector3.Distance(transform.position, rangeChecks[i].transform.position);

                    //If not obstructed by terrain
                    if (!Physics.Raycast(transform.position, directionToAdventurer, distanceToAdventurer, terrainMask))
                    {
                        if (distanceToAdventurer < shortestDistance)
                        {
                            adventurerSeen = i; //Fix this. It's giving i, but i is limited by the number seen, and doesn't show actual global adventurer list position
                            target = rangeChecks[i].transform;
                            shortestDistance = distanceToAdventurer;
                        }
                        canSeeAdventurer = true;
                    }
                    else
                    { //Can't see if blocked by terrain
                        continue;
                    }
                }
                else //Adventurer is not within FOV angle
                    continue;
            }

            //If you manage to make it through the entire loop without finding a valid target, then canSeeAdventurer = false
            if (!(shortestDistance < float.MaxValue))
            {
                canSeeAdventurer = false;
            }
        }
        else
        {
            canSeeAdventurer = false;
        }

        if (canSeeAdventurer)
        {
            Debug.Log(adventurerRefList[adventurerSeen].name);
        }
    }
}
