using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Lumberjack : MonoBehaviour
{
    public GameObject _selectedTree;
    public Vector3 _startingPlace;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Bank _scriptBank;
    public GameObject _myWoodCutter;
    private bool _flag = true;
    private bool _flag2 = false;
    private float _timeTheBeginningOfTheCutting;
    [SerializeField] private float _cuttingTime;
    [SerializeField] private float _distanceForCutting;
    [SerializeField] private int _materialsPerTree;

    private void Start()
    {
        AssignmentOfTreeToAgent();
    }
    private void Update()
    {
        if (_selectedTree)
        {
            if (Mathf.Abs(transform.position.x - _selectedTree.transform.position.x) < _distanceForCutting && Mathf.Abs(transform.position.z - _selectedTree.transform.position.z) < _distanceForCutting)
            {
                if (_flag)
                {
                    _timeTheBeginningOfTheCutting = Time.realtimeSinceStartup;
                    _flag = false;
                    _agent.ResetPath();
                }
                else if (_timeTheBeginningOfTheCutting + _cuttingTime <= Time.realtimeSinceStartup)
                {
                    Destroy(_selectedTree);
                    _selectedTree = null;
                    AssignmentOfTreeToAgent();
                    _flag = true;
                    _flag2 = true;
                }
            }
        }
        else 
        {
            if (Mathf.Abs(transform.position.x - _startingPlace.x) < _distanceForCutting && Mathf.Abs(transform.position.z - _startingPlace.z) < _distanceForCutting && _myWoodCutter)
            {
                if (_flag2)
                {
                    _scriptBank.Put("M", _materialsPerTree);
                    _flag2 = false;
                }
                _myWoodCutter.GetComponent<WoodCutter>().FindTheNearestTree();
                if (_myWoodCutter.GetComponent<WoodCutter>()._nearestTree)
                {
                    _selectedTree = _myWoodCutter.GetComponent<WoodCutter>()._nearestTree;
                    _selectedTree.GetComponent<Tree>()._isSelected = true;
                    AssignmentOfTreeToAgent();
                }
            }
        }
    }
    public void AssignmentOfTreeToAgent()
    {
        if (_selectedTree)
        {
            _agent.destination = _selectedTree.transform.position;
        }
        else
        {
            _agent.destination = _startingPlace;
        }
    }
}
