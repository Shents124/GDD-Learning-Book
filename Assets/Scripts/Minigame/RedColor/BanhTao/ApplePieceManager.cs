using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplePieceManager : MonoBehaviour
{
    public List<ApplePiece> allPieces;

    public int currentPieceDone
    {
        get => _currentPieceDone; 
        set
        {
            _currentPieceDone = value;
            CheckDone();
        }
    }

    private async void CheckDone()
    {
        if (_currentPieceDone >= allPieces.Count)
        {
            await currentCake.DoneStep();
            this.gameObject.SetActive(false);
            EventManager.SendSimpleEvent(Events.ArrangeCakeDone);
        }
    }

    private int _currentPieceDone;

    public Cake currentCake;

    private void Start()
    {
        foreach (var item in allPieces)
        {
            item.FillData(this, currentCake);   
        }
    }

    private void OnMouseDown()
    {
        if(currentPieceDone < allPieces.Count)
        {
            allPieces[currentPieceDone].SetUsed();
        }
    }

    private void OnMouseUp()
    {
        if (currentPieceDone < allPieces.Count)
        {
            allPieces[currentPieceDone].SetNotUsed();
        }
    }
}
