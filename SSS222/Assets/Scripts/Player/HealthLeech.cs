﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLeech : MonoBehaviour{
    [SerializeField] float healAmnt=0.05f;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<Enemy>()!=null&&other.GetComponent<Tag_LivingEnemy>()!=null){
            if(other.GetComponent<Enemy>().health>=healAmnt){if(Player.instance!=null){Player.instance.Damage(healAmnt,dmgType.healSilent);}}
            //else{if(Player.instance!=null){Player.instance.Damage(other.GetComponent<Enemy>().health/4,heal);}}
        }
    }
}
