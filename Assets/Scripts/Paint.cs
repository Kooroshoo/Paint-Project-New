using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


public class Paint : MonoBehaviour {

    public GameObject brush;
    GameObject tempBrush;
    public Material brushMaterial;
    public Material red;
    public Material green;
    public Material blue;
    public Material white;
    public Material black;
    public float BrushSize = 0.1f;
    public RenderTexture RTexture;
    public GameObject drawButtonsUI;


    // Use this for initialization
    void Start()
    {
        tempBrush = brush;
        brush = null;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            //cast a ray to the plane
            var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(Ray, out hit))
            {
                //instanciate a brush
                var go = Instantiate(brush, hit.point + Vector3.up * 0.1f, Quaternion.identity, transform);
                go.transform.localScale = Vector3.one * BrushSize;
                go.GetComponent<MeshRenderer>().material = brushMaterial;
            }
        }
    }

    public void Save()
    {
        StartCoroutine(CoSave());
    }

    public void DrawButtonsUI()
    {
        if (drawButtonsUI.active == true)
        {
            drawButtonsUI.SetActive(false);
            brush = null;
        }
        else if (drawButtonsUI.active == false)
        {
            drawButtonsUI.SetActive(true);
            brush = tempBrush;
        }
        

    }

    public void Black()
    {
        brushMaterial = black;
    }

    public void White()
    {
        brushMaterial = white;
    }

    public void Red()
    {
        brushMaterial = red;
    }

    public void Green()
    {
        brushMaterial = green;
    }

    public void Blue()
    {
        brushMaterial = blue;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Clear()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private IEnumerator CoSave()
    {
        var timestamp = DateTime.Now.ToString("yyyyMMddhhmmssfff");
        //wait for rendering
        yield return new WaitForEndOfFrame();
        Debug.Log(Application.dataPath + "Image-" + timestamp + ".jpg");

        //set active texture
        RenderTexture.active = RTexture;

        //convert rendering texture to texture2D
        var texture2D = new Texture2D(RTexture.width, RTexture.height);
        texture2D.ReadPixels(new Rect(0, 0, RTexture.width, RTexture.height), 0, 0);
        texture2D.Apply();

        //write data to file
        var data = texture2D.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath +"Image-" + timestamp + ".jpg", data);




    }
}
