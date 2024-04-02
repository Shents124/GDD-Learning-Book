using System;
using UnityEngine;

namespace Minigame.RedColor
{
    public class Strawberry : MonoBehaviour
    {
        [SerializeField] private Draggable draggable;
        
        public void SetData(Action action)
        {
            draggable.AddListener(action);
        }
    }
}