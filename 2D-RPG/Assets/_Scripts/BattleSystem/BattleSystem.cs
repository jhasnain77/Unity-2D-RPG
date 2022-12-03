using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy }

public class BattleSystem : MonoBehaviour
{

    [SerializeField] Unit playerUnit;
    [SerializeField] BattleHUD playerHUD;

    [SerializeField] Unit enemyUnit;
    [SerializeField] BattleHUD enemyHUD;

    [SerializeField] BattleDialogue dialogueBox;

    public event Action<bool> OnBattleEnd;

    BattleState state;

    int currentAction;
    int currentAbility;

    Party playerParty;
    BattleUnit wildEnemy;

    public void StartBattle(Party playerParty, BattleUnit wildEnemy)
    {
        this.playerParty = playerParty;
        this.wildEnemy = wildEnemy;
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle()
    {
        playerUnit.Setup(playerParty.GetHealthyUnits());
        enemyUnit.Setup(wildEnemy);
        playerHUD.SetData(playerUnit.BattleUnit);
        enemyHUD.SetData(enemyUnit.BattleUnit);

        dialogueBox.SetAbilityNames(playerUnit.BattleUnit.Abilities);

        yield return dialogueBox.TypeDialogue($"A wild {enemyUnit.BattleUnit.Base.Name} appeared!");
        yield return new WaitForSeconds(1f);

        PlayerAction();
    }

    void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogueBox.TypeDialogue("Choose an action..."));
        dialogueBox.EnableActionSelector(true);
    }

    void PlayerMove()
    {
        state = BattleState.PlayerMove;
        dialogueBox.EnableActionSelector(false);
        dialogueBox.EnableDialogueText(false);
        dialogueBox.EnableAbilitySelector(true);
    }

    IEnumerator PerformPlayerMove()
    {
        state = BattleState.Busy;

        var ability = playerUnit.BattleUnit.Abilities[currentAbility];
        playerUnit.BattleUnit.CurrentMP -= ability.MP;
        yield return playerHUD.UpdateMP();
        yield return dialogueBox.TypeDialogue($"{playerUnit.BattleUnit.Base.Name} used {ability.Base.Name}!");

        playerUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);

        enemyUnit.PlayHitAnimation();

        var damageDetails = enemyUnit.BattleUnit.TakeDamage(ability, playerUnit.BattleUnit);
        yield return enemyHUD.UpdateHP();

        yield return ShowDamageDetails(damageDetails);

        if (damageDetails.Fainted)
        {
            yield return dialogueBox.TypeDialogue($"{enemyUnit.BattleUnit.Base.Name} fainted.");
            enemyUnit.PlayFaintAnimation();

            yield return new WaitForSeconds(2f);
            OnBattleEnd(true);
        }
        else
        {
            StartCoroutine(EnemyMove());
        }

    }

    IEnumerator EnemyMove()
    {
        state = BattleState.EnemyMove;

        var ability = enemyUnit.BattleUnit.GetRandomAbility();
        enemyUnit.BattleUnit.CurrentMP -= ability.MP;
        yield return enemyHUD.UpdateMP();
        yield return dialogueBox.TypeDialogue($"{enemyUnit.BattleUnit.Base.Name} used {ability.Base.Name}!");

        enemyUnit.PlayAttackAnimation();

        yield return new WaitForSeconds(1f);

        playerUnit.PlayHitAnimation();

        var damageDetails = playerUnit.BattleUnit.TakeDamage(ability, enemyUnit.BattleUnit);
        yield return playerHUD.UpdateHP();

        yield return ShowDamageDetails(damageDetails);

        if (damageDetails.Fainted)
        {
            yield return dialogueBox.TypeDialogue($"{playerUnit.BattleUnit.Base.Name} fainted.");
            //playerUnit.PlayFaintAnimation();

            yield return new WaitForSeconds(2f);

            var nextUnit = playerParty.GetHealthyUnits();
            if (nextUnit != null) {
                playerUnit.Setup(nextUnit);
                playerHUD.SetData(nextUnit);

                dialogueBox.SetAbilityNames(playerUnit.BattleUnit.Abilities);

                yield return dialogueBox.TypeDialogue($"Go {nextUnit.Base.Name}!");
                yield return new WaitForSeconds(1f);

                PlayerAction();
            } else {
                OnBattleEnd(false);
            }
            
        }
        else
        {
            PlayerAction();
        }
    }

    IEnumerator ShowDamageDetails(DamageDetails damageDetails)
    {
        if (damageDetails.Element > 1)
        {
            yield return dialogueBox.TypeDialogue("It's super effective!");
            yield return new WaitForSeconds(1f);
        } else if (damageDetails.Element < 1)
        {
            yield return dialogueBox.TypeDialogue("It's not very effective...");
            yield return new WaitForSeconds(1f);
        }

        if (damageDetails.Critical > 1f)
        {
            yield return dialogueBox.TypeDialogue("A critical hit!");
            yield return new WaitForSeconds(1f);
        }
    }

    public void HandleUpdate()
    {
        if (state == BattleState.PlayerAction)
        {
            HandleActionSelection();
        } else if (state == BattleState.PlayerMove)
        {
            HandleMoveSelection();
        }
    }

    void HandleActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentAction < 1)
                currentAction++;
        } else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentAction > 0)
                currentAction--;
        }

        dialogueBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentAction == 0)
            {
                // Fight
                PlayerMove();
            } else if (currentAction == 1)
            {
                // Run
                OnBattleEnd(true);
            }
        }
    }

    void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentAbility < playerUnit.BattleUnit.Abilities.Count - 1)
                currentAbility++;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentAbility > 0)
                currentAbility--;
        } else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentAbility < playerUnit.BattleUnit.Abilities.Count - 2)
                currentAbility += 2;
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (currentAbility > 1)
                currentAbility -= 2;
        }

        dialogueBox.UpdateAbilitySelection(currentAbility, playerUnit.BattleUnit.Abilities[currentAbility]);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            dialogueBox.EnableAbilitySelector(false);
            dialogueBox.EnableDialogueText(true);
            StartCoroutine(PerformPlayerMove());
        }
    }
    
}
