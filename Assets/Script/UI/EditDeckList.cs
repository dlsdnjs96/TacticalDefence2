using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EditDeckList : BaseWindow
{
    [SerializeField]
    GameObject deckPrefab;
    [SerializeField]
    GameObject addDeckPrefab;

    GameObject addDeck;

    public GameManager gameManager;
    GameObject content;
    public HeroDeckList tempHeroDeckList { private set; get; }

    private void Awake()
    {
        content = transform.Find("Scroll View").Find("Viewport").Find("Content").gameObject;

    }



    private void OnEnable()
    {
        LoadDeckList();
        
    }

    public void LoadDeckList()
    {
        tempHeroDeckList = gameManager.GetDeckList();


        for (int i = 0; i < content.transform.childCount; i++)
            GameObject.Destroy(content.transform.GetChild(i).gameObject);


        foreach (HeroDeck heroDeck in tempHeroDeckList.deckList)
        {
            GameObject deck = Instantiate(deckPrefab, content.transform);
            deck.GetComponent<EditDeck>().heroDeck = heroDeck;
            deck.GetComponent<EditDeck>().editDeckList = this;
            deck.transform.Find("InputField").GetComponent<TMP_InputField>().text = heroDeck.name;
        }
        addDeck = Instantiate(addDeckPrefab, content.transform);
        addDeck.transform.GetComponent<Button>().onClick.AddListener(delegate { AddOption(); }); //text에 버튼 이벤트 부여
    }

    public void SaveDeckListButton()
    {
        SaveDeckList();
        gameObject.SetActive(false);
    }
    public void CancelDeckListButton()
    {
        gameObject.SetActive(false);
    }

    public void SaveDeckList()
    {
        DeckObjectsToList();
    }



    public void DeckObjectsToList()
    {
        Array.Resize(ref tempHeroDeckList.deckList, content.transform.childCount - 1);
        for (int j = 0; j < tempHeroDeckList.deckList.Length; j++)
        {
            tempHeroDeckList.deckList[j] = content.transform.GetChild(j).GetComponent<EditDeck>().heroDeck;
            tempHeroDeckList.deckList[j].name = content.transform.GetChild(j).Find("InputField").GetComponent<TMP_InputField>().text;
        }
        addDeck.transform.SetAsLastSibling();


        gameManager.SetDeckList(tempHeroDeckList);
        gameManager.UpdateDeckList();
    }

    public void AddOption()
    {
        Array.Resize(ref tempHeroDeckList.deckList, tempHeroDeckList.deckList.Length + 1);
        GameObject deck = Instantiate(deckPrefab, content.transform);
        deck.GetComponent<EditDeck>().heroDeck = new HeroDeck("NewDeck");
        deck.GetComponent<EditDeck>().editDeckList = this;
        tempHeroDeckList.deckList[tempHeroDeckList.deckList.Length - 1] = deck.GetComponent<EditDeck>().heroDeck;
        deck.transform.Find("InputField").GetComponent<TMP_InputField>().text = "NewDeck";
        addDeck.transform.SetAsLastSibling();
    }

    public void RemoveOption(int _opt)
    {
        for (int i = _opt; i < tempHeroDeckList.deckList.Length - 1; i++)
            tempHeroDeckList.deckList[i] = tempHeroDeckList.deckList[i + 1];
        Array.Resize(ref tempHeroDeckList.deckList, tempHeroDeckList.deckList.Length - 1);
    }
}
