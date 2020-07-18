using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using SmartDLL;

public class FileExplorer : MonoBehaviour
{

    string path;
    public Material imageMaterial;
    public SmartFileExplorer fileExplorer = new SmartFileExplorer();

    public void OpenExplorer()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        string initialDir = @"C:\";
        bool restoreDir = false;
        string title = "Open a jpg File";
        string defExt = "jpg";
        string filter = "jpg files (*.jpg)|*.jpg";

        fileExplorer.OpenExplorer(initialDir, restoreDir, title, defExt, filter);
        path = fileExplorer.fileName;
        GetImage();
    }

    void GetImage()
    {
        if (path != null)
        {
            UpdateImage();
        }
    }

    void UpdateImage()
    {
        Debug.Log("Update Image");
        WWW www = new WWW("file:///" + path);
        imageMaterial.mainTexture = www.texture;
    }
}
