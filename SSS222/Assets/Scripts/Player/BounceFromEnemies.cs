﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceFromEnemies : MonoBehaviour{
    [SerializeField] public float speed=9f;
    [SerializeField] List<Enemy> enemiesHit;

    void Update(){
        float step = speed * Time.deltaTime;
        var target=FindClosestEnemy();
        if(target!=null)transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
    }
    public Enemy FindClosestEnemy(){
        KdTree<Enemy> Enemies = new KdTree<Enemy>();
        Enemy[] EnemiesArr;
        EnemiesArr = FindObjectsOfType<Enemy>();
        foreach(Enemy enemy in EnemiesArr){
            //if(enemy.GetComponent<Enemy>().cTagged==false)Enemies.Add(enemy);
            if(!enemiesHit.Contains(enemy))Enemies.Add(enemy);
        }
        Enemy closest = Enemies.FindClosest(transform.position);
        return closest;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!CompareTag(other.tag)){
            if(other.CompareTag("Enemy")){
            //other.GetComponent<Enemy>().cTagged=true;
            enemiesHit.Add(other.GetComponent<Enemy>());
            }
        }
    }
}
