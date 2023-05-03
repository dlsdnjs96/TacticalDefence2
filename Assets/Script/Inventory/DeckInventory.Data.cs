using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using TMPro;


public class HeroDeckList
{
    public HeroDeck[] deckList;

    public HeroDeckList()
    {
        deckList = new HeroDeck[1];
    }
}

public partial class DeckInventory : BaseWindow
{
    public HeroDeckList heroDeckList;
    [SerializeField] private TMP_Dropdown options;
    List<string> optionList;


    private void SaveData()
    {
        File.WriteAllText(Application.dataPath + Constant.JSON_PATH_DECK, JsonConvert.SerializeObject(heroDeckList, Formatting.Indented));
    }
    public void LoadData()
    {
        string data = File.ReadAllText(Application.dataPath + Constant.JSON_PATH_DECK);
        heroDeckList = JsonConvert.DeserializeObject<HeroDeckList>(data);
        currentDeck = heroDeckList.deckList[options.value];
    }

    void AcceptDeckListToOption()
    {
        int optionValue = options.value;

        if (optionValue >= options.options.Count)
            optionValue = options.options.Count - 1;

        options.ClearOptions();

       foreach (HeroDeck heroDeck in heroDeckList.deckList)
        {
            optionList.Add(heroDeck.name);
            currentDeck = heroDeck;
        }


        // 기존 옵션(string) 리스트 초기화
        options.ClearOptions();
        // 업데이트된 옵션(string) 리스트 새로 추가
        options.AddOptions(optionList);
        // 옵션이 변결될 시 실행될 리스너 함수 지정
        options.onValueChanged.AddListener(delegate { SetDropDown(options.value); });
        options.value = optionValue;
    }

    void SetDropDown(int _deckIndex)
    {
        // 현재 덱 변경
        currentDeck = heroDeckList.deckList[_deckIndex];
        // 덱 배치도 업데이트
        UpdateSlots();
        // 덱에 배치중인 Hero 표시
        gameManager.UpdateSelectedHeroSlots();
        // HeroInventory 업데이트
        gameManager.UpdateHeroInventoryImages();
        // 현재 덱을 필드에 적용
        gameManager.InsertDeckIntoField();
    }
}
