using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPauseController : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button settingsButton;

    private void Start()
    {
        resumeButton.onClick.AddListener(ResumeButtonClicked);
        quitButton.onClick.AddListener(QuitButtonClicked);
        controlsButton.onClick.AddListener(ControlsButtonClicked);
        settingsButton.onClick.AddListener(SettingsButtonClicked);
    }

    private void SettingsButtonClicked()
    {
        EventManager.TriggerUISettingsClicked();
    }

    private void ControlsButtonClicked()
    {
        EventManager.TriggerUIControlsClicked();
    }

    private void ResumeButtonClicked()
    {
        EventManager.TriggerUIResumeClicked();
    }

    private void QuitButtonClicked()
    {
        EventManager.TriggerUIQuitClicked();
    }
}
