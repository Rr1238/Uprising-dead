using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool _available;
    public bool _buildingIsBuilt;
    public int _quantityOfCollidersInObject;
    public string _color = "default";

    [SerializeField] private List<Material> _defaultMaterials;

    private void Start()
    {
        Debug.Log(gameObject.GetComponent<MeshRenderer>().materials.Length);
        for (int i = 0; i < gameObject.GetComponent<MeshRenderer>().materials.Length; i++)
        {
            _defaultMaterials.Add(gameObject.GetComponent<MeshRenderer>().materials[i]);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!_buildingIsBuilt && other.gameObject.layer != 6)
        {
            _quantityOfCollidersInObject++;
            if (_quantityOfCollidersInObject > 0)
            {
                _available = false;
                ColoringInRed();
                _color = "red";
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!_buildingIsBuilt && other.gameObject.layer != 6)
        {
            _quantityOfCollidersInObject--;
            if (_quantityOfCollidersInObject < 1)
            {
                _available = true;
                ColoringInGreen();
                _color = "green";
            }
        }
    }
    public void ColoringInRed()
    {
        for(int i = 0; i < gameObject.GetComponent<MeshRenderer>().materials.Length; i++)
        {
            gameObject.GetComponent<MeshRenderer>().materials[i].color = Color.red;
        }
        _color = "red";

    }
    public void ColoringInGreen()
    {
        for (int i = 0; i < gameObject.GetComponent<MeshRenderer>().materials.Length; i++)
        {
            gameObject.GetComponent<MeshRenderer>().materials[i].color = Color.green;
        }
        _color = "green";
    }
    public void ColoringInDefault()
    {
        for (int i = 0; i < gameObject.GetComponent<MeshRenderer>().materials.Length; i++)
        {
            Debug.Log(gameObject.GetComponent<MeshRenderer>().materials[i].color);
            gameObject.GetComponent<MeshRenderer>().materials[i] = _defaultMaterials[i];
            gameObject.GetComponent<MeshRenderer>().materials[i].color = _defaultMaterials[i].color;
        }
        _color = "default";
        Debug.Log("defaultColor");
    }
}
