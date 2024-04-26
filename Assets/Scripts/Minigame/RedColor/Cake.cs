using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Utility;

public class Cake : MonoBehaviour
{
    public GameObject[] piecesDone;

    public GameObject fakeAppleDone;


    [SerializeField] private float timeMove = 1f;

    public Transform posDone;

    public void FillPieces(int currentStep)
    {
        piecesDone[currentStep].SetActive(true);
    }

    public async UniTask DoneStep()
    {
        transform.DOMove(posDone.position, timeMove).SetEase(Ease.Linear);
        transform.DOScale(posDone.localScale, timeMove).SetEase(Ease.Linear);
        fakeAppleDone.SetActive(true);
        await AsyncService.Delay(timeMove, this);
        this.gameObject.SetActive(false);
    }
}
