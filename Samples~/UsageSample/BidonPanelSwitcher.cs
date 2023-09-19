using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

// ReSharper disable once CheckNamespace
public class BidonPanelSwitcher : MonoBehaviour
{
    [SerializeField] private List<GameObject>   panels;
    [SerializeField] private Button             nextPanelButton;
    [SerializeField] private Button             previousPanelButton;

    private void Awake()
    {
        Assert.IsNotNull(panels);
        panels.ForEach(panel => Assert.IsNotNull(panel));
        Assert.IsNotNull(nextPanelButton);
        Assert.IsNotNull(previousPanelButton);
    }

    private void Start()
    {
        panels.ForEach(panel => panel.SetActive(false));
        panels.FirstOrDefault()?.SetActive(true);
        previousPanelButton.interactable = false;
        nextPanelButton.interactable = panels.Count > 1;
    }

    public void ShowPreviousPanel()
    {
        int index = panels.IndexOf(panels.First(panel => panel.activeSelf));
        panels[index].SetActive(false);
        panels[index - 1].SetActive(true);
        nextPanelButton.interactable = true;
        if (index - 1 <= 0) previousPanelButton.interactable = false;
    }

    public void ShowNextPanel()
    {
        int index = panels.IndexOf(panels.First(panel => panel.activeSelf));
        panels[index].SetActive(false);
        panels[index + 1].SetActive(true);
        previousPanelButton.interactable = true;
        if (index + 1 >= panels.Count - 1) nextPanelButton.interactable = false;
    }
}
