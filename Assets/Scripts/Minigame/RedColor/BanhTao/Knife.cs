using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public TypeKnife type;

    public GameObject mask;

    private bool _isUsed = false;

    private bool _onEnterLineCut = false;

    private Vector3 posStart;

    public void DisActiveKnife()
    {
        this.gameObject.SetActive(false);
        transform.position = posStart;
        _isUsed = false;
        transform.rotation = Quaternion.Euler(Vector3.forward * -60f);
        mask.gameObject.SetActive(false);
    }

    private void Start()
    {
        _isUsed = false;
        posStart = transform.position;
    }

    private void OnMouseDown()
    {
        _isUsed = true;
        if(type == TypeKnife.Peel)
        {
            transform.rotation = Quaternion.identity;
        }
        else
        {
            transform.rotation = Quaternion.Euler(Vector3.forward * -60f);
        }
    }

    private void OnMouseDrag()
    {
        if (_isUsed)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void OnMouseUp()
    {
        transform.position = posStart;
        _isUsed = false;
        transform.rotation = Quaternion.Euler(Vector3.forward * -60f);
        mask.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var line = collision.GetComponent<LineCutFruit>();

        if (line != null)
        {
            if (line.isEnterLine)
            {
                _onEnterLineCut = true;
                mask.gameObject.SetActive(true);
            }
            else
            {
                if(_onEnterLineCut)
                {
                    switch (type)
                    {
                        case TypeKnife.Cut:
                            EventManager.SendSimpleEvent(Events.CutAppleDone);
                            break;
                        case TypeKnife.Peel:
                            EventManager.SendSimpleEvent(Events.PeelAppleDone);
                            break;
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var line = collision.GetComponent<LineCutFruit>();

        if (line != null)
        {
            if (line.isEnterLine)
            {
                _onEnterLineCut = false;
                mask.gameObject.SetActive(false);
            }
        }
    }
}

public enum TypeKnife
{
    Cut,
    Peel
}
