using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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

        [SerializeField] private TableFood tableFood;

        private int _currentChickenIndex;
        
        public override UniTask Initialize(Memory<object> args)
        {
            return base.Initialize(args);
        }

        private void ShowTable()
        {
            var sets = _tableItemSet[_currentChickenIndex];
            tableFood.Show(sets, OnSelect);
        }
        
        private void OnSelect(TableFoodItem item)
        {
            
        }
    }
}