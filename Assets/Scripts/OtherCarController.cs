using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherCarController : MonoBehaviour
{
    [SerializeField] float _speed = 2.5f;
    
    private float _startX;
    private Vector3 _moveVector;
 
    private void Awake()
    {
        _startX = transform.position.x;
    }

    public void SetDirection(float direction)
    {
        // just get the sign of the direction. We just want it to be 1 or -1
        var dir = Mathf.Sign(direction);
        // pre-calculate the movement vector
        _moveVector = Vector3.right * _speed * dir;
        // 'flip' the car in the correct direction
        transform.localScale = new Vector3(dir, 1f, 1f);
    }

    private void Update()
    {
        // Move the car a bit
        transform.Translate(_moveVector * Time.deltaTime);
        // If we've reached the end, destroy the car
        if (Mathf.Abs(transform.position.x) > _startX) Destroy(gameObject);
    }
}