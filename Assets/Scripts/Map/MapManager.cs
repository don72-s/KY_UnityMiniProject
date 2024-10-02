using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class MapManager : MonoBehaviour {

    PoolManager poolManager;
    AudioPlayer audioPlayer;
    ScoreContoller scoreContoller;

    [SerializeField]
    Transform maps;

    [SerializeField]
    float blockLength;
    [SerializeField]
    Transform blockParent;

    [SerializeField]
    CarSpawnPosSO spawnInfoSO;

    float removeDistance;

    List<Block> mapBlocks = new List<Block>(10);
    Transform oldestBlock;

    Vector3 startPos;
    Vector3 endPos = Vector3.zero;
    Transform slideBlock;

    Character character;

    private void Awake() {

        character = GameObject.FindWithTag("Player").GetComponent<Character>();
        audioPlayer = AudioPlayer.GetInstance();
        scoreContoller = GetComponent<ScoreContoller>();

    }

    //초기 블록 생성.
    private void Start() {

        poolManager = PoolManager.GetInstance();

        removeDistance = blockLength * 1.5f;

        CreateEmptyFlatTile();
        CreateEmptyFlatTile();

        while (mapBlocks.Count < maxBlockCnt) {

            CreateTile();

        }

        curSpeed = speed = startSpeed;

        character.characterDamaged += () => { pauseFlag = 0; };
        character.characterRecovered += () => { pauseFlag = 1; };
        GameManager.GetInstance().GameStartEvent += () => { pauseFlag = 1; };
        GameManager.GetInstance().GameResetEvent += ResetGame;


    }

    private void FixedUpdate() {

        foreach (var block in mapBlocks) {

            block.transform.Translate(pauseFlag * curSpeed * Time.deltaTime * Vector3.back, Space.World);

        }

    }

    bool is3DMode = true;
    int createCnt = 0;
    int curModeTileCreateCnt = 7;
    bool isMaking3DBlock = true;
    int maxBlockCnt = 15;

    [SerializeField]
    int startSpeed;
    int speed;
    int curSpeed;
    int pauseFlag = 0;

    [Header("SFX")]
    [SerializeField]
    AudioClip coundDownSndClip;


    WaitForSeconds ret = new WaitForSeconds(0.4f);
    IEnumerator ChangeCountCoroutine() {
        
        audioPlayer.PlaySFX(coundDownSndClip, 1f);
        yield return ret;
        audioPlayer.PlaySFX(coundDownSndClip, 1f);
        yield return ret;
        audioPlayer.PlaySFX(coundDownSndClip, 1f);
        yield return ret;
        GameManager.GetInstance().ChangeViewMode();

        if (is3DMode) {
            speed = Mathf.Clamp(speed + 5, 0, 50);
            curSpeed = speed;
            ret = new WaitForSeconds(0.4f - (curSpeed - 30) / 5 * 0.04f);
        } else { 
            curSpeed = 18 + (curSpeed - 30) / 5 * 3;
        }


        foreach (var block in mapBlocks) {

            block.SetRightObjectVisivle(is3DMode);

        }

    }

    private void Update() {


        if (oldestBlock.transform.position.z <= -removeDistance) {

            scoreContoller.AddScore();
            RemoveOldestTile();
            
            oldestBlock = mapBlocks[0].transform;
            if (mapBlocks[1].GetType() == typeof(SlideBlock)) {

                slideBlock = mapBlocks[1].transform;
                startPos = endPos;
                endPos = startPos + new Vector3(0, mapBlocks[1].heightOffset, 0);


                Vector3 offset = new Vector3(0, 9, 0);
                character.SetGravityVec(mapBlocks[1].heightOffset > 0 ? offset : -offset);

            } else if (mapBlocks[1].GetType() == typeof(SwapBlock)) {

                is3DMode = !is3DMode;
                StartCoroutine(ChangeCountCoroutine());

                startPos = endPos;
                maps.position = startPos;
                character.SetGravityVec(Vector3.zero);

            } else {

                startPos = endPos;
                maps.position = startPos;
                character.SetGravityVec(Vector3.zero);

            }

            if (mapBlocks.Count >= maxBlockCnt) {
                
                return;
            }


            CreateTile();

        }

        if (startPos != endPos) {

            float val = (-slideBlock.position.z + (blockLength) / 2) / blockLength;
            maps.position = Vector3.LerpUnclamped(startPos, endPos, val);

        }


    }


    void CreateTile() {

        if (createCnt < curModeTileCreateCnt) {

            CreateRandomTile();
            createCnt++;

        } else {

            CreateSwapTile();
            CreateEmptyFlatTile();
            createCnt = 0;
            curModeTileCreateCnt = isMaking3DBlock ? 4 : 7;//모드별 생성 타일 수
            isMaking3DBlock = !isMaking3DBlock;

        }

    }

    void ResetGame() {

        ClearAllTile();
        
        startPos = endPos = maps.position = Vector3.zero;

        isMaking3DBlock = true;
        is3DMode = true;
        curModeTileCreateCnt = 7;
        createCnt = 0;

        CreateEmptyFlatTile();
        CreateEmptyFlatTile();

        while (mapBlocks.Count < maxBlockCnt) {

            CreateTile();

        }

        curSpeed = speed = startSpeed;
        ret = new WaitForSeconds(0.4f);

        scoreContoller.ResetScore();

    }

    void ClearAllTile() {

        foreach (Block _block in mapBlocks) {

            _block.RemoveBlock();

        }
        mapBlocks.Clear();

    }

    Block CreateEmptyFlatTile() {

        Block block = poolManager.GetObj<Block>();
        CreateTile(block);
        return block;

    }
    void CreateSwapTile() { 
    
        Block block = poolManager.GetObj<SwapBlock>();
        CreateTile(block);

    }
    void CreateTile(Block _block) {
       
        if (mapBlocks.Count == 0) {

            mapBlocks.Add(_block);
            mapBlocks[0].transform.SetParent(blockParent);
            mapBlocks[0].transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            oldestBlock = mapBlocks[0].transform;

        } else {

            _block.transform.SetPositionAndRotation(mapBlocks[mapBlocks.Count - 1].transform.position + Vector3.forward * blockLength + mapBlocks[mapBlocks.Count - 1].nextTileOffset, Quaternion.identity);
            _block.transform.SetParent(blockParent);
            mapBlocks.Add(_block);

        }

        _block.SetRightObjectVisivle();

    }


    /// <summary>
    /// 가장 뒤에 랜덤으로 블록 하나 추가.
    /// </summary>
    void CreateRandomTile() {

        int v = UnityEngine.Random.Range(0, 3);
        Block block = null;

        if (v == 0) block = poolManager.GetObj<Block>();
        else if (v == 1) block = poolManager.GetObj<SlideBlock>("Up");
        else if (v == 2) block = poolManager.GetObj<SlideBlock>("Down");

        CreateTile(block);
        block.SettingBlock(spawnInfoSO, isMaking3DBlock);

    }

    /// <summary>
    /// 가장 오래된 타일 제거
    /// </summary>
    void RemoveOldestTile() {

        Block blockObj = mapBlocks[0];
        mapBlocks.RemoveAt(0);

        blockObj.RemoveBlock();

    }

}
