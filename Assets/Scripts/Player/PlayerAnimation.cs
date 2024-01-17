using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Dependencies")]
    public Body playerCharacterBody;
    public SpriteRenderer bodyRenderer;
    public SpriteRenderer clothesRenderer;
    public SpriteRenderer hairRenderer;
    public AnimationClip idle;
    public AnimationClip walkDown;
    public AnimationClip walkUp;
    public AnimationClip walkRight;
    public AnimationClip walkLeft;
    public Animator playerAnimator;
    Func<Sprite, int> spriteOrderer = s => int.Parse(s.name.Split('_').Last());

    void Start()
    {
        UpdateCharacterSprites();
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 movementInput = value.ReadValue<Vector2>();
        playerAnimator.SetFloat("Horizontal", movementInput.x);
        playerAnimator.SetFloat("Vertical", movementInput.y);
        playerAnimator.SetBool("IsWalking", movementInput != Vector2.zero);
    }

    public void UpdateCharacterSprites(ItemSO item)
    {
        Debug.Log("item to be equiped:" + item);
        if (item.itemType == "Clothes")
        {
            playerCharacterBody.currentClothesSheetName = item.itemPath;
        }
        else if (item.itemType == "Hair")
        {
            playerCharacterBody.currentHairSheetName = item.itemPath;
        }
        else
        {
            return;
        }
        UpdateCharacterSprites();
    }


    //Frame Data
    //1 -> Idle
    //32 - 37 -> walk down
    //40 - 45 -> walk up
    //48 - 53 -> walk right
    //56 - 61 -> walk left
    public void UpdateCharacterSprites()
    {
        List<Sprite> bodyTextures = AssetDatabase.LoadAllAssetsAtPath(playerCharacterBody.bodySpriteSheetName).OfType<Sprite>().OrderBy(spriteOrderer).ToList();
        List<Sprite> clothesTextures = AssetDatabase.LoadAllAssetsAtPath(playerCharacterBody.currentClothesSheetName).OfType<Sprite>().OrderBy(spriteOrderer).ToList();
        List<Sprite> hairTextures = AssetDatabase.LoadAllAssetsAtPath(playerCharacterBody.currentHairSheetName).OfType<Sprite>().OrderBy(spriteOrderer).ToList();

        RegisterSprites(idle, 1, 0, bodyTextures, clothesTextures, hairTextures);
        RegisterSprites(walkDown, 6, 32, bodyTextures, clothesTextures, hairTextures);
        RegisterSprites(walkUp, 6, 40, bodyTextures, clothesTextures, hairTextures);
        RegisterSprites(walkRight, 6, 48, bodyTextures, clothesTextures, hairTextures);
        RegisterSprites(walkLeft, 6, 56, bodyTextures, clothesTextures, hairTextures);
    }

    private void RegisterSprites(AnimationClip animClip, int frameRate, int frameStart, List<Sprite> bodyTextures, List<Sprite> clothesTextures, List<Sprite> hairTextures)
    {
        animClip.ClearCurves();
        SetAnimationClip(animClip, frameRate, frameStart, "BodyRenderer", bodyTextures);
        SetAnimationClip(animClip, frameRate, frameStart, "ClothesRenderer", clothesTextures);
        SetAnimationClip(animClip, frameRate, frameStart, "HairRenderer", hairTextures);
    }

    private void SetAnimationClip(AnimationClip animClip, int frameRate, int frameStart, string path, List<Sprite> spriteSheet)
    {
        EditorCurveBinding spriteBinding = new EditorCurveBinding();
        spriteBinding.type = typeof(SpriteRenderer);
        spriteBinding.path = path;
        spriteBinding.propertyName = "m_Sprite";

        ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[frameRate];
        animClip.frameRate = frameRate;
        for (int i = 0; i < spriteKeyFrames.Length; i++)
        {
            spriteKeyFrames[i] = new ObjectReferenceKeyframe();
            spriteKeyFrames[i].time = i;
            spriteKeyFrames[i].value = spriteSheet[frameStart + i];
        }
        AnimationUtility.SetObjectReferenceCurve(animClip, spriteBinding, spriteKeyFrames);
    }

    
    
}
