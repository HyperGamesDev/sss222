using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsUI;
    public float prevGameSpeed = 1f;
    IEnumerator slowDownCo;
    //Shop shop;
    IEnumerator Start(){
        yield return new WaitForSeconds(0.05f);
        Resume();
        //shop=FindObjectOfType<Shop>();
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(GameIsPaused){
                if(pauseMenuUI.activeSelf)Resume();
                if(optionsUI.transform.GetChild(0).gameObject.activeSelf){GameSession.instance.SaveSettings();GameSession.instance.CloseSettings(true);pauseMenuUI.SetActive(true);}
                if(optionsUI.transform.GetChild(1).gameObject.activeSelf){optionsUI.GetComponent<SettingsMenu>().OpenSettings();PauseEmpty();}
            }else{
                if(((Shop.instance!=null&&Shop.shopOpened!=true)||(Shop.instance==null))&&
                ((UpgradeMenu.instance!=null&&UpgradeMenu.UpgradeMenuIsOpen!=true)||(UpgradeMenu.instance==null)))Pause();
            }
        }//if(Input.GetKeyDown(KeyCode.R)){//in GameSession}
    }
    public void Resume(){
        pauseMenuUI.SetActive(false);
        if(optionsUI.transform.GetChild(0).gameObject.activeSelf){GameSession.instance.CloseSettings(false);}
        //if(optionsUI.transform.GetChild(1).gameObject.activeSelf){optionsUI.GetComponent<SettingsMenu>().OpenSettings();}
        GameObject.Find("BlurImage").GetComponent<SpriteRenderer>().enabled=false;
        GameSession.instance.gameSpeed=1;
        //StartCoroutine(SpeedUp());
        GameIsPaused = false;
        slowDownCo=null;
        //Debug.Log("Resuming pause");
    }
    public void PauseEmpty(){
        GameObject.Find("BlurImage").GetComponent<SpriteRenderer>().enabled=true;
        GameIsPaused = true;
        if(GameSession.instance.slowingPause){if(slowDownCo==null)slowDownCo=SlowDown();StartCoroutine(slowDownCo);}
        else{GameSession.instance.gameSpeed=0;}
        //Debug.Log("Pausing");
    }
    public void Pause(){
        prevGameSpeed = GameSession.instance.gameSpeed;
        pauseMenuUI.SetActive(true);
        PauseEmpty();
        //ParticleSystem.Stop();
        //var ptSystems = FindObjectOfType<ParticleSystem>();
        //foreach(ptSystem in ptSystems){ParticleSystem.Pause();}
    }
    IEnumerator SlowDown(){
        while(GameSession.instance.gameSpeed>0){
        GameSession.instance.speedChanged=true; GameSession.instance.gameSpeed -= 0.075f;
        yield return new WaitForEndOfFrame();
        }
    }IEnumerator SpeedUp(){
        while(GameSession.instance.gameSpeed<1){
        GameSession.instance.speedChanged=true; GameSession.instance.gameSpeed += 0.075f;
        yield return new WaitForEndOfFrame();
        }
    }
    public void Menu(){
        //GameSession.instance.gameSpeed = prevGameSpeed;
        GameSession.instance.gameSpeed = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void OpenOptions(){
        optionsUI.GetComponent<SettingsMenu>().OpenSettings();
        pauseMenuUI.SetActive(false);
    }
    public void PreviousGameSpeed(){GameSession.instance.gameSpeed = prevGameSpeed;}
}