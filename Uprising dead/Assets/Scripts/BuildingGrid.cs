using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    public Building _scriptBuilding;
    public Bank _scriptBank;

    private Camera _mainCamera;
    public bool _available;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }
    public void StartPlacingBuilding(Building GO_for_building) {
        if (_scriptBank._freeResidents >= GO_for_building._price[0] && _scriptBank._materials >= GO_for_building._price[1])
        {
            if (_scriptBuilding == null)
            {
                _scriptBuilding = Instantiate(GO_for_building);
            }
            else
            {
                _scriptBuilding.Destroy();
                _scriptBuilding = Instantiate(GO_for_building);
            }

        }
        
    }
    public void StopBuilding() {
        _scriptBuilding.Destroy();
    }
    private void Update()
    {
        if (_scriptBuilding != null) {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (groundPlane.Raycast(ray, out float position)) {
                Vector3 worldposition = ray.GetPoint(position);
                _scriptBuilding.transform.position = worldposition;
                if (_available && Input.GetMouseButtonDown(0)) {
                    _scriptBuilding._buildingIsBuilt = true;
                    _scriptBank.Put("FR", -_scriptBuilding._price[0]);
                    _scriptBank.Put("M", -_scriptBuilding._price[1]);
                    _scriptBuilding.ColoringInDefault(_scriptBuilding, _scriptBuilding._defaultMaterialsPartsOfObject);
                    _scriptBuilding.gameObject.GetComponent<Collider>().isTrigger = false;
                    _scriptBuilding = null;
                }
            }
        }
    }

}
