using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class ItemClick : MonoBehaviour
{
    [SerializeField]
    private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();

    private void OnMouseDown()
    {
        m_OnClick?.Invoke();
    }
}
