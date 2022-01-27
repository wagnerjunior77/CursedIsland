using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;
    private void Awake(){

        
        if (GameManager.instance != null){
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(hitpointBar.gameObject);
    }


    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;

    public Weapon weapon;

    public FloatingTextManager floatingTextManager;

    public RectTransform hitpointBar;

    public GameObject hud;
    public GameObject menu;

    public Animator DeathMenuAnim;

    // Logic
    public int coins;
    public int experience;
    
    //Floating Text

    
    public void ShowText (string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration) {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }


    //Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        // is the weapon max level?
        if (weaponPrices.Count <= weapon.weaponLevel)
        return false;

        if (coins >= weaponPrices[weapon.weaponLevel])
        {
            coins -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }
    
    private void Update()
    {
        //Debug.Log(GetCurrentLevel());
    }

    //Hitpoint Bar
    public void OnHitpointChange() 
    {
        float ratio = (float)player.hitpoint / (float)player.maxHitpoint;
        hitpointBar.localScale = new Vector3(1, ratio, 1);
    } 

    //Experience System
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while(experience >= add) 
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count) //MaxLevel
              return r;
        }

        return r;
    }
    public int GetxpToLevel(int level) 
    {
        int r = 0;
        int xp = 0;

        while (r < level) 
        {
            xp += xpTable[r];
            r++;
        }

        return xp;
    }
    public void GrantXp(int xp) 
    {
        int currLevel = GetCurrentLevel();
        experience += xp;
        if (currLevel < GetCurrentLevel())
          OnLevelUp();
    }
    
    public void OnLevelUp() 
    {
        Debug.Log("Level Up!");
        GameManager.instance.ShowText("Level Up!",40,Color.white, transform.position + new Vector3(-0.7f,-0.3f,0), Vector3.zero, 1.0f);
        player.OnLevelUp();
        OnHitpointChange();

    }
    

    //Death Menu and Respawn

    public void Respawn() 
    {
        DeathMenuAnim.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        player.Respawn();
    }


    //SaveState

    /*
    * INT skin
    * INT coins
    * INT experience
    * INT weaponLevel
    */

    public void SaveState(){
        string s = "";

        s += "0" + "|" ;
        s += coins.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);

        Debug.Log("SaveState");
    }

   public void LoadState(Scene s, LoadSceneMode mode){
        
        if(!PlayerPrefs.HasKey("SaveState")){
            return;
        }

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //Change Player Skin
        coins = int.Parse(data[1]);

        //experience
        experience = int.Parse(data[2]);
        if (GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());
        //Change the weapon level
        weapon.SetWeaponLevel(int.Parse(data[3]));

        player.transform.position = GameObject.Find("SpawnPoint").transform.position;

        Debug.Log("Load State");
    } 
}
