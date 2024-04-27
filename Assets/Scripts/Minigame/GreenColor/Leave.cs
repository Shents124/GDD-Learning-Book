using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Utility;

public class Leave : MonoBehaviour
{
    public Transform posAnimStart;
    public Transform posAnimDone;
    public Transform posFrog;

    [SerializeField] private Transform leaveDone;

    [SerializeField] private Transform leaveFake;
    public async UniTask ShowDone()
    {
        leaveFake.gameObject.SetActive(false);
        leaveDone.gameObject.SetActive(true);
        leaveDone.position = posAnimStart.position;
        leaveDone.DOMove(posAnimDone.position, 0.75f);
        await AsyncService.Delay(0.75f, this);
    }
}
