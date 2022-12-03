using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum PauseMenuState { MenuOptions, PartyOptions, Controls }

public class PauseMenu : MonoBehaviour
{

    [SerializeField] PlayerController player;
    [SerializeField] List<PartyMemberHUD> partyHUDs;
    [SerializeField] List<Text> menuTexts;
    [SerializeField] Color highlightedColor;
    [SerializeField] Color defaultColor;
    [SerializeField] GameObject controls;

    List<BattleUnit> partyMembers;

    PauseMenuState state;

    public event Action OnResume;

    int selectedMenuOption;
    int selectedPartyMember;

    private void Awake() {

        SetupParty();

        selectedMenuOption = 0;
        selectedPartyMember = -1;

        state = PauseMenuState.MenuOptions;
        UpdateMenuSelection();
    }

    public void SetupParty() {
        partyMembers = player.GetComponent<Party>().GetAllUnits();

        for (int i = 0; i < partyMembers.Count; i++) {
            partyHUDs[i].SetData(partyMembers[i]);
            partyHUDs[i].gameObject.SetActive(true);
        }
    }

    public void HandleUpdate() {
        switch(state) {
            case PauseMenuState.MenuOptions:
                HandleMenuSelection();
                break;
            case PauseMenuState.PartyOptions:
                HandlePartySelection();
                break;
            case PauseMenuState.Controls:
                HandleControlsView();
                break;
            default:
                HandleMenuSelection();
                break;
        }
    }

    public void HandlePartySelection() {
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (selectedPartyMember < partyMembers.Count - 1) {
                selectedPartyMember++;
            } else {
                selectedPartyMember = 0;
            }
            UpdatePartySelection();
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (selectedPartyMember > 0) {
                selectedPartyMember--;
            } else {
                selectedPartyMember = partyMembers.Count - 1;
            }
            UpdatePartySelection();
        } else if (Input.GetKeyDown(KeyCode.X)) {
            selectedPartyMember = -1;
            UpdatePartySelection();
            state = PauseMenuState.MenuOptions;
        }
    }

    public void UpdatePartySelection() {
        for (int i = 0; i < partyMembers.Count; i++) {
            if (selectedPartyMember == i) {
                partyHUDs[i].GetComponent<Image>().color = highlightedColor;
            } else {
                partyHUDs[i].GetComponent<Image>().color = Color.white;
            }
        }
    }

    public void HandleMenuSelection() {
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (selectedMenuOption < menuTexts.Count - 1) {
                selectedMenuOption++;
            } else {
                selectedMenuOption = 0;
            }
            UpdateMenuSelection();
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (selectedMenuOption > 0) {
                selectedMenuOption--;
            } else {
                selectedMenuOption = menuTexts.Count - 1;
            }
            UpdateMenuSelection();
        } else if (Input.GetKeyDown(KeyCode.Z)) {
            if (selectedMenuOption == 0) {
                Resume();
            } else if (selectedMenuOption == 1) {
                selectedPartyMember = 0;
                UpdatePartySelection();
                state = PauseMenuState.PartyOptions;
            } else if (selectedMenuOption == 4) {
                controls.SetActive(true);
                state = PauseMenuState.Controls;
            } else if (selectedMenuOption == 6) {
                Application.Quit();
            }
        } else if (Input.GetKeyDown(KeyCode.Escape)) {
            Resume();
        }
    }

    public void HandleControlsView() {
        if (Input.GetKeyDown(KeyCode.X)) {
            controls.SetActive(false);
            state = PauseMenuState.MenuOptions;
        }
    }

    public void UpdateMenuSelection() {
        for (int i = 0; i < menuTexts.Count; i++) {
            if (selectedMenuOption == i) {
                menuTexts[i].color = highlightedColor;
            } else {
                menuTexts[i].color = defaultColor;
            }
        }
    }

    public void Resume() {
        OnResume();
    }

}
