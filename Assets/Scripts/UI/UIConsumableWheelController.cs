using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIConsumableWheelController : MonoBehaviour
{
    [SerializeField] private Button rockButton;
    [SerializeField] private Button fragButton;
    [SerializeField] private Button flareButton;
    [SerializeField] private Button medkitButton;
    [SerializeField] private Button flashButton;

    // Start is called before the first frame update
    void Start()
    {
        rockButton.onClick.AddListener(RockButtonClicked);
        fragButton.onClick.AddListener(FragButtonClicked);
        flareButton.onClick.AddListener(FlareButtonClicked);
        medkitButton.onClick.AddListener(MedkitButtonClicked);
        flashButton.onClick.AddListener(FlashButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            rockButton.gameObject.SetActive(true);
            fragButton.gameObject.SetActive(true);
            flareButton.gameObject.SetActive(true);
            medkitButton.gameObject.SetActive(true);
            flashButton.gameObject.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            rockButton.gameObject.SetActive(false);
            fragButton.gameObject.SetActive(false);
            flareButton.gameObject.SetActive(false);
            medkitButton.gameObject.SetActive(false);
            flashButton.gameObject.SetActive(false);
        }
    }

    private void RockButtonClicked()
    {
        EventManager.TriggerPlayerChangedConsumable("Rock");
    }

    private void FragButtonClicked()
    {
        EventManager.TriggerPlayerChangedConsumable("Frag Grenade");
    }

    private void FlareButtonClicked()
    {
        EventManager.TriggerPlayerChangedConsumable("Flare");
    }

    private void MedkitButtonClicked()
    {
        EventManager.TriggerPlayerChangedConsumable("Medkit");
    }

    private void FlashButtonClicked()
    {
        EventManager.TriggerPlayerChangedConsumable("Flashbang");
    }
}
