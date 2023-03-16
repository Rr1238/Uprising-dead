using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementOnTheMap : MonoBehaviour
{
    private Vector3 _mousePosition;
    private Vector2 _cameraMovement;
    private bool _mouseButtonIsDown = false;
    [SerializeField] private float[] _cameraRestrictionXZ;
    [SerializeField] private float[] _cameraRestrictionY1Y2;
    [SerializeField] private float _speedMap;
    [SerializeField] private float _speedOfDistance;
    [SerializeField] private GameObject _cameraGO;
    [SerializeField] private Camera _camera;
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mw = Input.GetAxis("Mouse ScrollWheel");

        Vector3 tempVect = new Vector3(h, 0, v);
        tempVect = tempVect.normalized * _speedMap * _camera.orthographicSize * Time.deltaTime;
        _cameraGO.transform.position += tempVect;


        _cameraGO.transform.position = new Vector3(_cameraGO.transform.position.x, transform.position.y + mw * _speedOfDistance , _cameraGO.transform.position.z);

        if (Input.GetMouseButtonDown(1))
        {
            _mouseButtonIsDown = true;
            _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }
        else if (Input.GetMouseButtonUp(1))
        {
            _mouseButtonIsDown = false;
        }
        if (_mouseButtonIsDown)
        {
            Movement();
        }
        transform.position = new Vector3
            (
            Mathf.Clamp(transform.position.x, -_cameraRestrictionXZ[0], _cameraRestrictionXZ[0]),
            Mathf.Clamp(transform.position.y, _cameraRestrictionY1Y2[0], _cameraRestrictionY1Y2[1]),
            Mathf.Clamp(transform.position.z, -_cameraRestrictionXZ[1], _cameraRestrictionXZ[1])
            );
    }
    void Movement()
    {
        _cameraMovement = _mousePosition - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _cameraGO.transform.Translate(_cameraMovement);
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
    }
}
