using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Dependencies")]
    public CharacterSO playerCharacter;
    public SpriteRenderer bodyRenderer;
    public SpriteRenderer clothesRenderer;
    public SpriteRenderer hairRenderer; 
    public AnimationClip walkDown;
    public AnimationClip walkUp;
    public AnimationClip walkRight;
    public AnimationClip walkLeft;

    private List<Sprite> bodyTextures;
    private List<Sprite> clothesTextures;
    private List<Sprite> hairTextures;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCharacterSprites();
    }

    //Frame Data
    //32 - 37 -> walk down
    //40 - 45 -> walk up
    //48 - 53 -> walk right
    //56 - 61 -> walk left
    public void UpdateCharacterSprites()
    {
        bodyTextures = AssetDatabase.LoadAllAssetsAtPath(playerCharacter.body.bodySpriteSheetName).OfType<Sprite>().ToList();
        clothesTextures = AssetDatabase.LoadAllAssetsAtPath(playerCharacter.body.currentClothes.itemPath).OfType<Sprite>().ToList();
        hairTextures = AssetDatabase.LoadAllAssetsAtPath(playerCharacter.body.currentHair.itemPath).OfType<Sprite>().ToList();

        RegisterSprites(walkDown, 6, 32);
        RegisterSprites(walkUp, 6, 40);
        RegisterSprites(walkRight, 6, 48);
        RegisterSprites(walkLeft, 6, 56);
    }

    private void RegisterSprites(AnimationClip animClip, int frameRate, int frameStart)
    {
        SetAnimationClip(animClip, frameRate, frameStart, "body", bodyTextures);
        SetAnimationClip(animClip, frameRate, frameStart, "clothes", clothesTextures);
        SetAnimationClip(animClip, frameRate, frameStart, "hair", hairTextures);

    }

    private void SetAnimationClip(AnimationClip animClip, int frameRate, int frameStart, string path,List<Sprite> spriteSheet)
    {
        EditorCurveBinding spriteBinding = new EditorCurveBinding();
        spriteBinding.type = typeof(SpriteRenderer);
        spriteBinding.path = path;
        spriteBinding.propertyName = "m_Sprite";

        ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[6];
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
