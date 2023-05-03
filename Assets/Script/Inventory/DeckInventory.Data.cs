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


        // ���� �ɼ�(string) ����Ʈ �ʱ�ȭ
        options.ClearOptions();
        // ������Ʈ�� �ɼ�(string) ����Ʈ ���� �߰�
        options.AddOptions(optionList);
        // �ɼ��� ����� �� ����� ������ �Լ� ����
        options.onValueChanged.AddListener(delegate { SetDropDown(options.value); });
        options.value = optionValue;
    }

    void SetDropDown(int _deckIndex)
    {
        // ���� �� ����
        currentDeck = heroDeckList.deckList[_deckIndex];
        // �� ��ġ�� ������Ʈ
        UpdateSlots();
        // ���� ��ġ���� Hero ǥ��
        gameManager.UpdateSelectedHeroSlots();
        // HeroInventory ������Ʈ
        gameManager.UpdateHeroInventoryImages();
        // ���� ���� �ʵ忡 ����
        gameManager.InsertDeckIntoField();
    }
}
