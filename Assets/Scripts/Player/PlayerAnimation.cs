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

    private List<Sprite> bodyTextures;
    private List<Sprite> clothesTextures;
    private List<Sprite> hairTextures;

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
        bodyTextures = AssetDatabase.LoadAllAssetsAtPath(playerCharacterBody.bodySpriteSheetName).OfType<Sprite>().ToList();
        clothesTextures = AssetDatabase.LoadAllAssetsAtPath(playerCharacterBody.currentClothes.itemPath).OfType<Sprite>().ToList();
        hairTextures = AssetDatabase.LoadAllAssetsAtPath(playerCharacterBody.currentHair.itemPath).OfType<Sprite>().ToList();
        Debug.Log("item to be equiped:" + item);
        if (item.itemType == "Clothes")
        {
            clothesTextures = AssetDatabase.LoadAllAssetsAtPath(item.itemPath).OfType<Sprite>().ToList();
        }
        else if (item.itemType == "Hair")
        {
            hairTextures = AssetDatabase.LoadAllAssetsAtPath(item.itemPath).OfType<Sprite>().ToList();
        }
        else
        {
            return;
        }

        RegisterSprites(idle, 1, 0);
        RegisterSprites(walkDown, 6, 32);
        RegisterSprites(walkUp, 6, 40);
        RegisterSprites(walkRight, 6, 48);
        RegisterSprites(walkLeft, 6, 56);
    }


    //Frame Data
    //1 -> Idle
    //32 - 37 -> walk down
    //40 - 45 -> walk up
    //48 - 53 -> walk right
    //56 - 61 -> walk left
    public void UpdateCharacterSprites()
    {
        bodyTextures = AssetDatabase.LoadAllAssetsAtPath(playerCharacterBody.bodySpriteSheetName).OfType<Sprite>().ToList();
        clothesTextures = AssetDatabase.LoadAllAssetsAtPath(playerCharacterBody.currentClothes.itemPath).OfType<Sprite>().ToList();
        hairTextures = AssetDatabase.LoadAllAssetsAtPath(playerCharacterBody.currentHair.itemPath).OfType<Sprite>().ToList();

        RegisterSprites(idle, 1, 0);
        RegisterSprites(walkDown, 6, 32);
        RegisterSprites(walkUp, 6, 40);
        RegisterSprites(walkRight, 6, 48);
        RegisterSprites(walkLeft, 6, 56);
    }

    private void RegisterSprites(AnimationClip animClip, int frameRate, int frameStart)
    {
        SetAnimationClip(animClip, frameRate, frameStart, "BodyRenderer", bodyTextures);
        SetAnimationClip(animClip, frameRate, frameStart, "ClothesRenderer", clothesTextures);
        SetAnimationClip(animClip, frameRate, frameStart, "HairRenderer", hairTextures);
    }

    private void SetAnimationClip(AnimationClip animClip, int frameRate, int frameStart, string path,List<Sprite> spriteSheet)
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
