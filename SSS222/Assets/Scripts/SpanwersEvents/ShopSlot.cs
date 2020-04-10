﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour{
    public int ID;
    public GameObject slotText;
    public Shop shopMenu;
    Player player;
    GameSession gameSession;
    void Start()
    {
        if (player == null) player = FindObjectOfType<Player>();
        if (shopMenu == null) shopMenu = FindObjectOfType<Shop>();
        if (gameSession == null) gameSession = FindObjectOfType<GameSession>();
    }

    void Update()
    {
        if (player == null) player = FindObjectOfType<Player>();
        if (shopMenu == null) shopMenu = FindObjectOfType<Shop>();
        if (gameSession == null) gameSession = FindObjectOfType<GameSession>();
    }
    public void ResetState()
    {
        GetComponent<Image>().sprite = shopMenu.shopSlotIDs[ID].sprite;
        slotText.GetComponent<TMPro.TextMeshProUGUI>().text = shopMenu.shopSlotIDs[ID].price.ToString();
    }
    public void Buy()
    {
        //0 - Minigun
        //1 - Phaser
        //2 - Homing Rockets
        //3 - Laser 3
        //4 - Ultra Medkit
        //5 - Lightsaber
        if (gameSession.coins >= shopMenu.shopSlotIDs[ID].price)
        {
            gameSession.coins -= shopMenu.shopSlotIDs[ID].price;
            //if(ID<=shopMenu.shopSlotIDs.Count){
                if (ID == 0) { player.powerup = "mlaser"; }
                if (ID==1) { player.powerup = "phaser"; }
                if (ID==2) { player.powerup = "hrockets"; }
                if (ID==3) { player.powerup = "laser3"; }
                if (ID==4) { if (player.health > player.maxHP - 30) { gameSession.AddToScoreNoEV(Mathf.RoundToInt(player.maxHP-player.health)); } player.health += player.medkitUHpAmnt; }
                if (ID == 5) { player.powerup = "lsaber"; }
            //} else { Debug.LogError("Shop Slot ID out of bounds"); }
        }else{
            shopMenu.GetComponent<AudioSource>().Play();//PlayClipAtPoint(player.noEnergySFX,transform.position,100);
        }
    }
}
