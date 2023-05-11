using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionController : MonoBehaviour
{
    public Building _scriptBuilding;

    [SerializeField] private LayerMask _terrain;
    [SerializeField] private int _maxHeight;
    [SerializeField] private int _minHeight;
    private Camera _mainCamera;
    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (_scriptBuilding != null)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f, _terrain))
            {
                Debug.Log(Vector3.Angle(Vector3.up, hit.normal));
                if (Vector3.Angle(Vector3.up, hit.normal) > 20 || hit.point.y > _maxHeight || hit.point.y < _minHeight)
                {
                    if(_scriptBuilding._color != "red")
                    {
                        _scriptBuilding.ColoringInRed();
                        _scriptBuilding._available = false;
                    }
                }
                else if(_scriptBuilding._quantityOfCollidersInObject <=0)
                {
                    _scriptBuilding._available = true;
                    _scriptBuilding.ColoringInGreen();
                }
                _scriptBuilding.transform.position = hit.point;
                if (_scriptBuilding._available && Input.GetMouseButtonDown(0))
                {
                    _scriptBuilding.gameObject.GetComponent<Collider>().isTrigger = false;
                    _scriptBuilding.ColoringInDefault();
                    _scriptBuilding._buildingIsBuilt = true;
                    _scriptBuilding = null;
                }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Vector3 rotate = _scriptBuilding.transform.eulerAngles;
                rotate.y += 10;
                _scriptBuilding.transform.rotation = Quaternion.Euler(rotate);
            }
        }
    }
    public void StartPlacingBuilding(Building GO_for_building)
    {
        if (_scriptBuilding == null)
        {
            _scriptBuilding = Instantiate(GO_for_building);
        }
        else
        {
            Destroy(_scriptBuilding);
            _scriptBuilding = Instantiate(GO_for_building);
        }

    }
}
