using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card :ScriptableObject {

    //Sprite de la carta
    public Sprite artwork;
    //Nombre de la carta
    public new string name;
    //Flag de uso
    public bool Used = false;
    //Acción
    public CardActionEnum.Action Action = CardActionEnum.Action.MoveRight;

}
