using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AnimScreen : MonoBehaviour
{
    private Image bgWhite;
    private void Awake()
    {
        bgWhite = GetComponent<Image>();
        bgWhite.DOFade(0, 0);
    }
    private void OnEnable()
    {
        bgWhite.DOFade(1, 0.25f).OnComplete(() => {
            bgWhite.DOFade(0, 0.25f);
            this.gameObject.SetActive(false);
        });
    }
}
