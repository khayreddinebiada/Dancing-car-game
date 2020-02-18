using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMoving : MonoBehaviour {

    
    public float scrollSpeedX = 0.015f;
    public float scrollSpeedY = 0.015f;
    public float scrollSpeedXMaterial2 = 0.015f;
    public float scrollSpeedYMaterial2 = 0.015f;
    public int distanceMaxDisplayUI = 300;
    public GameObject   GrandUI;
    public Vector3      speedGrand;
    public float        tailleMax;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
    void Update()
    {

        float offsetX = Time.time * scrollSpeedX%1;
        float offsetY = Time.time * scrollSpeedY%1;
        float offset2X = Time.time * scrollSpeedXMaterial2%1;
        float offset2Y = Time.time * scrollSpeedYMaterial2%1;
        
        // GetComponent.<Renderer>().material.SetTextureOffset ("_BumpMap", Vector2(offsetX,offsetY));
        GetComponent<Renderer>().material.SetTextureOffset ("_MainTex", new Vector2(offsetX,offsetY));

        if(GetComponent<Renderer>().materials.Length>1){
            GetComponent<Renderer>().materials[1].SetTextureOffset("_MainTex", new Vector2(offset2X, offset2Y));
        }

    }
    /*
    private float coloNumberConversion(float num)
    {
        return (num / 255.0f);
    }
    */
}
