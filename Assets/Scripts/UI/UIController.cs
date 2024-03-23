using System;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using ZBase.UnityScreenNavigator.Core;
using ZBase.UnityScreenNavigator.Core.Activities;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Screens;

namespace UI
{
    public class UIController : UnityScreenNavigatorLauncher
    {
        private const float PADDING_RECT_MASK = -10f;

        public Action onInitializeDone;
        
        protected override void OnEnable()
        {
            SpriteAtlasManager.atlasRequested += RequestAtlas;
            base.OnEnable();
        }

        protected override void Start()
        {
            base.Start();
             SetRectMask2D(UIConstant.SCREEN, WindowContainerType.Screen);
             SetRectMask2D(UIConstant.MODAL, WindowContainerType.Modal);
             SetRectMask2D(UIConstant.ACTIVITY, WindowContainerType.Activity);
             onInitializeDone?.Invoke();
        }

        protected override void OnDisable()
        {
            SpriteAtlasManager.atlasRequested -= RequestAtlas;
        }

        void RequestAtlas(string tag, Action<SpriteAtlas> callback)
        {
            // var sa = Singleton.Of<LoadResourceService>().LoadAsset<SpriteAtlas>($"spriteatlas-{tag}");
            // callback(sa);
        }

        private void SetRectMask2D(string layerName, WindowContainerType layerType)
        {
            RectMask2D rectMask2D = null;
            switch (layerType)
            {
                case WindowContainerType.Modal:
                    rectMask2D = ModalContainer.Find(layerName).GetComponent<RectMask2D>();
                    break;
                case WindowContainerType.Screen:
                    rectMask2D = ScreenContainer.Find(layerName).GetComponent<RectMask2D>();
                    break;
                case WindowContainerType.Activity:
                    rectMask2D = ActivityContainer.Find(layerName).GetComponent<RectMask2D>();
                    break;
            }

            if (rectMask2D != null)
                rectMask2D.padding = new Vector4(PADDING_RECT_MASK, PADDING_RECT_MASK, PADDING_RECT_MASK, PADDING_RECT_MASK);
        }
    }
}