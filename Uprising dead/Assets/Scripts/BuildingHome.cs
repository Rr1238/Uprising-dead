using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHome : MonoBehaviour
{
    [SerializeField] private Building _scriptThisBuilding;
    [SerializeField] private Bank _scriptBank;

    [SerializeField] private bool _flag = true;
    void Start()
    {
        _scriptThisBuilding = gameObject.GetComponent<Building>();
    }
    void Update()
    {
        if (_scriptThisBuilding._buildingIsBuilt)
        {
            if (_flag)
            {
                StartingFunction();
                _flag = false;
            }
        }
    }
    void StartingFunction()
    {
        _scriptBank.Put("FR", 5);
    }
}
