using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj : MonoBehaviour
{
    SpriteRenderer _sprite;
    Vector3 _firstPosition;
    Vector3 _firstRotation;

    public bool IsOK ()
    {
        if (Vector3.Distance(transform.position, _firstPosition) < 0.2f)
        {
            return true;
        }
        return false;
    }

    public void Reset()
    {
        transform.position = _firstPosition;
        transform.eulerAngles = _firstRotation;
        Destroy(_rigidbody);
    }

    public void SetPosition(Vector3 pos)
    {
        _rigidbody.position = pos;
        _rigidbody.velocity = Vector3.zero;
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
        _firstRotation = transform.eulerAngles;
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        var shadow = new GameObject();
        shadow.transform.position = transform.position;
        shadow.transform.rotation = transform.rotation;
        var renderer = shadow.AddComponent<SpriteRenderer>();
        renderer.sprite = _sprite.sprite;
        renderer.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        renderer.sortingOrder = -1;
    }

    // Update is called once per frame
    void Update()
    {
        var color = _sprite.color;
        color.a = IsOK() ? 1f : 0.5f;
        _sprite.color = color;
    }
}
