using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card {

    //Sprite de la carta
    public Texture2D CardIcon = null;
    //Nombre de la carta
    public string Name = "New card";
    //Flag de uso
    public bool Used = false;
    //Acción
    public CardActionEnum.Action Action = CardActionEnum.Action.MoveRight;

}
