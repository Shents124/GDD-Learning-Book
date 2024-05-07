using System;
using DG.Tweening;
using Sound.Service;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class BtnCollectFruit : MonoBehaviour
{
    private SkeletonGraphic animFruit;
    [SerializeField] [SpineAnimation]
    public string animIdle, animLaugh;

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
        animFruit = GetComponentInChildren<SkeletonGraphic>();
        posStart = transform.position;
        if (canDrag)
        {
            dragObject = GetComponent<DragObject>();
            dragObject.Initialize(CheckDoneObj, () => {
                animFruit.AnimationState.SetAnimation(0, animLaugh, true);
            }, () => {
                animFruit.AnimationState.SetAnimation(0, animIdle, true);
            });
        }
        else
        {
            btnClick = GetComponent<Button>();
            btnClick.onClick.AddListener(CheckDoneObj);
        }
    }

    private void CheckDoneObj()
    {
        if (animFruit)
        {
            animFruit.AnimationState.SetAnimation(0, animLaugh, true);
        }

        if (correctFruit)
        {
            AudioUtility.PlaySFX(AudioClipName.Jump);
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
            AudioUtility.PlaySFX(AudioClipName.Falldown);
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
