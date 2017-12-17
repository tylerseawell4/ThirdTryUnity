using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigateMenuItems : MonoBehaviour
{
    public Color FocusedMenuButtonColor = new Color(1f, 0.95f, 0.5f, 1);
    
    private Color _defaultFocusedButtonColor;
    private Button _doojeeDesignButton;
    private Button _customizeCharacterButton;
    private Button _materialsButton;
    private Image _doojeeDesignImage;
    private Image _customizeCharacterImage;
    private Image _materialsImage;
    private GameObject _doojeeDesignClassPanel;
    private GameObject _doojeeDesignFlightSuitPanel;
    private GameObject _customizeDoojeeMarkers;
    
    private string _buttonPressed;

    private void Start()
    {
        //Menu Buttons
        _doojeeDesignButton = GameObject.Find("DoojeeDesign").GetComponent<Button>();
        _customizeCharacterButton = GameObject.Find("CustomizeCharacter").GetComponent<Button>();
        _materialsButton = GameObject.Find("Materials").GetComponent<Button>();
        _doojeeDesignImage = GameObject.Find("DoojeeDesign").GetComponent<Image>();
        _customizeCharacterImage = GameObject.Find("CustomizeCharacter").GetComponent<Image>();
        _materialsImage = GameObject.Find("Materials").GetComponent<Image>();
        _defaultFocusedButtonColor = _materialsImage.color; //This should not be the doojee design button since during startup that changes the color in the method FocusedMenuOnDoojeeDesgin();

        //Panels and other menu components
        _doojeeDesignClassPanel = GameObject.Find("DoojeeDesignClassPanel");
        _doojeeDesignFlightSuitPanel = GameObject.Find("DoojeeDesignSuitPanel");
        _customizeDoojeeMarkers = GameObject.Find("CustomizeDoojeeMarkers");

        FocusedMenuOnDoojeeDesgin(); //this needs to be called after initializing the _doojeeDesignImage
    }
   
    public void MenuChanges()
    {
        ResetMenuButtonAppearance();
        ChangeFocusedMenuAppearance();

        _buttonPressed = gameObject.name;
        if (_buttonPressed != null)
        {
            if (_buttonPressed == "CustomizeCharacter")
            {
                EnableCustomizeCharacterMenu();
                DisableDoojeeDesignMenu();
                DisableMaterialsMenu();
            }
            else if (_buttonPressed == "Materials")
            {
                EnableMaterialsMenu();
                DisableCustomizeCharacterMenu();
                DisableDoojeeDesignMenu();
            }
            else if (_buttonPressed == "DoojeeDesign")
            {
                EnableDoojeeDesign();
                DisableMaterialsMenu();
                DisableCustomizeCharacterMenu();
            }
        }
    }

    private void FocusedMenuOnDoojeeDesgin()
    {
        if (_doojeeDesignImage != null)
        {
            _doojeeDesignImage.fillCenter = true;
            _doojeeDesignImage.color = FocusedMenuButtonColor;
        }
    }

    private void ChangeFocusedMenuAppearance()
    {
        Image buttonsImage = gameObject.GetComponent<Image>();

        if (buttonsImage != null)
        {
            buttonsImage.fillCenter = true;
            buttonsImage.color = FocusedMenuButtonColor;
        }
    }

    private void ResetMenuButtonAppearance()
    {
        if (_doojeeDesignImage != null)
        {
            _doojeeDesignImage.fillCenter = false;
            _doojeeDesignImage.color = _defaultFocusedButtonColor;
        }
        if (_customizeCharacterImage != null)
        {
            _customizeCharacterImage.fillCenter = false;
            _customizeCharacterImage.color = _defaultFocusedButtonColor;
        }
        if (_materialsImage != null)
        {
            _materialsImage.fillCenter = false;
            _materialsImage.color = _defaultFocusedButtonColor;
        }
    }

    private void EnableCustomizeCharacterMenu()
    {
        _customizeDoojeeMarkers.SetActive(true);
    }
    private void DisableCustomizeCharacterMenu()
    {
        _customizeDoojeeMarkers.SetActive(false);
    }
    private void EnableMaterialsMenu()
    {

    }
    private void DisableMaterialsMenu()
    {

    }
    private void EnableDoojeeDesign()
    {
        _doojeeDesignClassPanel.SetActive(true);
        _doojeeDesignFlightSuitPanel.SetActive(true);
    }
    private void DisableDoojeeDesignMenu()
    {
        _doojeeDesignClassPanel.SetActive(false);
        _doojeeDesignFlightSuitPanel.SetActive(false);
    }

}
