using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Minotaur minotaur;

    private void Awake()
    {
        minotaur = GetComponentInParent<Minotaur>();
    }

    private void OnIdleTriggerEnter()
    {
        minotaur.SetIdlingStatus(true);
    }

    private void OnIdleTriggerExited()
    {

        minotaur.SetIdlingStatus(false);
    }
}
