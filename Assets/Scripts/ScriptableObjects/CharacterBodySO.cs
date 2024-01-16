using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Body", menuName = "ScriptableObjects/Character/Body")]
public class BodySO : ScriptableObject
{
    public ItemSO currentClothes;
    public ItemSO currentHair;
}
