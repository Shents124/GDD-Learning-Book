using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplePiece : MonoBehaviour
{
    private bool _isUsed = false;

    private bool _isCorrect = false;

    private Vector3 posStart;

    private Collider2D currentCake;

    public ApplePieceManager manager;

    public void FillData(ApplePieceManager manager, Cake cake)
    {
        posStart = transform.position;
        currentCake = cake.GetComponent<Collider2D>();
        this.manager = manager; 
    }
    private void Update()
    {
        if (_isUsed)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    public void SetUsed()
    {
        _isUsed = true;
        GetComponent<Collider2D>().enabled = true;
    }


    public void SetNotUsed()
    {
        if (_isCorrect)
        {
            transform.position = posStart;
            gameObject.SetActive(false);
            currentCake.GetComponent<Cake>().FillPieces(manager.currentPieceDone);
            manager.currentPieceDone++;
        }
        else
        {
            transform.position = posStart;
            _isUsed = false;
        }

        GetComponent<Collider2D>().enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(currentCake == collision) 
        {
            _isCorrect = true;
        }
        //if (collision.CompareTag("Cake"))
        //{

        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (currentCake == collision)
        {
            _isCorrect = false;
        }
    }

}
