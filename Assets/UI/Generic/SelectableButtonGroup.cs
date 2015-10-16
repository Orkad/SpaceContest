using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;

public class SelectableButtonGroup : MonoBehaviour {

    private List<Button> m_buttons = new List<Button>();

	void Start () {
        m_buttons.AddRange(GetComponentsInChildren<Button>());
        m_buttons.ForEach((Button b) => b.onClick.AddListener(() => SelectButton(b)));
    }

    public void SelectButton(Button p_Button)
    {
        m_buttons.ForEach((Button b) => b.interactable = true);
        p_Button.interactable = false;
    }
}
