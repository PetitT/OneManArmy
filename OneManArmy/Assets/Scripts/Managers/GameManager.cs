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
    [SerializeField] CombatManager combatManager;
    MinionManager enemyManager;

    List<IUpdatable> updatables = new List<IUpdatable>();

    [SerializeField] private MeshRenderer background;

    private void Start()
    {
        inputManager = new InputManager();
        dataManager = new DataManager();
        movementManager = new MovementManager();
        movingBackground = new MovingBackground(background);
        minionSpawner = new MinionSpawner();
        enemyManager = new MinionManager();
        combatManager = new CombatManager();

        updatables.Add(inputManager);
        updatables.Add(movingBackground);
        updatables.Add(movementManager);
        updatables.Add(minionSpawner);
        updatables.Add(combatManager);

        OnPlayerDeathEvent.RegisterListener(OnPlayerDeath);
    }

    private async void OnPlayerDeath(OnPlayerDeathEvent info)
    {
        updatables.Remove(inputManager);
        updatables.Remove(movingBackground);
        updatables.Remove(minionSpawner);
        updatables.Remove(movementManager);
        updatables.Remove(combatManager);

        movementManager.Stop();

        await Task.Delay(1000);
        if (!Application.isPlaying) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        updatables.ForEach(t => t.OnUpdate());
    }
}
