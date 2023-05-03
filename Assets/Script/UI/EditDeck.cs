using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditDeck : MonoBehaviour
{
    public HeroDeck heroDeck;
    public EditDeckList editDeckList;

    public void RemoveDeck()
    {
        if (editDeckList.tempHeroDeckList.deckList.Length <= 2)
            return;
        GameObject.Destroy(this.gameObject);
        editDeckList.DeckObjectsToList();

        //gameObject.transform.SetAsLastSibling();
    }
}
