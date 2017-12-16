using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigateMenuItems : MonoBehaviour {
    
    private Text _mainText;
    private Color _defaultFocusedButtonColor = new Color(0.5f, 0.5f, 0.5f, 1);
    private Button _doojeeDesignButton;
    private Button _customizeCharacterButton;
    private Button _materialsButton;
    private GameObject _doojeeDesignClassPanel;
    private GameObject _doojeeDesignFlightSuitPanel;
    

    private string _buttonPressed;
    private bool _doojeeDesign;
    private bool _customizeCharacter;
    private bool _materials;

    public bool _doojeeDesignSelected { get { return _doojeeDesign; }}
    public bool _customizeCharacterSelected { get { return _customizeCharacter; } }
    public bool _materialSelected { get { return _materials; } }

    private void Start()
    {
        //Main text
        _mainText = GameObject.Find("MainText").GetComponent<Text>();

        //Menu Buttons
        _doojeeDesignButton = GameObject.Find("DoojeeDesign").GetComponent<Button>();
        _customizeCharacterButton = GameObject.Find("CustomizeCharacter").GetComponent<Button>();
        _materialsButton = GameObject.Find("Materials").GetComponent<Button>();

        //Panels and other menu components
        _doojeeDesignClassPanel = GameObject.Find("DoojeeDesignClassPanel");
        _doojeeDesignFlightSuitPanel = GameObject.Find("DoojeeDesignSuitPanel");
    }

    public void MenuChanges()
    {
       
        _buttonPressed = gameObject.name;
        ChangeFocusedMenuAppearance();
        if (_buttonPressed != null)
        {
            if(_buttonPressed == "CustomizeCharacter")
            {
                _customizeCharacter = true;
                _doojeeDesign = false;
                _materials = false;

                _mainText.text = "Customize Doojee!";

                EnableCustomizeCharacterMenu();
                DisableDoojeeDesignMenu();
                DisableMaterialsMenu();
            }
            else if(_buttonPressed == "Materials")
            {
                _materials = true;
                _customizeCharacter = false;
                _doojeeDesign = false;

                _mainText.text = "Materials";

                EnableMaterialsMenu();
                DisableCustomizeCharacterMenu();
                DisableDoojeeDesignMenu();
            }
            else if(_buttonPressed == "DoojeeDesign")
            {
                _doojeeDesign = true;
                _materials = false;
                _customizeCharacter = false;

                _mainText.text = "Doojee Design!";
                EnableDoojeeDesign();
                DisableMaterialsMenu();
                DisableCustomizeCharacterMenu();
            }
        }
    }

    private void ChangeFocusedMenuAppearance()
    {
        Button currentFocusedMenuButton = gameObject.GetComponent<Button>();
        if (currentFocusedMenuButton != null)
        {
            ColorBlock doojeeDesignButtonColors = currentFocusedMenuButton.colors;
            if (doojeeDesignButtonColors != null)
            {
                doojeeDesignButtonColors.normalColor = _defaultFocusedButtonColor;
            }
        }
    }

    private void EnableCustomizeCharacterMenu()
    {

    }

    private void EnableMaterialsMenu()
    {

    }

    private void EnableDoojeeDesign()
    {
        _doojeeDesignClassPanel.SetActive(true);
        _doojeeDesignFlightSuitPanel.SetActive(true);
    }

    private void DisableCustomizeCharacterMenu()
    {

    }

    private void DisableMaterialsMenu()
    {

    }

    private void DisableDoojeeDesignMenu()
    {
        _doojeeDesignClassPanel.SetActive(false);
        _doojeeDesignFlightSuitPanel.SetActive(false);
    }

}
