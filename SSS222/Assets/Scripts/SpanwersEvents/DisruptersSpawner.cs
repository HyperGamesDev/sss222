﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisruptersSpawner : MonoBehaviour{
    //[SerializeField] int[] waveConfigsWeights;
    //[SerializeField] int startingWave = 0;
    [HeaderAttribute("Mecha Leech")]
    public bool spawnLeech=true;
    [SerializeField] WaveConfig cfgLeech;
    [SerializeField] float mSTimeSpawnsLeech = 55f;
    [SerializeField] float mETimeSpawnsLeech = 80f;
    public float timeSpawnsLeech = 0f;
    [HeaderAttribute("Homing Laser")]
    public bool spawnHlaser=true;
    [SerializeField] WaveConfig cfgHlaser;
    [SerializeField] float mSTimeSpawnsHlaser = 30f;
    [SerializeField] float mETimeSpawnsHlaser = 60f;
    public float timeSpawnsHlaser = 0f;
    [HeaderAttribute("Goblin Thief")]
    public bool spawnGoblin=true;
    [SerializeField] WaveConfig cfgGoblin;
    [SerializeField] int mPowerupsGoblin=2;
    [SerializeField] public int powerupsGoblin;
    [SerializeField] float mSTimeSpawnsGoblin = 40f;
    [SerializeField] float mETimeSpawnsGoblin = 50f;
    public float timeSpawnsGoblin = 0f;
    [HeaderAttribute("Healing Drone")]
    public bool spawnHealDrone=true;
    [SerializeField] WaveConfig cfgHealDrone;
    [SerializeField] int mEnemiesCountHealDrone = 50;
    public int EnemiesCountHealDrone = 0;
    [SerializeField] float mSTimeSpawnsHealDrone = 40f;
    [SerializeField] float mETimeSpawnsHealDrone = 50f;
    public float timeSpawnsHealDrone = 0f;
    [HeaderAttribute("Vortex Wheel")]
    public bool spawnVortexWheel=true;
    [SerializeField] WaveConfig cfgVortexWheel;
    [SerializeField] float mEnergyCountVortexWheel = 200;
    public float EnergyCountVortexWheel = 0;
    [SerializeField] float mSTimeSpawnsVortexWheel = 40f;
    [SerializeField] float mETimeSpawnsVortexWheel = 50f;
    public float timeSpawnsVortexWheel = 0f;
    [HeaderAttribute("Glare Devil")]
    public bool spawnGlareDevil=true;
    [SerializeField] WaveConfig cfgGlareDevil;
    [SerializeField] float mEnergyCountGlareDevil = 20;
    public float EnergyCountGlareDevil = 0;
    [SerializeField] float mSTimeSpawnsGlareDevil = 40f;
    [SerializeField] float mETimeSpawnsGlareDevil = 50f;
    public float timeSpawnsGlareDevil = 0f;
    //public int waveIndex = 0;
    //WaveConfig currentWave;
    bool looping = true;
    //[SerializeField] bool progressiveWaves = false;

    //WaveDisplay waveDisplay;
    GameSession gameSession;
    Player player;

    private void Awake() {
        StartCoroutine(SetValues());
    }
    IEnumerator SetValues(){
        //Set values
        yield return new WaitForSeconds(0.1f);
        var i=GameRules.instance;
        if(i!=null){
            spawnLeech=i.spawnLeech;
            mSTimeSpawnsLeech=i.mSTimeSpawnsLeech;
            mETimeSpawnsLeech=i.mETimeSpawnsLeech;
            spawnHlaser=i.spawnHlaser;
            mSTimeSpawnsHlaser=i.mSTimeSpawnsHlaser;
            mETimeSpawnsHlaser=i.mETimeSpawnsHlaser;
            spawnGoblin=i.spawnGoblin;
            mPowerupsGoblin=i.mPowerupsGoblin;
            mSTimeSpawnsGoblin=i.mSTimeSpawnsGoblin;
            mETimeSpawnsGoblin=i.mETimeSpawnsGoblin;
            spawnHealDrone=i.spawnHealDrone;
            mEnemiesCountHealDrone=i.mEnemiesCountHealDrone;
            mSTimeSpawnsHealDrone=i.mSTimeSpawnsHealDrone;
            mETimeSpawnsHealDrone=i.mETimeSpawnsHealDrone;
            spawnVortexWheel=i.spawnVortexWheel;
            mEnergyCountVortexWheel=i.mEnergyCountVortexWheel;
            EnergyCountVortexWheel=i.EnergyCountVortexWheel;
            mSTimeSpawnsVortexWheel=i.mSTimeSpawnsVortexWheel;
            mETimeSpawnsVortexWheel=i.mETimeSpawnsVortexWheel;
            spawnGlareDevil=i.spawnGlareDevil;
            mEnergyCountGlareDevil=i.mEnergyCountGlareDevil;
            mSTimeSpawnsGlareDevil=i.mSTimeSpawnsGlareDevil;
            mETimeSpawnsGlareDevil=i.mETimeSpawnsGlareDevil;
        }
    }
    IEnumerator Start(){
        //waveDisplay = FindObjectOfType<WaveDisplay>();
        gameSession = FindObjectOfType<GameSession>();
        player = FindObjectOfType<Player>();
        if(spawnLeech==true)timeSpawnsLeech = Random.Range(mSTimeSpawnsLeech,mETimeSpawnsLeech);
        if(spawnHlaser==true)timeSpawnsHlaser = Random.Range(mSTimeSpawnsHlaser, mETimeSpawnsHlaser);
        if(spawnGoblin==true)timeSpawnsGoblin = Random.Range(mSTimeSpawnsGoblin, mETimeSpawnsGoblin);
        if(spawnHealDrone==true)if(mEnemiesCountHealDrone==-1)timeSpawnsHealDrone = Random.Range(mSTimeSpawnsHealDrone, mETimeSpawnsHealDrone);
        if(spawnVortexWheel==true)if(EnergyCountVortexWheel==-1)timeSpawnsVortexWheel = Random.Range(mSTimeSpawnsVortexWheel, mETimeSpawnsVortexWheel);
        if(spawnGlareDevil==true)if(EnergyCountGlareDevil==-1)timeSpawnsGlareDevil = Random.Range(mSTimeSpawnsGlareDevil, mETimeSpawnsGlareDevil);
        do{
            yield return StartCoroutine(SpawnWaves());
        }
        while (looping);
    }

    public IEnumerator SpawnWaves()
    {
        if(spawnLeech==true){
        if(timeSpawnsLeech<=0&&timeSpawnsLeech>-4&&FindObjectOfType<Player>()!=null){
            //currentWave = cfgLeech;
            yield return StartCoroutine(SpawnAllEnemiesInWave(cfgLeech));
            timeSpawnsLeech=-4;
        }
        //if (progressiveWaves == true){if (waveIndex<waveConfigs.Count){ waveIndex++; } }
        //else{if(gameSession.EVscore>=50){ /*WaveRandomize();*/
        //waveIndex = Random.Range(0, waveConfigs.Count); gameSession.EVscore = 0; } }
        }
        if(spawnHlaser==true){
            if(timeSpawnsHlaser<=0&&timeSpawnsHlaser>-4){
                yield return StartCoroutine(SpawnAllEnemiesInWave(cfgHlaser));
                timeSpawnsHlaser=-4;
            }
        }
        if(spawnGoblin==true){
            if(GameObject.FindGameObjectWithTag("Powerups")!=null){
                if(timeSpawnsGoblin<=0&&timeSpawnsGoblin>-4){
                    yield return StartCoroutine(SpawnAllEnemiesInWave(cfgGoblin));
                    powerupsGoblin=0;
                    timeSpawnsGoblin=-4;
                }
            }
        }
        if(spawnHealDrone==true){
            if((EnemiesCountHealDrone>=mEnemiesCountHealDrone)||(mEnemiesCountHealDrone==0&&timeSpawnsHealDrone<=0&&timeSpawnsHealDrone>-4)){
                if(FindObjectOfType<HealingDrone>()==null){
                    yield return StartCoroutine(SpawnAllEnemiesInWave(cfgHealDrone));
                    timeSpawnsHealDrone=-4;
                    EnemiesCountHealDrone=0;
                }
            }
        }if(spawnVortexWheel==true){
            if((EnergyCountVortexWheel>=mEnergyCountVortexWheel)||(mEnergyCountVortexWheel==0&&timeSpawnsVortexWheel<=0&&timeSpawnsVortexWheel>-4)){
                if(FindObjectOfType<VortexWheel>()==null){
                    yield return StartCoroutine(SpawnAllEnemiesInWave(cfgVortexWheel));
                    timeSpawnsVortexWheel=-4;
                    EnergyCountVortexWheel=0;
                }
            }
        }if(spawnGlareDevil==true){
            if((EnergyCountGlareDevil>=mEnergyCountGlareDevil&&timeSpawnsGlareDevil<=0&&timeSpawnsGlareDevil!=-4)||(mEnergyCountGlareDevil==0&&timeSpawnsGlareDevil<=0&&timeSpawnsGlareDevil>-4)){
                if(FindObjectOfType<GlareDevil>()==null){
                    yield return StartCoroutine(SpawnAllEnemiesInWave(cfgGlareDevil));
                    timeSpawnsGlareDevil=-4;
                    EnergyCountGlareDevil=0;
                }
            }
        }
    }

    public IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        var RpathIndex = Random.Range(0, waveConfig.pathsRandom.Count);
        if (waveConfig.randomPath == false && waveConfig.between2PtsPath==false && waveConfig.shipPlace==false && waveConfig.randomPoint==false){
            for (int enCount = 0; enCount < waveConfig.GetNumberOfEnemies(); enCount++)
            {
                var newEnemy = Instantiate(
                    waveConfig.GetEnemyPrefab(),
                    waveConfig.GetWaypoints()[enCount].transform.position,
                    Quaternion.identity);
                newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
                newEnemy.GetComponent<EnemyPathing>().enemyIndex = enCount;
                yield return new WaitForSeconds(waveConfig.GetTimeSpawn());
            }
        }else if (waveConfig.randomPath == true || waveConfig.between2PtsPath == true) {
            if(waveConfig.between2PtsPath==true){
                for (int enCount = 0; enCount < waveConfig.GetNumberOfEnemies(); enCount++)
                {
                    var newEnemy = Instantiate(
                        waveConfig.GetEnemyPrefab(),
                        waveConfig.GetWaypoints()[0].transform.position,
                        Quaternion.identity);
                    newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
                   // newEnemy.GetComponent<EnemyPathing>().enemyIndex = enCount;
                    yield return new WaitForSeconds(waveConfig.GetTimeSpawn());
                }
            }
            if(waveConfig.randomPath == true){
                for (int enCount = 0; enCount < waveConfig.GetNumberOfEnemies(); enCount++)
                {
                    var newEnemy = Instantiate(
                        waveConfig.GetEnemyPrefab(),
                        waveConfig.GetWaypointsRandomPath(RpathIndex)[0].transform.position,
                        Quaternion.identity);
                    newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
                    newEnemy.GetComponent<EnemyPathing>().enemyIndex = RpathIndex;
                    yield return new WaitForSeconds(waveConfig.GetTimeSpawn());
                }
            }
        }else if(waveConfig.randomPoint==true){
            for (int enCount = 0; enCount < waveConfig.GetNumberOfEnemies(); enCount++)
            {
                var waveWaypoints = new List<Transform>();
                foreach (Transform child in waveConfig.pathsRandom[0].transform){waveWaypoints.Add(child);}
                var pointIndex = Random.Range(0, waveWaypoints.Count);
                var newEnemy = Instantiate(
                    waveConfig.GetEnemyPrefab(),
                    waveWaypoints[Random.Range(0, pointIndex)].transform.position,
                    Quaternion.identity);
                newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
                newEnemy.GetComponent<EnemyPathing>().waypointIndex = pointIndex;
                yield return new WaitForSeconds(waveConfig.GetTimeSpawn());
            }
        }else if(waveConfig.shipPlace==true){
            if(player!=null){
            for (int enCount = 0; enCount < waveConfig.GetNumberOfEnemies(); enCount++)
            {
                GameObject newEnemy=null;newEnemy = Instantiate(
                    waveConfig.GetEnemyPrefab(),
                    new Vector2(player.transform.position.x, 7.2f+waveConfig.shipYY),
                Quaternion.identity);
                if(newEnemy!=null)newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
                yield return new WaitForSeconds(waveConfig.GetTimeSpawn());
            }
            }
        }
        else { yield return new WaitForSeconds(waveConfig.GetTimeSpawn()); }
    }

    /*public void WaveRandomize()
    {
        var weights = new Dictionary<WaveConfig, int>();
        for (int index = 0; index < waveConfigs.Count; index++){
            weights.Add(waveConfigs[index], waveConfigsWeights[index]);
        }

        WaveConfig selected = WeightedRandomizer.From(weights).TakeOne(); // Strongly-typed object returned. No casting necessary.
    }*/
    //public string GetWaveName(){return currentWave.waveName;}
    void Update(){
        Mathf.Clamp(EnergyCountVortexWheel,0,mEnergyCountVortexWheel);
        if(Time.timeScale>0.0001f){
            if(spawnLeech==true){
                if(timeSpawnsLeech>-0.01f){timeSpawnsLeech -= Time.deltaTime; }
                else if(timeSpawnsLeech==-4){ timeSpawnsLeech = Random.Range(mSTimeSpawnsLeech, mETimeSpawnsLeech); }
            }
            if(spawnHlaser==true){
                if(timeSpawnsHlaser>-0.01f){ timeSpawnsHlaser -= Time.deltaTime; }
                else if(timeSpawnsHlaser==-4){ timeSpawnsHlaser = Random.Range(mSTimeSpawnsHlaser, mETimeSpawnsHlaser); }
            }
            if(spawnGoblin==true){
                if(powerupsGoblin>=mPowerupsGoblin&&timeSpawnsGoblin>=0){ timeSpawnsGoblin -= Time.deltaTime; }
                else if(timeSpawnsGoblin==-4){ timeSpawnsGoblin = Random.Range(mSTimeSpawnsGoblin, mETimeSpawnsGoblin); }
            }
            if(spawnHealDrone==true){//} && mEnemiesCountHealDrone!=0){
                if(timeSpawnsHealDrone>=0){ timeSpawnsHealDrone -= Time.deltaTime; }
                else if(timeSpawnsHealDrone==-4){ timeSpawnsHealDrone = Random.Range(mSTimeSpawnsHealDrone, mETimeSpawnsHealDrone); }
            }
            if(spawnVortexWheel==true){//} && mEnergyCountVortexWheel!=0){
                if(timeSpawnsVortexWheel>=0){ timeSpawnsVortexWheel -= Time.deltaTime; }
                else if(timeSpawnsVortexWheel==-4){ timeSpawnsVortexWheel = Random.Range(mSTimeSpawnsVortexWheel, mETimeSpawnsVortexWheel); }
            }
            if(spawnGlareDevil==true){//} && mEnergyCountGlareDevil!=0){
                if(timeSpawnsGlareDevil>=0){ timeSpawnsGlareDevil -= Time.deltaTime; }
                else if(timeSpawnsGlareDevil==-4){ timeSpawnsGlareDevil = Random.Range(mSTimeSpawnsGlareDevil, mETimeSpawnsGlareDevil); }
            }
            /*if(progressiveWaves==true){if (waveIndex >= waveConfigs.Count) { waveIndex = startingWave; } }
            else{if (gameSession.EVscore >= 50) { waveDisplay.enableText = true; waveDisplay.timer = waveDisplay.showTime;
                    timeSpawns = 0; waveIndex = Random.Range(0, waveConfigs.Count); currentWave = waveConfigs[waveIndex];
                    gameSession.EVscore = 0; } }*/
            //if (timeSpawns <= 0) {timeSpawns = mTimeSpawns; }
            //Debug.Log(timeSpawns);
        }
    }
}
