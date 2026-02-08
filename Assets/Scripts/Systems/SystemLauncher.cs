using UnityEngine;
using Scripts.Systems;
using Scripts.Data;
using Scripts.NPC;
using System.Collections.Generic;

public class SystemLauncher : MonoBehaviour
{
    public static SystemLauncher Instance { get; private set; }
    public BehaviorSystem behaviorSystem;
    private List<NPCData> npcDataList = new();

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        //Launch Systems
        LaunchBehaviorSystem();

    }

    private void LaunchBehaviorSystem()
    {
        // Find all NPCs and register their data
        foreach (var npc in FindObjectsByType<NonPlayerCharacter>(FindObjectsSortMode.None))
        {
            npcDataList.Add(npc.npcData);
        }

        //behaviorSystem = new BehaviorSystem(npcDataList);
    }
}
