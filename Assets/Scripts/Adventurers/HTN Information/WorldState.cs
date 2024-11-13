using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldState
{
    private GameObject minotaur;
    private GameObject chest;

    #region World State Variables
    public enum EObjectNextTo
    {
        Chest,
        Minotaur,
        Corner,
        None
    }
    public bool ChosenChestCarrier { get; set; } //If chest claimed, can't hold chest
    public bool CanAttack { get; set; } //If can attack (only do it if not holding/trying to claim chest), attack minotaur
    public bool CanHoldChest { get; set; } //If can hold chest, go to pick up chest
    public bool IsHoldingChest { get; set; } //If holding chest, try to take chest to corner
    public bool InLineOfSight { get; set; }
    public bool InAttackingRange { get; set; }
    //public GameObject Minotaur {  get; set; }
    //public GameObject Chest { get; set; }
    public EObjectNextTo ObjectNextTo { get; set; }
    #endregion

    #region Getters/Setters
    public GameObject GetMinotaur() { return minotaur; }
    public GameObject GetChest() {  return chest; }
    #endregion

    public WorldState()
    {
        Initialize();
    }

    private void Initialize()
    {
        minotaur = GameObject.FindObjectOfType<Minotaur>().gameObject;
        chest = GameObject.FindGameObjectWithTag("Chest");
    }

    public WorldState Clone()
    {
        return new WorldState
        {
            ChosenChestCarrier = ChosenChestCarrier,
            CanAttack = CanAttack,
            CanHoldChest = CanHoldChest,
            IsHoldingChest = IsHoldingChest,
            InLineOfSight = InLineOfSight,
            InAttackingRange = InAttackingRange,
            minotaur = minotaur,
            chest = chest,
            ObjectNextTo = ObjectNextTo
        };
    }
}
