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
    [SerializeField] private Text selectedItem;

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
            selectedItem.gameObject.SetActive(true);

            EventManager.TriggerMouseShouldHide(false);

            Time.timeScale = 0.3f;
        }

        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            rockButton.gameObject.SetActive(false);
            fragButton.gameObject.SetActive(false);
            flareButton.gameObject.SetActive(false);
            medkitButton.gameObject.SetActive(false);
            flashButton.gameObject.SetActive(false);
            selectedItem.gameObject.SetActive(false);

            EventManager.TriggerPlayerChangedConsumable(selectedItem.text);
            EventManager.TriggerMouseShouldHide(true);

            Time.timeScale = 1f;
        }
    }

    public void RockButtonHover()
    {
        selectedItem.text = "Rock";
    }

    public void FragButtonHover()
    {
        selectedItem.text = "Frag Grenade";
    }

    public void FlareButtonHover()
    {
        selectedItem.text = "Flare";
    }

    public void MedkitButtonHover()
    {
        selectedItem.text = "Medkit";
    }

    public void FlashButtonHover()
    {
        selectedItem.text = "Flashbang";
    }
}
