using System;
using Sound.Service;

namespace Step345Screen
{
    public class Step345BaseScreen : BaseActivity
    {
        protected override void InitializeData(Memory<object> args)
        {
            AudioUtility.PlayMusicGamePlay();
            base.InitializeData(args);
        }
    }
}