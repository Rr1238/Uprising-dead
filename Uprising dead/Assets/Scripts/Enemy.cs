using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _purpose;
    [SerializeField] private float _speed;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _health;
    private void Start()
    {
        _health = _maxHealth;
    }
    private void Update()
    {
        SeeOnObject();
        Run();
    }
    private void SeeOnObject()
    {
        Vector3 differens = _purpose.transform.position - transform.position;
        float rot_y = Mathf.Atan2(differens.x, differens.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, rot_y, 0f);
    }
    private void Run()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
    public void TakeDamage(int _damage)
    {
        _health -= _damage;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
