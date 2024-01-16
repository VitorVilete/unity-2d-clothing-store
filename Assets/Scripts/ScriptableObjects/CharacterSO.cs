using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Character", menuName ="ScriptableObjects/Character/Character")]
public class CharacterSO : ScriptableObject
{
    public InventorySO inventory;
    public BodySO body;
}
