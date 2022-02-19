using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class Texture2DArrayEditor : Editor
{
    [MenuItem("Tools/CreatTexture2DArrary")]
    public static  void combineTexture()
    {
        string texfloder = "Assets/ExampleAssets/Textures/Concrete/";
        string texarrPath = texfloder + "texarr.asset";
        string tex0path = texfloder + "Ground_Albedo.tif";
        string tex1path = texfloder + "Ground_Albedo_01.tif";
        string tex2path = texfloder + "Ground_Albedo_02.tif";
        Texture2D tex0= AssetDatabase.LoadAssetAtPath<Texture2D>(tex0path);
        Texture2D tex1 = AssetDatabase.LoadAssetAtPath<Texture2D>(tex1path);
        Texture2D tex2 = AssetDatabase.LoadAssetAtPath<Texture2D>(tex2path);
        Texture2DArray texArr = new Texture2DArray(tex0.width, tex0.width, 3, tex0.format, false, false);
        Graphics.CopyTexture(tex0, 0, 0, texArr, 0, 0);
        Graphics.CopyTexture(tex1, 0, 0, texArr, 1, 0);
        Graphics.CopyTexture(tex2, 0, 0, texArr, 2, 0);
        texArr.wrapMode = TextureWrapMode.Clamp;
        texArr.filterMode = FilterMode.Bilinear;
        if (AssetDatabase.LoadAssetAtPath<Texture2D>(texarrPath))
        {
            AssetDatabase.DeleteAsset(texarrPath);
        }
        AssetDatabase.CreateAsset(texArr,texarrPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
