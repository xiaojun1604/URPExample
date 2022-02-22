using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    private string assetRoot = string.Empty;
    private readonly string albedoTexsID = "_AlbedoTexs";

    public GameObject go;
    public Slider slider;
    public Texture2DArray textoArray;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogError("supports2DArrayTextures state:["+SystemInfo.supports2DArrayTextures);
#if UNITY_ANDROID && !UNITY_EDITOR
        assetRoot = "jar:file:///"+ Application.dataPath + "!assets/AssetBundles/";
#else
        assetRoot = Application.dataPath;// + "/../AssetBundles/";
        assetRoot = Application.streamingAssetsPath;// + "/AssetBundles/";
#endif
        Debug.LogError(Application.streamingAssetsPath);
        string albedoTexPath = assetRoot + "/texarr.asset";
        //loadTexArray(albedoTexPath);
        //loadTexArray();
        slider.onValueChanged.AddListener(sliderChange);
    }

    public void sliderChange(float num)
    {
        Debug.LogError(num);
        SetMaterial(textoArray,slider.value);
    }

    private void loadTexArray(string path)
    {
        if (false == File.Exists(path))
        {
            //return;
        }
        AssetBundle ab = AssetBundle.LoadFromFile(path);
        if (null != ab)
        {
            // Object[] objects = ab.LoadAllAssets();
            // foreach (var o in objects)
            // {
            //     Debug.Log(o.name);
            // }
            Texture2DArray[] texArrays = ab.LoadAllAssets<Texture2DArray>();
            Debug.Log(texArrays.Length);
            textoArray = texArrays[0];
            SetMaterial(textoArray,0);

        }
        else
        {
            Debug.LogError("ab is null,path:["+ path +"]");
        }
    }
    private void loadTexArray()
    {
        textoArray =  Resources.Load<Texture2DArray>("texarr.asset");
        SetMaterial(textoArray,0);
    }

    private void SetMaterial(Texture2DArray texArr,float index)
    {
        Material ma = go.GetComponent<Renderer>().sharedMaterial;
        ma.SetTexture("_TexArray",texArr);
        ma.SetFloat("_TexArrayIndex",index);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
