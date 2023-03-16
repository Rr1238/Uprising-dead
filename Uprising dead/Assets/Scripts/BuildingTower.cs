using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTower : MonoBehaviour
{
    private bool _flag = true;
    private Building _thisScriptBuilding;
    [SerializeField] private Bank _scriptBank;
    private void Start()
    {
        _thisScriptBuilding = gameObject.GetComponent<Building>();
    }
    private void Update()
    {
        if (_thisScriptBuilding._buildingIsBuilt && _flag)
        {
            IfBuildingIsBuild();
            _flag = false;
        }
    }
    public void IfBuildingIsBuild()
    {
        _scriptBank.TowerBuilding();
    }
}
