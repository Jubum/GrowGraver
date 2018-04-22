using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GraverController : MonoBehaviour{
    public Graver graver;
    public GameController gameController;
    public UIController uiController;
    public Inventory inventory;
    public MakingController makingController;

    GameObject sculptMaterialPrefab;
    GameObject sculpturePrefab;

    Animator animator;
    public GameObject sprite;

    //체력 바
    public Image steminaBar;
    public Image expBar;
    public Image workBar;

    //레벨 텍스트
    public Text levelText;

    public bool expBuff;


    //새로운 변수 추가시 DataSaver Class에도 추가 Load()에도 추가해준다. Refresh가 필요한지 확인한다.
    public Graver GetGraver()
    {
        return graver;
    }

    void RefreshDisplay()
    {
        levelText.text = graver.Level.ToString();

        UpdateProgressBar();
    }



    void Start()
    {
        FloatingTextController.Initalize();

        sculpturePrefab = Resources.Load<GameObject>("Prefabs/Sculpture/SculpturePrefabs");
        sculptMaterialPrefab = Resources.Load<GameObject>("Prefabs/Sculpture/Materials");

        //데이터 Load
        if (System.IO.File.Exists(DataSaver.filePath))
        {
            graver = DataSaver.LoadData<Graver>(DataSaver.filePath);
            RefreshDisplay();

        }
        else
        {
            graver = new Graver();
            graver.CurrentSculpture = Inventory.database.FetchSculptureItemById(0);
            graver.CurrentSculptMaterial = Inventory.database.FetchMaterialItemById(0);
            Directory.CreateDirectory(Application.persistentDataPath + "/Data");
        }

        graver.CurrentSculpture.sprite = Inventory.database.FetchSculptureItemById(graver.CurrentSculpture.Id).sprite;
        animator = sprite.GetComponent<Animator>();
        Invoke("WaitCommand", 1f);
        StartCoroutine(IncreaseHealth());
        StartCoroutine(UpdateProgressBar());
    }
    void WaitCommand()
    {
        StartCoroutine(AutoWorking());
    }

    Touch touch;
	void Update ()
    {
        
        if (uiController.currentPanel.Count < 1)
        {
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    touch = Input.GetTouch(i);
                    if (touch.phase == TouchPhase.Ended)
                        Working();
                }

            }
        }
        if (uiController.currentPanel.Count < 1)
        { if (Input.GetButtonDown("Fire1")) Working(); }


    }

    //체력바 계산을 위한 함수
    //경험치 바는 제작을 완료 했을때 업데이트

    IEnumerator UpdateProgressBar()
    {
        while (true)
        {
            steminaBar.fillAmount = graver.CurrentStemina / graver.MaxStemina;
            workBar.fillAmount = graver.CurrentWorkValue / graver.MaxWorkValue;

            expBar.fillAmount = graver.CurrentExp / graver.MaxExp;
            if (graver.CurrentExp >= graver.MaxExp) LevelUp();

            yield return new WaitForSeconds(0.02f);
        }
    }

    //터치 할 때마다 실행
    void Working()
    {
        if (graver.CurrentStemina < graver.CurrentSculpture.RequireHealth) return;
        //터치할때마다 움직이기
        animator.Play("graver@work");



        //스테미너가 단다
        graver.CurrentStemina -= graver.CurrentSculpture.RequireHealth;

        //제작품 workvalue 상승
        
        if(UnityEngine.Random.Range(0,1000) < graver.CriticalProbability)
        {
            graver.CurrentWorkValue += graver.SculptDamage * (graver.CriticalDamage / 100);
            //데미지 출력
            CriticalDamageTextOn();
        }
        else
        {
            graver.CurrentWorkValue += graver.SculptDamage;
            //데미지 출력
            DamageTextOn();
        }
        if (graver.CurrentWorkValue >= graver.MaxWorkValue) CompleteWorking();
    }
    
    //자동 생산
    IEnumerator AutoWorking()
    {
        while (true) {
            graver.CurrentStemina += graver.CurrentSculpture.RequireHealth;
            if (uiController.currentPanel.Count < 1) Working();

            yield return new WaitForSeconds(graver.AutoSpeed);
        }
    }

    //데미지 텍스트 이펙트 노출
    void DamageTextOn()
    {

        FloatingTextController.CreateFloatingText(graver.SculptDamage.ToString(), transform);

    }
    //크리티컬 데미지 텍스트 이펙트 노출
    void CriticalDamageTextOn()
    {

        FloatingTextController.CreateFloatingCriticalText(Mathf.Round(graver.SculptDamage * (graver.CriticalDamage / 100)).ToString(), transform);

    }



    IEnumerator IncreaseHealth()
    {
        while (true) { 
            if (graver.CurrentStemina < graver.MaxStemina)
                graver.CurrentStemina += graver.IncreaseStemina / 50;
            yield return new WaitForSeconds(0.001f);
        }

    }

    void SetSculptMaterial(int num)
    {
        //if (graver.CurrentMaterial != null) Destroy(graver.CurrentMaterial);
        graver.CurrentSculptMaterial = Inventory.database.FetchMaterialItemById(num);
        sculptMaterialPrefab.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(graver.CurrentSculptMaterial.Path + name);
    }

    public void SetCurrentSculpture(int num)
    {
        graver.CurrentSculpture = Inventory.database.FetchSculptureItemById(num);
        graver.MaxWorkValue = graver.CurrentSculpture.RequireWorkValue;
        graver.CurrentSculpture.Rank = makingController.makingInfo[num].Rank;
    }
    public void SetCurrentSculpture(Item item)
    {
        graver.CurrentSculpture = item;
        graver.MaxWorkValue = graver.CurrentSculpture.RequireWorkValue;
        graver.CurrentSculpture.Rank = makingController.makingInfo[item.Id].Rank;
    }

    //할당량 다 채웠을때
    void CompleteWorking()
    {
        FloatingTextController.CreateFloatingSclupture(graver.CurrentSculpture, transform);
        graver.CurrentWorkValue = 0;
        if(expBuff) graver.CurrentExp += graver.CurrentSculpture.Exp * 4;
        graver.CurrentExp += graver.CurrentSculpture.Exp;
        makingController.CountUp(graver.CurrentSculpture);
        inventory.AddItem(graver.CurrentSculpture);
        //MakingController 
    }

    public void OnExpBuff()
    {
        if(graver.Diamond > 0 && !expBuff) { 
            expBuff = true;
            Invoke("OffExpBuff", 30);
            graver.Diamond -= 1;
        }
    }

    public void OffExpBuff()
    {
        expBuff = false;
    }

    //레벨업 시
    void LevelUp()
    {
        graver.CurrentExp -= graver.MaxExp;
        graver.Level += 1;
        graver.MaxExp = graver.MaxExp * 2;
        levelText.text = graver.Level.ToString();
    }

}