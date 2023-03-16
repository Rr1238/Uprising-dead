using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitControl : MonoBehaviour
{
    [SerializeField] private float _speed;
    public bool _isSelected;
    public Vector3 _purpose;

    private void Update()
    {
        if (_purpose != Vector3.zero)
        {
            SeeOnObject();
            Run();
        }
        if (Mathf.Abs(transform.position.x - _purpose.x) < 0.1f && Mathf.Abs(transform.position.z - _purpose.z) < 0.1f)
        {
            _purpose = Vector3.zero;
        }
    }
    private void SeeOnObject()
    {
        Vector3 differens = _purpose - transform.position;
        float rot_y = Mathf.Atan2(differens.x, differens.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, rot_y, 0f);
        Debug.Log(rot_y);
    }
    private void Run()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}
