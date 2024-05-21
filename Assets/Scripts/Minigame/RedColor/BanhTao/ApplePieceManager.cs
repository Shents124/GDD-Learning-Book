using System.Collections;
using System.Collections.Generic;
using Sound.Service;
using UnityEngine;

public class ApplePieceManager : MonoBehaviour
{
    public List<ApplePiece> allPieces;

    public GameObject dia;

    public GameObject hand;

    public int currentPieceDone
    {
        get => _currentPieceDone; 
        set
        {
            _currentPieceDone = value;
            AudioUtility.PlaySFX(AudioClipName.Correct);
            CheckDone();
        }
    }

    private async void CheckDone()
    {
        if (_currentPieceDone >= allPieces.Count)
        {
            dia.SetActive(false);
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
        if (hand.activeSelf)
        {
            hand.SetActive(false);
        }

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
