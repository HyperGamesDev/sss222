﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon Config")]
public class WeaponProperties:ScriptableObject{
    [SerializeField] bool validate;
    [SerializeField] public string name;
    [SerializeField] public string assetName;
    [SerializeField] public weaponType weaponType;
    [SerializeReference] public weaponTypeProperties weaponTypeProperties;
    [SerializeField] public costType costType;
    [SerializeField] public float cost;
    [SerializeField] public float ovheat;
    [SerializeField] public float ammoSize;
    void OnValidate(){
        if(validate){
        if(weaponType==weaponType.bullet){weaponTypeProperties=new weaponTypeBullet();}
        if(weaponType==weaponType.held){weaponTypeProperties=new weaponTypeHeld();}
        validate=false;
        }
    }
}
public enum costType{energy,ammo}
public enum weaponType{bullet,held}
[System.Serializable]public class weaponTypeProperties{}
[System.Serializable]public class weaponTypeBullet:weaponTypeProperties{
    public bool leftSide=true;
    public Vector2 leftAnchor=new Vector2(-0.35f,0);
    public bool rightSide=true;
    public Vector2 rightAnchor=new Vector2(0.35f,0);
    public bool randomSide=false;
    public int bulletAmount=1;
    public Vector2 speed=new Vector2(0,9);
    public Vector2 serialOffsetSpeed=new Vector2(0.55f,0);
    public Vector2 serialOffsetAngle=new Vector2(0,5);
    public float serialOffsetSound=0.03f;
    public float shootDelay=0.34f;
    public float holdDelayMulti=0.65f;
    public float tapDelayMulti=1;
    public bool flare=true;
    public float flareDur=0.3f;
}
[System.Serializable]public class weaponTypeHeld:weaponTypeProperties{
    [SerializeField] public string nameActive;
    [SerializeField] public Vector2 offset=new Vector2(0,1);
}