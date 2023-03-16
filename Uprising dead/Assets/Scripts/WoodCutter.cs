using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCutter : MonoBehaviour
{
    [SerializeField] private GameObject _lumberjack;
    [SerializeField] private List<GameObject> _lumberjacks;
    [SerializeField] private List<GameObject> _trees;
    [SerializeField] private Bank _scriptBank;

    [SerializeField] private int _numbersOfLumberjacks;
    [SerializeField] private bool _flag = true;

    private Building _scriptThisBuilding;

    private float _shortestDistanceToPurpose = 100000;
    public GameObject _nearestTree;
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
    private void StartingFunction()
    {
        foreach (GameObject tree in GameObject.FindGameObjectsWithTag("Tree"))
        {
            _trees.Add(tree);
        }
        for (int i = 0; i < _numbersOfLumberjacks; i++)
        {
            _lumberjacks.Add(Instantiate(_lumberjack));
            _lumberjacks[i].GetComponent<Lumberjack>()._startingPlace = new Vector3(gameObject.transform.position.x + 3, 1, gameObject.transform.position.z + i);
            _lumberjacks[i].GetComponent<Lumberjack>().transform.position = _lumberjacks[i].GetComponent<Lumberjack>()._startingPlace;
            _lumberjacks[i].GetComponent<Lumberjack>()._myWoodCutter = gameObject;
        }
    }
    public void FindTheNearestTree()
    {

        foreach (GameObject tree in _trees)
        {
            if (tree != null)
            {
                if ((_shortestDistanceToPurpose * _shortestDistanceToPurpose > (gameObject.transform.position - tree.transform.position).sqrMagnitude) && tree.GetComponent<Tree>()._isSelected == false)
                {
                    _shortestDistanceToPurpose = (gameObject.transform.position - tree.transform.position).magnitude;
                    _nearestTree = tree;
                }
            }
        }
        _shortestDistanceToPurpose = 1000000;
        if (_nearestTree != null)
        {
            if (_nearestTree.GetComponent<Tree>()._isSelected)
            {
                _nearestTree = null;
                Debug.Log("Нет деревьев, которые лесоруб может срубить, он остался без работы");
            }

        }
        else 
        {
            Debug.Log("Нет деревьев, которые лесоруб может срубить, он остался без работы");
        }
    }
}
