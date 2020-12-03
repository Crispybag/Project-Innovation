using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEnemy : MonoBehaviour
{
    public int hp;
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
                if (GetComponent<DetermineEnemyMovePhaseFaweedEditon>())
                {
                    DetermineEnemyMovePhaseFaweedEditon phase = obj.GetComponent<DetermineEnemyMovePhaseFaweedEditon>();
                    phase.moveTrail.enabled = true;
                }
            }
            
            Player.keys++;
            isDying = true;
            sounds.playSound(2);
        }
    }
}
