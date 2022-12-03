using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Battle, Paused, Dialogue }

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] DialogueManager dialogueView;
    [SerializeField] NPC abel;
    [SerializeField] NPC meili;
    [SerializeField] Camera worldCamera;
    [SerializeField] AudioManager audioManager;

    GameState state;   

    // Start is called before the first frame update
    void Start()
    {
        playerController.OnEncountered += StartBattle;
        battleSystem.OnBattleEnd += EndBattle;
        playerController.OnPause += Pause;
        pauseMenu.OnResume += Resume;
        abel.OnStartDialogue += StartDialogue;
        meili.OnStartDialogue += StartDialogue;
        dialogueView.OnEndDialogue += EndDialogue;
        audioManager.PlayTownAudio();
    }

    void Pause() {
        state = GameState.Paused;
        pauseMenu.gameObject.SetActive(true);
    }

    void Resume() {
        state = GameState.FreeRoam;
        pauseMenu.gameObject.SetActive(false);
    }

    void StartBattle() {
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        var playerParty = playerController.GetComponent<Party>();
        var wildUnit = FindObjectOfType<MapArea>().GetComponent<MapArea>().GetRandomWildUnit();

        audioManager.PlayBattleAudio();
        battleSystem.StartBattle(playerParty, wildUnit);
    }

    void EndBattle(bool won) {
        if (won) {
            state = GameState.FreeRoam;
            battleSystem.gameObject.SetActive(false);
            worldCamera.gameObject.SetActive(true);
            audioManager.PlayTownAudio();
        } else {
            Debug.Log("You lost. Game over");
        }
    }

    void StartDialogue() {
        state = GameState.Dialogue;
        dialogueView.gameObject.SetActive(true);
        dialogueView.LoadNode();
    }

    void EndDialogue() {
        state = GameState.FreeRoam;
        dialogueView.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.FreeRoam) {
            playerController.HandleUpdate();
        } else if (state == GameState.Battle) {
            battleSystem.HandleUpdate();
        } else if (state == GameState.Paused) {
            pauseMenu.HandleUpdate();
        } else if (state == GameState.Dialogue) {
            dialogueView.HandleUpdate();
        }
    }
}
