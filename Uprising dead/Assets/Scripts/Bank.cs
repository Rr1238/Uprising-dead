using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bank : MonoBehaviour
{
    //Свободные жители.
    public int _freeResidents;
    public int _materials;

    [SerializeField] private TextMeshProUGUI _FR;
    [SerializeField] private TextMeshProUGUI _M;
    [SerializeField] private int _numberOfTowers;
    [SerializeField] private GameObject _victoryPlate;

    private void Start()
    {
        BankUpdate();
    }
    public void Put(string type, int quantity)
    {
        if (type == "M")
        {
            _materials += quantity;
        }
        else if (type == "FR")
        {
            _freeResidents += quantity;
        }
        BankUpdate();
    }
    private void BankUpdate()
    {
        _FR.text = _freeResidents.ToString();
        _M.text = _materials.ToString();
    }
    public void TowerBuilding()
    {
        _numberOfTowers++;
        if (_numberOfTowers >= 4)
        {
            _victoryPlate.SetActive(true);
        }
    }
}