using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogue : MonoBehaviour
{

    [SerializeField] int lettersPerSecond;
    [SerializeField] Color highlightedColor;
    [SerializeField] Color defaultColor;

    [SerializeField] Text dialogueText;
    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject abilitySelector;
    [SerializeField] GameObject abilityDetails;

    [SerializeField] List<Text> actionTexts;
    [SerializeField] List<Text> abilityTexts;

    [SerializeField] Text mpText;
    [SerializeField] Text elementText;

    public void SetDialogue(string dialogue) {
        dialogueText.text = dialogue;
    }

    public IEnumerator TypeDialogue(string dialogue)
    {
        dialogueText.text = "";
        foreach (var letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
    }

    public void EnableDialogueText(bool enabled)
    {
        dialogueText.enabled = enabled;
    }

    public void EnableActionSelector(bool enabled)
    {
        actionSelector.SetActive(enabled);
    }

    public void EnableAbilitySelector(bool enabled)
    {
        abilitySelector.SetActive(enabled);
        abilityDetails.SetActive(enabled);
    }

    public void UpdateActionSelection(int selectedAction)
    {
        for (int i = 0; i < actionTexts.Count; i++)
        {
            if (i == selectedAction)
                actionTexts[i].color = highlightedColor;
            else
                actionTexts[i].color = defaultColor;
        }
    }

    public void UpdateAbilitySelection(int selectedability, Ability ability)
    {
        for (int i = 0; i < abilityTexts.Count; i++)
        {
            if (i == selectedability)
                abilityTexts[i].color = highlightedColor;
            else
                abilityTexts[i].color = defaultColor;
        }

        mpText.text = $"MP Cost: {ability.Base.MP}";
        elementText.text = "Element: " + ability.Base.ElementType.ToString();
    }

    public void SetAbilityNames(List<Ability> abilities)
    {
        for (int i = 0; i < abilityTexts.Count; i++)
        {
            if (i < abilities.Count)
                abilityTexts[i].text = abilities[i].Base.Name;
            else
                abilityTexts[i].text = "-";
        }
    }


}
