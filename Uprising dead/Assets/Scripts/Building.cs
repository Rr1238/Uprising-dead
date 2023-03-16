using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingGrid _scriptBuildingGrid;
    public Bank _scriptbank;
    private Building _thisScript;

    public List<Renderer> _partsOfTheBuilding;
    public List<Color> _defaultMaterialsPartsOfObject;
    public bool _buildingIsBuilt;
    public int _quantityOfCollidersInObject;
    //Население, металл
    public List<int> _price;

    private void Start()
    {
        _thisScript = gameObject.GetComponent<Building>();
        _buildingIsBuilt = false;

        for (int _numberMaterial = 0; _numberMaterial < _partsOfTheBuilding.Count; _numberMaterial++)
        {
            _defaultMaterialsPartsOfObject.Add(_partsOfTheBuilding[_numberMaterial].material.color);
        }
        ColoringInGreen(_partsOfTheBuilding);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!_buildingIsBuilt && other.gameObject.tag != "Plane") {
            _quantityOfCollidersInObject++;
            if (_quantityOfCollidersInObject > 0)
            {
                _scriptBuildingGrid._available = false;
                ColoringInRed(_partsOfTheBuilding);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!_buildingIsBuilt && other.gameObject.tag != "Plane") {
            _quantityOfCollidersInObject--;
            if (_quantityOfCollidersInObject < 1)
            {
                _scriptBuildingGrid._available = true;
                ColoringInGreen(_partsOfTheBuilding);
            }
        }
    }
    public void ColoringInRed(List<Renderer> _materialsOfObject) {
        for (int _numberMaterial = 0; _numberMaterial < _materialsOfObject.Count; _numberMaterial++) {
            _materialsOfObject[_numberMaterial].material.color = Color.red;   
        }
    }
    public void ColoringInGreen(List<Renderer> _materialsOfObject)
    {
        for (int _numberMaterial = 0; _numberMaterial < _materialsOfObject.Count; _numberMaterial++)
        {
            _materialsOfObject[_numberMaterial].material.color = Color.green;
        }
    }
    public void ColoringInDefault(Building _buildingScript, List<Color> _defaultMaterialsPartsOfObject)
    {
        for (int _numberMaterial = 0; _numberMaterial < _buildingScript._partsOfTheBuilding.Count; _numberMaterial++)
        {
            _buildingScript._partsOfTheBuilding[_numberMaterial].material.color = _defaultMaterialsPartsOfObject[_numberMaterial];
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}