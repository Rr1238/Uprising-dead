using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUnits : MonoBehaviour
{
    [SerializeField] private Texture _selectTexture;
        
    private Ray _ray;
    private Ray _rayRMB;
    private RaycastHit _hit;
    private RaycastHit _hitRMB;
    private Vector3 _mouseStartPosition;
    private float _mouseX;
    private float _mouseY;
    private float _selectionHeight;
    private float _selectionWidth;
    private Vector3 _selectionStartPoint;
    private Vector3 _selectionEndPoint;
    private bool _selecting;
    public UnitsControl _scriptUnitsControl;

    

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _selecting = true;
            _mouseStartPosition = Input.mousePosition;

            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit))
            {
                _selectionStartPoint = _hit.point;
            }
        }

        _mouseX = Input.mousePosition.x;
        _mouseY = Screen.height - Input.mousePosition.y;
        _selectionWidth = _mouseStartPosition.x - _mouseX;
        _selectionHeight = Input.mousePosition.y - _mouseStartPosition.y;

        if (Input.GetMouseButtonUp(0))
        {
            _selecting = false;
            DeselectAll();

            if (_mouseStartPosition == Input.mousePosition)
            {
                SingleSelect();
            }
            else
            {
                MultiSelect();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            _rayRMB = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_rayRMB, out _hitRMB))
            {
                if (_hitRMB.collider.gameObject.layer == 6)
                {
                    foreach (GameObject unit in _scriptUnitsControl._units)
                    {
                        if (unit.GetComponent<UnitControl>()._isSelected)
                        {
                            unit.GetComponent<UnitControl>()._purpose = _hitRMB.point;
                        }
                    }
                }
            }
        }
    }

    private void OnGUI()
    {
        if (_selecting)
        {
            GUI.DrawTexture(new Rect(_mouseX, _mouseY, _selectionWidth, _selectionHeight), _selectTexture);
        }

    }

    private void MultiSelect()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit))
        {
            _selectionEndPoint = _hit.point;
        }
        SelectHightlighted();
    }

    private void SelectHightlighted()
    {
        foreach (GameObject unit in _scriptUnitsControl._units)
        {
            float x = unit.transform.position.x;
            float z = unit.transform.position.z;

            if ((x > _selectionStartPoint.x && x < _selectionEndPoint.x) || (x < _selectionStartPoint.x && x > _selectionEndPoint.x))
            {
                if ((z > _selectionStartPoint.z && z < _selectionEndPoint.z) || (z < _selectionStartPoint.z && z > _selectionEndPoint.z))
                {
                    unit.GetComponent<UnitControl>()._isSelected = true;
                    unit.GetComponent<MeshRenderer>().material.color = Color.green;
                }
            }
        }
    }

    private void SingleSelect()
    {
        DeselectAll();
        if (_hit.collider.gameObject.tag == "Unit")
        {
            _hit.collider.gameObject.GetComponent<UnitControl>()._isSelected = true;
            _hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }

    private void DeselectAll()
    { 
        foreach(GameObject unit in _scriptUnitsControl._units)
        {
            unit.GetComponent<UnitControl>()._isSelected = false;
            unit.GetComponent<MeshRenderer>().material.color = Color.white; 
        }
    }
}
