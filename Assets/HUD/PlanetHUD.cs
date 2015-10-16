using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System.Collections.Generic;
using System;

public class PlanetHUD : FadeInOut {

    [SerializeField]
    private Text m_PlanetNameText;
    [SerializeField]
    private ProgressBar m_PlanetWaterProgressBar;
    [SerializeField]
    private ProgressBar m_PlanetPopulationProgressBar;
    [SerializeField]
    private ProgressBar m_PlanetRawMaterialProgressBar;
    [SerializeField]
    private ProgressBar m_PlanetArmorProgressBar;
    [SerializeField]
    private Button m_PlanetModNoneButton;
    [SerializeField]
    private Button m_PlanetModFeedButton;
    [SerializeField]
    private Button m_PlanetModDrillButton;
    [SerializeField]
    private Button m_PlanetModBuildButton;
    [SerializeField]
    private Button m_PlanetModReserchButton;
    [SerializeField]
    private Button m_PlanetModDefenseButton;
    [SerializeField]
    private SelectableButtonGroup m_PlanetModSelectableButtonGroup;

    private Planet m_PlanetReference;

	void Start () {
        canvasGroup.alpha = 0f;
        Hide();
        SelectionManager.Instance.EventSelect += OnSelectGameElements;
        SelectionManager.Instance.EventDeselect += OnDeselectGameElements;

        //Affectation des Evenements Button
        m_PlanetModNoneButton.onClick.AddListener(() => m_PlanetReference.CmdChangePlanetMod(PlanetMod.NONE));
        m_PlanetModFeedButton.onClick.AddListener(() => m_PlanetReference.CmdChangePlanetMod(PlanetMod.FEED));
        m_PlanetModDrillButton.onClick.AddListener(() => m_PlanetReference.CmdChangePlanetMod(PlanetMod.DRILL));
        m_PlanetModBuildButton.onClick.AddListener(() => m_PlanetReference.CmdChangePlanetMod(PlanetMod.BUILD));
        m_PlanetModReserchButton.onClick.AddListener(() => m_PlanetReference.CmdChangePlanetMod(PlanetMod.RESEARCH));
        m_PlanetModDefenseButton.onClick.AddListener(() => m_PlanetReference.CmdChangePlanetMod(PlanetMod.DEFENCE));
    }

    protected override bool InterractableCondition()
    {
        //Le panel sera disponible uniquement si une planete de ref est attribuée et que c'est bien celle du joueur
        //Mais aussi si le panel est totalement affichier avec un alpha = 1
        return m_PlanetReference && m_PlanetReference.IsMine() && base.InterractableCondition();
    }

    protected override void UpdateShow()
    {
        m_PlanetWaterProgressBar.value = m_PlanetReference.water;
        m_PlanetPopulationProgressBar.value = m_PlanetReference.population;
        m_PlanetRawMaterialProgressBar.value = m_PlanetReference.rawMaterial;
        m_PlanetArmorProgressBar.value = 0; //NOT IMPLEMENTED

        //Initialisation des maximum
        m_PlanetWaterProgressBar.maxValue = m_PlanetReference.maxWater;
        m_PlanetPopulationProgressBar.maxValue = m_PlanetReference.maxPopulation;
        m_PlanetRawMaterialProgressBar.maxValue = m_PlanetReference.maxRawMaterial;
        m_PlanetArmorProgressBar.maxValue = 1; //NOT IMPLEMENTED

        //Nom de la planette
        m_PlanetNameText.text = m_PlanetReference.planetName;

        //Selection
        switch (m_PlanetReference.planetMod)
        {
            case PlanetMod.NONE:
                m_PlanetModSelectableButtonGroup.SelectButton(m_PlanetModNoneButton);
                break;
            case PlanetMod.FEED:
                m_PlanetModSelectableButtonGroup.SelectButton(m_PlanetModFeedButton);
                break;
            case PlanetMod.DRILL:
                m_PlanetModSelectableButtonGroup.SelectButton(m_PlanetModDrillButton);
                break;
            case PlanetMod.BUILD:
                m_PlanetModSelectableButtonGroup.SelectButton(m_PlanetModBuildButton);
                break;
            case PlanetMod.RESEARCH:
                m_PlanetModSelectableButtonGroup.SelectButton(m_PlanetModReserchButton);
                break;
            case PlanetMod.DEFENCE:
                m_PlanetModSelectableButtonGroup.SelectButton(m_PlanetModDefenseButton);
                break;
            default:
                break;
        }
    }

    private void OnSelectGameElements(List<GameElement> p_ListGameElement)
    {
        if(p_ListGameElement.Count == 1 && p_ListGameElement[0] is Planet)
        {
            //Mise en place de la planete de reférence
            m_PlanetReference = p_ListGameElement[0] as Planet;
            //Affichage du Panel
            Show();
        }
    }

    private void OnDeselectGameElements(List<GameElement> p_ListGameElement)
    {
        if (!m_PlanetReference)
            return;
        Hide();
    }
}
