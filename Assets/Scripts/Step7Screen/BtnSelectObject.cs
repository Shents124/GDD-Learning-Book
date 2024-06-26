﻿using Step7;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Step7Screen
{
    public class BtnSelectObject : MonoBehaviour
    {
        public TypeObject objectSelect;

        [SerializeField] private GameObject imageDone;

        [SerializeField] Step7Activity step7;

        [SerializeField] private Button btnSelect;

        private void Start()
        {
            btnSelect.onClick.AddListener(OnClickedObj);
        }

        private async void OnClickedObj()
        {
            step7.AddStep();
            step7.stepFillColor.InitData(objectSelect);
            step7.stepFillColor.gameObject.SetActive(true);
            btnSelect.onClick.RemoveAllListeners();
            await AsyncService.Delay(1f, this);
            imageDone.SetActive(true);
            this.gameObject.SetActive(false);
            btnSelect.interactable = false;
        }
    }
}