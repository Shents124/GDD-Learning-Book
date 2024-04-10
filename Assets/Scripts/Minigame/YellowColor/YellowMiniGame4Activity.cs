using System;
using Cysharp.Threading.Tasks;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame.YellowColor
{
    public class YellowMiniGame4Activity : BaseActivity
    {
        [SerializeField] private Button momChickenBtn;
        [SerializeField] private Button babyChickenBtn;
        [SerializeField] private Step8Activity step8Activity;

        private int _currentStep;
        protected override void Start()
        {
            momChickenBtn.onClick.AddListener(OnClickedMomChicken);
            babyChickenBtn.onClick.AddListener(OnClickedBabyChicken);
        }

        public override UniTask Initialize(Memory<object> args)
        {
            _currentStep = 0;
            return base.Initialize(args);
        }

        protected override void OnEnable()
        {
            EventManager.Connect(Events.FillColorDone, CheckNextStep);
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            EventManager.Disconnect(Events.FillColorDone, CheckNextStep);
            base.OnDisable();
        }
        
        private void OnClickedMomChicken()
        {
            _currentStep++;
            step8Activity.InitData(TypeObject.MomChicken);
            step8Activity.gameObject.SetActive(true);
        }

        private void OnClickedBabyChicken()
        {
            _currentStep++;
            step8Activity.InitData(TypeObject.BabyChicken);
            step8Activity.gameObject.SetActive(true);
        }
        
        private void CheckNextStep()
        {
            if (_currentStep >= 2)
            {
                
            }
        }
    }
}