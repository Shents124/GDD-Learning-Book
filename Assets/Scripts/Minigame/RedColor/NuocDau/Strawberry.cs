using System;
using DG.Tweening;
using UnityEngine;

namespace Minigame.RedColor
{
    public class Strawberry : MonoBehaviour
    {
        [SerializeField] private Draggable draggable;
        
        public void SetData(Action action)
        {
            draggable.AddListener(action, () => {
                transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.2f);
            });

            draggable.AddListener(() => {
                transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.2f);
            });
        }
    }
}