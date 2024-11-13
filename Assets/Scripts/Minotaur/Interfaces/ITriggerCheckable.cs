using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerCheckable
{
    bool IsIdling { get; set; }
    bool IsPatrolling { get; set; }
    bool IsWithinStrikingDistance { get; set; }
    bool IsAggroed { get; set; }
    bool IsWithinView {  get; set; }

    void SetIdlingStatus(bool isIdling);
    void SetPatrollingStatus(bool isPatrolling);
    void SetAggro(bool isAggro);
    void SetWithinStrikingDistance(bool isWithinStrikingDistance);
    void SetWithinView(bool isWithinView);
}
