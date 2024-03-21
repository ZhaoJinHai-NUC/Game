using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public WallMovement wm;

    [Header("UI组件")]
    public Text textLabel;
    public Image faceImage;

    [Header("文本文件")]
    public TextAsset textFile;
    public int index;

    List<string> textList = new List<string>();

    void Awake()
    {
        GetTextFromFlie(textFile);
    }
    private void OnEnable()
    {
        textLabel.text = textList[index];
        index++;
    }

    void Update()
    {


        if (Input .GetKeyDown (KeyCode.E)&& index==textList .Count)
        {
            wm.canMove = true;
            gameObject.SetActive(false);
            index = 0;
            return;
        }
        if(Input .GetKeyDown(KeyCode.E))
        {
            wm.canMove = false;
            textLabel.text = textList[index];
            index++;
        }   
    }

    void GetTextFromFlie(TextAsset file)
    {
        textList.Clear();
        index = 0;

        var lineData = file.text.Split("\n");

        foreach (var line in lineData)
        {
            textList.Add(line);
        }
    }
}
