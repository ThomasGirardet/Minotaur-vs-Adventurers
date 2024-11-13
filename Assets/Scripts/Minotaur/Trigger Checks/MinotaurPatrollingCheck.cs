using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurPatrollingCheck : MonoBehaviour
{
    private Minotaur minotaur;

    private void Awake()
    {
        minotaur = GetComponentInParent<Minotaur>();
    }

    private void OnPatrolTriggerEnter()
    {
        minotaur.SetPatrollingStatus(true);
    }
    private void OnPatrolTriggerExit()
    {
        minotaur.SetPatrollingStatus(false);
    }
}
