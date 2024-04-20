using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BtnCollectFruit : MonoBehaviour
{
    [NonSerialized] public StepCollect managerStep;

    public bool canDrag = true;

    public float jumpower;

    public float timeJump = 0.5f;

    public bool correctFruit;

    private Button btnClick;

    public DragObject dragObject;

    private Vector2 posStart;

    [SerializeField] private DOTweenAnimation animShake;

    private void Start()
    {
        posStart = transform.position;

        if (canDrag)
        {
            dragObject = GetComponent<DragObject>();
            dragObject.Initialize(CheckDoneObj);
        }
        else
        {
            btnClick = GetComponent<Button>();
            btnClick.onClick.AddListener(CheckDoneObj);
        }
    }
    private void CheckDoneObj()
    {
        if (correctFruit)
        {
            Vector2 scale = transform.localScale;
            transform.DOScale(scale * 0.8f, timeJump);
            transform.DOJump(managerStep.posCollect[managerStep.CurrentFruit].position, jumpower, 1, timeJump).OnComplete(() => {
                managerStep.IncreaseFruit();
                this.gameObject.SetActive(false);
                transform.position = posStart;
                transform.localScale = scale;
            }).SetEase(Ease.Linear);
        }
        else
        {
            animShake.DOPlay();
        }
    }

    public void OnDrop()
    {
        if (correctFruit)
        {
            CorrectFruit();
        }
        else
        {
            animShake.DOPlay();
        }
    }

    private void CorrectFruit()
    {
        Vector3 scale = transform.localScale;
        managerStep.IncreaseFruit();
        this.gameObject.SetActive(false);
        transform.position = posStart;
        transform.localScale = scale * 0.8f;
        dragObject.DisableOverrideSorting();
    }
    
    public void OnFall()
    {
        transform.DOMoveY(managerStep.posFall.position.y, timeJump).OnComplete(() => {
            transform.position = posStart;
            this.gameObject.SetActive(false);
        });
    }
}
