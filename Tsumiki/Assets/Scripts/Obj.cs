using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj : MonoBehaviour
{
    SpriteRenderer _sprite;
    Vector3 _firstPosition;

    public bool IsOK ()
    {
        if (Vector3.Distance(transform.position, _firstPosition) < 0.5f)
        {
            return true;
        }
        return false;
    }

    public void Bomb()
    {
        _rigidbody.AddForce(Quaternion.Euler(0, 0, 360f * Random.value) * Vector2.up * 2500f);
        _rigidbody.AddTorque(2500f * Random.value);
    }

    Rigidbody2D _rigidbody = null;

    // Start is called before the first frame update
    void Awake()
    {
        _firstPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var color = _sprite.color;
        color.a = IsOK() ? 1f : 0.5f;
        _sprite.color = color;
    }
}
