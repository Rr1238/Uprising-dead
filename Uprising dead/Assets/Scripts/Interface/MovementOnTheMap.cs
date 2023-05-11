using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementOnTheMap : MonoBehaviour
{
    [SerializeField] private float[] _cameraRestrictionX1X2;
    [SerializeField] private float[] _cameraRestrictionZ1Z2;
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
        tempVect = tempVect.normalized * _speedMap * (_cameraGO.transform.position.y - 10) * Time.deltaTime;
        _cameraGO.transform.position += tempVect;
        _cameraGO.transform.position = new Vector3(_cameraGO.transform.position.x, transform.position.y + mw * _speedOfDistance * (transform.position.y - 10), _cameraGO.transform.position.z);
        transform.position = new Vector3
            (
            Mathf.Clamp(transform.position.x, -_cameraRestrictionX1X2[0], _cameraRestrictionX1X2[1]),
            Mathf.Clamp(transform.position.y, _cameraRestrictionY1Y2[0], _cameraRestrictionY1Y2[1]),
            Mathf.Clamp(transform.position.z, -_cameraRestrictionZ1Z2[0], _cameraRestrictionZ1Z2[1])
            );
    }

}
