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

    private void Start()
    {
        resumeButton.onClick.AddListener(ResumeButtonClicked);
        quitButton.onClick.AddListener(QuitButtonClicked);
        controlsButton.onClick.AddListener(ControlsButtonClicked);
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
