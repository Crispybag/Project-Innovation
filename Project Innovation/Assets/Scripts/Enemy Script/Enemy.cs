using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public bool hasKey = false;
    [HideInInspector]public bool inCombat = false;

    private MonsterCombatSounds sounds;
    private GameObject[] enemies;
    [HideInInspector] public bool isDying;

    //public static bool isPaused = false;
    private void Start()
    {
        sounds = GetComponent<MonsterCombatSounds>();
    }

    private void Update()
    {
        if (hp <= 0 && !isDying)
        {

            DetermineEnemyMovePhaseFaweedEditon faweed = GetComponent<DetermineEnemyMovePhaseFaweedEditon>();
            foreach (var obj in faweed._enemies)
            {
                if (obj != null)
                {
                    if (GetComponent<DetermineEnemyMovePhaseFaweedEditon>())
                    {
                        DetermineEnemyMovePhaseFaweedEditon phase = obj.GetComponent<DetermineEnemyMovePhaseFaweedEditon>();
                        if (phase != null) phase.moveTrail.enabled = true;
                    }
                }
            }

            if (hasKey)Player.keys++;
            isDying = true;
            sounds.playSound(2);
        }
    }
}
