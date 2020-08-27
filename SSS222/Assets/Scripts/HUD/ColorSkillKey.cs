﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSkillKey : MonoBehaviour{
    [SerializeField] public int ID;
    [SerializeField] public skillKeyBind key;
    Player player;
    Skill[] skills;
    skillKeyBind[] skillsBinds;
    Color color;
    Image spr;
    TMPro.TextMeshProUGUI txt;
    public bool on;
    void Start(){
        player=FindObjectOfType<Player>();
        StartCoroutine(SetSkills());
    }
    IEnumerator SetSkills(){
        yield return new WaitForSeconds(0.07f);
        skills=FindObjectOfType<PlayerSkills>().skills;
        skillsBinds=FindObjectOfType<PlayerSkills>().skillsBinds;
        spr=GetComponent<Image>();
        txt=GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    void Update(){
        if(skillsBinds[ID]==key){color=Color.green;on=true;}
        else{color=Color.white;on=false;}
        spr.color=color;
        txt.color=color;
    }
}
