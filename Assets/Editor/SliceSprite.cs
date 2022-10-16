using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SliceSprite : MonoBehaviour
{
    [MenuItem("EditorByXu/SliceSprites")]
    private static void SliceSprites()
    {
        int sliceWidth = 64;
        int sliceHeight = 64;

        string folderPath = "Sprite";

        Object[] spritesheets = Resources.LoadAll(folderPath, typeof(Texture2D));
        for (int z = 0; z < spritesheets.Length; z++)
        {
            string path = AssetDatabase.GetAssetPath(spritesheets[z]);
            TextureImporter ti = AssetImporter.GetAtPath(path) as TextureImporter;
            ti.isReadable = true;
            ti.spriteImportMode = SpriteImportMode.Multiple;
            List<SpriteMetaData> newData = new List<SpriteMetaData>();
            Texture2D spritesheet = spritesheets[z] as Texture2D;
            for (int i = 0; i < spritesheet.width; i += sliceWidth)
            {
                for (int j = spritesheet.height; j > 0; j -= sliceHeight)
                {
                    SpriteMetaData smd = new SpriteMetaData();
                    smd.pivot = new Vector2(0.5f, 0.5f);
                    smd.alignment = 9;
                    smd.name = (spritesheet.height - j) / sliceHeight + "_ " + i / sliceWidth;
                    smd.rect = new Rect(i, j - sliceHeight, sliceWidth, sliceHeight);
                    newData.Add(smd);
                }
            }
            ti.spritesheet = newData.ToArray();
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }
        Debug.Log("success !");
    }
}