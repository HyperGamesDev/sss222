﻿using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
/*public class LootTableEntryPowerup{
    [HideInInspector]public string name;
    [SerializeField]public PowerupItem lootItem;
    //[HideInInspector]public float dropChance=0f;
}
//[System.Serializable]
public class LootTableDropPowerup{
    [HideInInspector]public string name;
    [SerializeField]public float dropChance=0f;
}*/
[System.Serializable]
public class ShopItem{
    [HideInInspector]public string name;
    public ShopSlotID item;
    public float[] dropChance=new float[GameRules.repLength+1];
    public int price=-1;
    public int priceS=1;
    public int priceE=3;
}
[System.Serializable]
public class ItemPercentageShop2{
    [HideInInspector]public string name;
    //[SerializeField]public float itemPercentage;
}
public class LootTableShop2 : MonoBehaviour{
    [SerializeField]
    public ShopItem[] itemList;
    [SerializeField]int[] reputationThresh=new int[GameRules.repLength];
    public int reputation;
    public List<float> dropList;
    private Dictionary<PowerupItem, float> itemTable;
    [SerializeField] ItemPercentageShop2[] itemsPercentage;
    //[HideInInspector] ItemPercentagePowerup[] itemsPercentage2;
    public float sum;
    
    private void Awake(){
        /*itemTable = new Dictionary<LootItem, float>();
        foreach(LootTableEntry entry in itemList){
            itemTable.Add(entry.lootItem, entry.dropChance);
        }*/
        //foreach(float dropChance in itemTable.Values){sum+=dropChance;}
        StartCoroutine(SetValues());
        SumUp();
    }
    IEnumerator SetValues(){
    yield return new WaitForSeconds(0.07f);
    var i=GameRules.instance;
    if(i!=null){
        itemList=i.shopList;
        reputationThresh=i.reputationThresh;
    }
    }
    void OnValidate(){
        /*itemTable = new Dictionary<LootItem, float>();
        foreach(LootTableEntry entry in itemList){
            itemTable.Add(entry.lootItem, entry.dropChance);
        }*/
        SumUp();
        SumUpAfter();
    }
    void Update(){
        reputation=GetComponent<Shop>().reputation;
        SumUpAfter();
    }
    public ShopSlotID GetItem(){
        float randomWeight = 0;
        do
        {
            //No weight on any number?
            if (sum == 0) return null;
            randomWeight = Random.Range(0, sum);
        } while (randomWeight == sum);
        var i=-1;
        foreach(ShopItem entry in itemList){
            i++;
            //foreach(float drop in dropList){
                if(randomWeight<dropList[i]) return entry.item;
                randomWeight-=dropList[i];
            //}
        }
        /*foreach (LootItem item in items)
        {
            if (randomWeight < item.GetComponent<LootItem>().spawnRate) return item;
            randomWeight -= item.GetComponent<LootItem>().spawnRate;
        }*/
        return null;
    }
    void SumUp(){
        //itemList=Resources.FindObjectsOfTypeAll(typeof(ShopSlotID));
        //System.Array.Resize(ref dropList, itemList.Count);
        dropList.Clear();
        //foreach(float dropChance in itemTable.Values){sum+=dropChance;};
        //itemTable = new Dictionary<PowerupItem, float>();
        //itemsPercentage = new ItemPercentage[itemList.Count];
        var i=-1;
        System.Array.Resize(ref itemsPercentage, itemList.Length);
        foreach(ShopItem entry in itemList){
            i++;
            dropList.Add(entry.dropChance[0]);
            //dropList.Add(entry.dropChance);
            //foreach(float drop in dropList){
            //itemTable.Add(entry, (float)dropList[i]);
            //entry.name=entry.lootItem.name;
            
            var value=System.Convert.ToSingle(System.Math.Round((dropList[i]/sum*100),2));
            //itemsPercentage.Add(value);
            //for(var i=0; i<itemTable.Count; i++){
                
                //itemsPercentage.Join();
                //ItemPercentage itemsPercentage= new ItemPercentage();
                //itemsPercentage[i].itemPercentage=value;
                /*string r="";
                if(entry.rarity==rarityPowerup.Legendary){}r="|L";
                if(entry.rarity==rarityPowerup.Rare){r="|R";}
                if(entry.rarity==rarityPowerup.Common){r="c";}*/
                
                
                itemsPercentage[i].name=entry.name+" - "+value+"%"+" - "+dropList[i]+"/"+(sum-dropList[i]);
                //foreach(ItemPercentage item in itemsPercentage){item.name=entry.name;item.itemPercentage=value;}
            //}
            //}
        }
        sum=dropList.Sum();
    }
    void SumUpAfter(){
        var i=-1;
        foreach(ShopItem entry in itemList){
            i++;
            //For 0
            if(reputation<reputationThresh[0]){
                dropList[i]=entry.dropChance[0];
            }
            //above 0
            for(var r=1;r<reputationThresh.Length;r++){
                if(reputation<=reputationThresh[r]&&reputation>reputationThresh[r-1]){
                    dropList[i]=entry.dropChance[r];
                }
            }
            //last
            if(reputation>reputationThresh[reputationThresh.Length-1]){
                dropList[i]=entry.dropChance[reputationThresh.Length];
            }
            //System.Array.Resize(ref itemsPercentage, itemList.Count);
            var value=System.Convert.ToSingle(System.Math.Round((dropList[i]/sum*100),2));
            itemsPercentage[i].name=entry.name+" - "+value+"%"+" - "+dropList[i]+"/"+(sum-dropList[i]);
        }
        sum=dropList.Sum();
    }
}