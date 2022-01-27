using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    //Text Fields
    public Text levelText, hitpointText, coinsText, upgradeCostText, xpText;

    //Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    //Character Selection
    public void OnArrowClick(bool right)
    {
        if (right) 
        {
            currentCharacterSelection++;

            //if we went too far away
            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
               currentCharacterSelection = 0;

            OnSelectionChanged();
        }

        else 
        {
            currentCharacterSelection--;

            //if we went too far away
            if (currentCharacterSelection <0)
               currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;

            OnSelectionChanged();
        }
    }
    public void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
    }

    //Weapon Upgrade
    public void OnUpgradeClick() 
    {
        if (GameManager.instance.TryUpgradeWeapon())
        {
            UpdateMenu();
        }
    }
    
    //Update the character information
    public void UpdateMenu()
    {
        //Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
           upgradeCostText.text = "Max";

        else 
           upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();

        
           

        //Meta
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        coinsText.text = GameManager.instance.coins.ToString();

        //xpBar
        int currLevel = GameManager.instance.GetCurrentLevel();
        if (currLevel == GameManager.instance.xpTable.Count) 
        {
            xpText.text = GameManager.instance.experience.ToString() + " chegou ao máximo de XP."; //display the experience points
            xpBar.localScale = Vector3.one;
        }

        else 
        {
           int prevLevelXp = GameManager.instance.GetxpToLevel(currLevel - 1);
           int currLevelXp = GameManager.instance.GetxpToLevel(currLevel);

           int diff = currLevelXp - prevLevelXp;
           int currXpIntoLevel = GameManager.instance.experience - prevLevelXp;

           float completionRatio = (float)currXpIntoLevel / (float)diff;
           xpBar.localScale = new Vector3(completionRatio, 1, 1);
           xpText.text = currXpIntoLevel.ToString() + " / " + diff;
        }

        
    }

}
