using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    InputManager inputManager;
    DataManager dataManager;
    MovementManager movementManager;
    MovingBackground movingBackground;
    MinionSpawner minionSpawner;
   [SerializeField] CombatManager combatManager;
    EnemyManager enemyManager;

    List<IUpdatable> updatables = new List<IUpdatable>();

    [SerializeField] private MeshRenderer background;

    private void Start()
    {
        inputManager = new InputManager();
        dataManager = new DataManager();
        movementManager = new MovementManager();
        movingBackground = new MovingBackground(background);
        minionSpawner = new MinionSpawner();
        enemyManager = new EnemyManager();
        combatManager = new CombatManager();

        updatables.Add(inputManager);
        updatables.Add(movingBackground);
        updatables.Add(movementManager);
        updatables.Add(minionSpawner);
        updatables.Add(combatManager);
    }

    private void Update()
    {
        updatables.ForEach(t => t.OnUpdate());
    }
}
