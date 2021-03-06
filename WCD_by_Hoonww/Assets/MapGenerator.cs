﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    MapParser parser;

    // Use this for initialization
    void Start ()
    {
        float[] xx = new float[6] { 0, 9, 9, 0, -9, -9 };
        float[] zz = new float[6] { 10.392f, 5.196f, -5.196f, -10.392f, -5.196f, 5.196f };
        float x = 0.0f, z = 0.0f;
        parser = GameObject.Find("MapParser").GetComponent<MapParser>();
        DS MapData = parser.data;

        parser.Parse();
        
        GameObject obj = GameObject.Find("tile");
        GameObject tile;
        for (int area=0; area<MapData.Areas; area++)
        {
            if (area != 0)
            {
                tile = Instantiate(obj, new Vector3(obj.transform.position.x + x, obj.transform.position.y, obj.transform.position.z + z), Quaternion.identity);

                tile.GetComponentInChildren<MeshRenderer>().material = (UnityEngine.Material)Resources.Load("Tile Material " + MapData.map[area].tile[0].material_num.ToString());
            }
            for (int i = 0; i < 6; i++)
            {
                if (MapData.map[area].tile[i].enable)
                {
                    tile = Instantiate(obj, new Vector3(obj.transform.position.x + x + xx[i], obj.transform.position.y, obj.transform.position.z + z + zz[i]), Quaternion.identity);

                    tile.GetComponentInChildren<MeshRenderer>().material = (UnityEngine.Material)Resources.Load("Tile Material " + MapData.map[area].tile[i].material_num.ToString());
                }
            }

            if (area < MapData.Areas - 1)
            {
                x += xx[MapData.map[area].nextArea-1] * 3;
                z += zz[MapData.map[area].nextArea-1] * 3;
            }
        }
        
        for (int i = 0; i < 6; i++)
        {
            GameObject curwall = GameObject.Find("FW" + i);
            curwall.transform.position = new Vector3(0.0f + x + xx[i] / 2, 4.0f, 0.0f + z + zz[i] / 2);
            curwall.GetComponent<Rotate>().goal = 1;
            if (i % 3 == 1) curwall.transform.RotateAround(curwall.transform.position, Vector3.up, 60.0f);
            else if (i % 3 == 2) curwall.transform.RotateAround(curwall.transform.position, Vector3.up, -60.0f);
        }
    }
	
	// Update is called once per frame
	void Update () {
	}
}
