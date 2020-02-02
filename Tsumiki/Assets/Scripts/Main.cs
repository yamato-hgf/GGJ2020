using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [SerializeField] int _nextStage;
    [SerializeField] Text _text;
    [SerializeField] AudioClip[] _sounds;
    [SerializeField] AudioSource _source;

    Obj[] _objs = null;

    Obj _clicked = null;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        _objs = FindObjectsOfType<Obj>();

        yield return new WaitForSeconds(2f);
        _source.clip = _sounds[0];
        _source.Play();

        foreach (var obj in _objs)
        {
            obj.Bomb();
        }

        yield return new WaitForSeconds(2f);
        _text.text = "";

        bool loop = true;
        while (loop) {
            if (Input.GetMouseButtonDown(0))
            {
                _clicked = null;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

                if (hit2d)
                {
                    _clicked = hit2d.transform.gameObject.GetComponent<Obj>();
                }
            }

            if(_clicked != null)
            {
                var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                _clicked.SetPosition(pos);
            }

            if (Input.GetMouseButtonUp(0))
            {
                if(_clicked != null)
                {
                    _clicked = null;
                }
            }

            loop = false;
            foreach (var obj in _objs)
            {
                if (!obj.IsOK())
                {
                    loop = true;
                    break;
                }
            }
            yield return null;
        }

        foreach (var obj in _objs)
        {
            obj.Reset();
        }

        if (_nextStage > 0)
        {
            _text.text = "Success! Go to the next stage.\n\nうまい！次は何が出てくるかな！？";
        }
        else
        {
            _text.text = "Congratulations!\n\nご馳走様でした！";
        }
        _source.clip = _sounds[1];
        _source.Play();
        yield return new WaitForSeconds(3f);

        while(true)
        {
            if(Input.anyKeyDown)
            {
                Application.LoadLevelAsync(_nextStage);
            }
            yield return null;
        }
    }
}
