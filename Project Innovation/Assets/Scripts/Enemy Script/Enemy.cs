using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    [HideInInspector]public bool inCombat = false;

    private MonsterCombatSounds sounds;
    private bool isDying;
    private void Start()
    {
        sounds = GetComponent<MonsterCombatSounds>();
    }

    private void Update()
    {
        if (hp <= 0 && !isDying)
        {
            isDying = true;
            sounds.playSound(2);
        }
    }
}
