using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Body", menuName = "ScriptableObjects/Character/Body")]
public class BodySO : ScriptableObject
{
    public ItemSO currentClothes;
    public ItemSO currentHair;
    public string bodySpriteSheetName = "Assets/Art/Sprites/Characters/Mana Seed Character Base Demo/char_a_p1/char_a_p1_0bas_humn_v01.png";
}
