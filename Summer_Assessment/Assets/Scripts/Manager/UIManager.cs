using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    bool isStop;    //判断是否已经暂停
    bool isOver;    //game over界面是否已经呼出
    GameObject stopMenu; //暂停界面
    public Text money;  //金钱数
    public GameObject gameOverMenu; //游戏结束时界面
    public Image playerHealth;  //玩家生命条
    public Slider bossHealth;   //boss生命条
    public Button reStartBtn;   //重新开始按钮

    public Button returnStartMenu;   //返回主菜单

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        GameManager.Instance.ResetGameAction += SetGameOverMenu;
        reStartBtn.onClick.AddListener(SceneController.Instance.TransitionToLoadGame);
        reStartBtn.onClick.AddListener(SetGameOverMenu);
        returnStartMenu.onClick.AddListener(SceneController.Instance.TransitionToStartMenu);
        returnStartMenu.onClick.AddListener(SetStopMenu);
        if(GameManager.Instance.player != null)
        {
            money.text = GameManager.Instance.player.characterStats.Money.ToString();
        }
    }

    void Update()
    {
        if(stopMenu != null)
            isStop = stopMenu.gameObject.activeSelf;
        
        if(gameOverMenu != null)
            isOver = gameOverMenu.gameObject.activeSelf;
    }

    public void RegisterMainMenu(GameObject mainMenu)
    {
        stopMenu = mainMenu;
    }

    public void SetMainMenu()
    {
        if(!isStop)
        {
            stopMenu.SetActive(!isStop);
            Time.timeScale = 0f;
        }
        else
        {
            stopMenu.GetComponent<MainMenu>()?.CloseMainMenu();
            Time.timeScale = 1f;
        }   
    }

    public void UpdatePlayerHealthBar(float currentHealth, float maxHealth) //更新玩家的生命值
    {
        playerHealth.fillAmount = currentHealth/maxHealth;
    }
    public void UpdateMoney()
    {
        money.text = GameManager.Instance.player.characterStats.Money.ToString();
    }

    public void SetBossHealth(float currentHealth, float maxHealth) //boss出现，设置其生命值
    {
        bossHealth.gameObject.SetActive(true);
        bossHealth.maxValue = maxHealth;
        bossHealth.value = currentHealth;
    }
    
    public void UpdateBossHealthBar(float currentHealth) //更新Bosss的生命值
    {
        bossHealth.value = currentHealth;
    }

    public void CloseBossHealth()   //boss死亡，关闭生命条
    {
        bossHealth.gameObject.SetActive(false);
    }

    public void SetGameOverMenu()
    {
        if(!isOver)
        {
            gameOverMenu.gameObject.SetActive(true);
        }
        else
        {
            gameOverMenu.gameObject.SetActive(false);
        }
    }

    public void SetStopMenu()
    {
        Time.timeScale = 1f;
        stopMenu.gameObject.SetActive(!stopMenu.gameObject.activeSelf);
    }
}
