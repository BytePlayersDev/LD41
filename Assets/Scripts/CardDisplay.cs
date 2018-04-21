using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDisplay : MonoBehaviour {

    public Card Card;

    //Sprite de la carta
    public Sprite artwork;
    //Nombre de la carta
    public new string name;
    //Flag de uso
    public bool Used;
    //Acción
    public CardActionEnum.Action Action;
    // Use this for initialization
    void Start ()
    {
        UpdateCardAtributes();

    }

    private void UpdateCardAtributes()
    {
        //busca la imagen del boton y actualizarla

        UnityEngine.UI.Image ImageButton = GetComponent<UnityEngine.UI.Image>();
        if(ImageButton != null)
        {
            ImageButton.sprite = Card.artwork;
        }
        
        name = Card.name;
        Used = Card.Used;
        Action = Card.Action;
    }

    public void UpdateCard(Card NewCard)
    {
        Card = NewCard;
        UpdateCardAtributes();
    }

  
}
