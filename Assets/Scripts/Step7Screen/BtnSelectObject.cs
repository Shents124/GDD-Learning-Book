using System.Collections;
using Cysharp.Threading.Tasks;
using Step7;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Step7Screen
{
    public class BtnSelectObject : MonoBehaviour
    {
        public TypeObject objectSelect;

        [SerializeField] private Sprite spriteDone;

        [SerializeField] private Image imageIcon;

        [SerializeField] Step7Activity step7;

        [SerializeField] private Button btnSelect;

        private void Start()
        {
            btnSelect.onClick.AddListener(OnClickedObj);
        }

        private async void OnClickedObj()
        {
            step7.AddStep();
            await UIService.OpenActivityAsyncNoClose(ActivityType.Step8);
            var step8 = Object.FindObjectOfType<Step8Activity>(true);
            step8.InitData(objectSelect);
            imageIcon.sprite = spriteDone;
            btnSelect.onClick.RemoveAllListeners();
        }
    }
}