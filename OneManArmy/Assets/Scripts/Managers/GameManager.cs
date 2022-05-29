using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    InputManager inputManager;
    DataManager dataManager;
    MovementManager movementManager;
    MovingBackground movingBackground;
    MinionSpawner minionSpawner;
    CoinManager coinManager;
    CoinSpawner coinSpawner;
    MinionManager minionManager;
    CombatManager combatManager;
    AttacksSelect attacksSelect;
    TimeManager timeManager;
    [SerializeField] CanvasManager canvasManager;
    [SerializeField] MeshRenderer background;
    [SerializeField] List<Attack> attacks;

    List<IUpdatable> updatables = new List<IUpdatable>();
    GameState currentState = GameState.pause;

    public enum GameState { playing, pause }

    private void Start()
    {
        inputManager = new InputManager();
        dataManager = new DataManager();
        movementManager = new MovementManager();
        movingBackground = new MovingBackground(background);
        minionManager = new MinionManager();
        minionSpawner = new MinionSpawner();
        coinManager = new CoinManager();
        coinSpawner = new CoinSpawner();
        combatManager = new CombatManager();
        attacksSelect = new AttacksSelect(attacks);
        timeManager = new TimeManager();

        updatables.Add(inputManager);
        updatables.Add(movingBackground);
        updatables.Add(movementManager);
        updatables.Add(minionSpawner);
        updatables.Add(coinSpawner);
        updatables.Add(coinManager);
        updatables.Add(combatManager);
        updatables.Add(minionManager);
        updatables.Add(timeManager);

        OnPlayerDeathEvent.RegisterListener(OnPlayerDeath);
        OnLevelUpEvent.RegisterListener(OnLevelUp);
        OnSpellLevelUpEvent.RegisterListener(OnSpellLevelUp);

        canvasManager.DisplayGameCanvas();

        SetState(GameState.playing);
    }

    private void OnDestroy()
    {
        OnPlayerDeathEvent.UnregisterListener(OnPlayerDeath);
        OnLevelUpEvent.UnregisterListener(OnLevelUp);
        OnSpellLevelUpEvent.UnregisterListener(OnSpellLevelUp);
    }

    private void OnLevelUp(OnLevelUpEvent info)
    {
        SetState(GameState.pause);
        canvasManager.DisplayLevelUpCanvas();
    }

    private void OnSpellLevelUp(OnSpellLevelUpEvent info)
    {
        SetState(GameState.playing);
        canvasManager.DisplayGameCanvas();
    }

    private async void OnPlayerDeath(OnPlayerDeathEvent info)
    {
        SetState(GameState.pause);

        await Task.Delay(1000);
        if (!Application.isPlaying) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        if (currentState == GameState.pause) return;

        for (int i = 0; i < updatables.Count; i++)
        {
            updatables[i].OnUpdate();
        }
    }

    private void SetState(GameState state)
    {
        currentState = state;
        switch (currentState)
        {
            case GameState.playing:
                break;
            case GameState.pause:
                movementManager.Stop();
                break;
            default:
                break;
        }
    }
}
