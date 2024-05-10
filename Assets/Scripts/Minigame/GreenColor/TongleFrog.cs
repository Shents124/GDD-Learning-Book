using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sound.Service;
using UnityEngine;
using UnityEngine.UI;

public class TongleFrog : MonoBehaviour
{
    [SerializeField] private RectTransform m_RectTransform;

    [SerializeField] private Image image;

    public float lengthTongleConst = 570f;

    [SerializeField] private Transform constPosDis;

    private float distanceConst;

    private void Start()
    {
        distanceConst = Vector2.Distance(constPosDis.position, m_RectTransform.position);
        m_RectTransform.sizeDelta = new Vector2(0, m_RectTransform.sizeDelta.y);
        image.fillAmount = 0;
    }

    public void Eat(AnimalFlyRandom animal, Action<bool> callBack)
    {
        image.fillAmount = 0;
        animal.CancelFly();
        AudioUtility.PlaySFX(AudioClipName.Frog_catch);
        transform.rotation = Vector2Extensions.LookAt2D(transform.position, animal.transform.position);
        float distance = Vector2.Distance(animal.transform.position, transform.position);
        m_RectTransform.sizeDelta = new Vector2(570f * distance / distanceConst, m_RectTransform.sizeDelta.y);
        image.DOFillAmount(1, 0.75f).OnComplete(() => {
            image.fillAmount = 1;
            animal.transform.parent = transform;
            animal.GetComponent<RectTransform>().DOAnchorPosX(0, 0.75f).OnComplete(() => {
                animal.gameObject.SetActive(false);
            });
            image.DOFillAmount(0, 0.75f).OnComplete(() => {
                AudioUtility.PlaySFX(AudioClipName.Frog_eat);
                callBack?.Invoke(animal.isCurrentAnimal);
            });
        });
    }
}

public static class Vector2Extensions
{
    public static Quaternion LookAt2D(this Vector2 fromPosition, Vector2 toPosition)
    {
        Vector2 direction = toPosition - fromPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
