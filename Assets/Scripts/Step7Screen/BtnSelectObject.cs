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

        private void OnClickedObj()
        {
            step7.AddStep();
            step7.stepFillColor.InitData(objectSelect);
            step7.stepFillColor.gameObject.SetActive(true);
            imageIcon.sprite = spriteDone;
            btnSelect.onClick.RemoveAllListeners();
        }
    }
}