﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableChildOnPlayer : MonoBehaviour{
    [SerializeField] public string valueName;
    [SerializeField] public float valueReq=1;
    public float value;
    Image image;
    PlayerSkills pskills;
    void Start(){
        image=GetComponent<Image>();
        pskills=FindObjectOfType<PlayerSkills>();
        //valueReq=
    }

    void Update(){
        value=(float)pskills.GetType().GetField(valueName).GetValue(pskills);
        if(value>=valueReq){
            SetActiveAllChildren(transform,true);
        }else{SetActiveAllChildren(transform,false);}
    }

    void SetActiveAllChildren(Transform transform, bool value){
        foreach (Transform child in transform){
            child.gameObject.SetActive(value);
            SetActiveAllChildren(child, value);
        }
    }
}
