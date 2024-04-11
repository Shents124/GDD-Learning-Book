using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Minigame.YellowColor
{
    public class YellowMiniGame2Activity : BaseActivity
    {
        private readonly List<List<YellowFood>> _tableItemSet = new() {
            new List<YellowFood> { YellowFood.Carot, YellowFood.Giun, YellowFood.Ngo },
            new List<YellowFood> { YellowFood.Giun, YellowFood.Ngo, YellowFood.RauCai },
            new List<YellowFood> { YellowFood.Ngo, YellowFood.RauCai, YellowFood.XupLo },
            new List<YellowFood> { YellowFood.RauCai, YellowFood.XupLo, YellowFood.Carot },
        };

        [SerializeField] private float chickenMoveDuration = 1f;
        [SerializeField] private List<RectTransform> lines;
        [SerializeField] private RectTransform selectFoodPosition;
        [SerializeField] private List<BabyChickenSelectFood> babyChickens;
        
        [SerializeField] private TableFood tableFood;

        private int _chickenIndex;
        private int _chickenCount;

        
        public override void DidEnter(Memory<object> args)
        {
            StartCoroutine(SetLineChicken(BabyChickenSelectFood));
            base.DidEnter(args);
        }

        private IEnumerator SetLineChicken(Action onFinish)
        {
            for (int i = 0, n = babyChickens.Count; i < n; i++)
            {
                babyChickens[i].RunToPosition(lines[i], chickenMoveDuration);
                _chickenCount++;
            }

            yield return new WaitForSeconds(chickenMoveDuration);
            onFinish?.Invoke();
        }

        private void BabyChickenSelectFood()
        {
            if (_chickenIndex == 0)
            {
                ShowChickenSelectFood();
            }
            else
            {
                babyChickens[_chickenIndex].RunToPosition(selectFoodPosition, 1f, ShowChickenSelectFood);
            }
        }

        private void HideTable()
        {
            tableFood.Hide();
        }
        
        private void ShowTable()
        {
            var sets = _tableItemSet[_chickenIndex];
            tableFood.Show(sets, OnSelect);
        }

        private void ShowChickenSelectFood()
        {
            babyChickens[_chickenIndex].ShowDialog();
            ShowTable();
        }
        
        private void OnSelect(TableFoodItem item)
        {
            var currentChicken = babyChickens[_chickenIndex];
            if (item.YellowFood != currentChicken.yellowFood)
                return;
            
            currentChicken.OnEat(item, HideTable, CheckFinish);
        }

        private void CheckFinish()
        {
            _chickenIndex++;
            if (_chickenIndex >= _chickenCount)
            {
                UIService.OpenActivityWithFadeIn(ActivityType.MiniGameYellow3Screen);
            }
            else
            {
                BabyChickenSelectFood();
            }
        }
    }
}