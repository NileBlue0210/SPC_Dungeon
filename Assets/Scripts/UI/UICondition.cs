using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health;
    public Condition stamina;

    void Start()
    {
        UnitManager.Instance.Player._playerCondition.uiCondition = this;
    }

    void Update()
    {
        
    }
}
