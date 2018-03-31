using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDestroyer : MonoBehaviour {

    #region Singleton

    public static MapDestroyer instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public Tilemap tilemap;

    public Tile wallTile;
    public Tile destrucableTile;

    public GameObject explosionPrefab;
    public GameObject[] pickupPrefabs;

    public void Explode (Vector2 worldPos, float fireP)
    {
        Vector3Int originCell = tilemap.WorldToCell(worldPos);

        ExplodeCell(originCell);
        if(ExplodeCell(originCell + new Vector3Int(1, 0, 0)) && fireP > 1)
        {
            if (ExplodeCell(originCell + new Vector3Int(2, 0, 0)) && fireP > 2)
            {
                if (ExplodeCell(originCell + new Vector3Int(3, 0, 0)) && fireP > 3)
                {
                    ExplodeCell(originCell + new Vector3Int(4, 0, 0));
                }
            }
        }
        
        if (ExplodeCell(originCell + new Vector3Int(0, 1, 0)) && fireP > 1)
        {
            if (ExplodeCell(originCell + new Vector3Int(0, 2, 0)) && fireP > 2)
            {
                if (ExplodeCell(originCell + new Vector3Int(0, 3, 0)) && fireP > 3)
                {
                    ExplodeCell(originCell + new Vector3Int(0, 4, 0));
                }
            }
        }
        
        if (ExplodeCell(originCell + new Vector3Int(-1, 0, 0)) && fireP > 1)
        {
            if (ExplodeCell(originCell + new Vector3Int(-2, 0, 0)) && fireP > 2)
            {
                if (ExplodeCell(originCell + new Vector3Int(-3, 0, 0)) && fireP > 3)
                {
                    ExplodeCell(originCell + new Vector3Int(-4, 0, 0));
                }
            }
        }
        
        if (ExplodeCell(originCell + new Vector3Int(0, -1, 0)) && fireP > 1)
        {
            if (ExplodeCell(originCell + new Vector3Int(0, -2, 0)) && fireP > 2)
            {
                if (ExplodeCell(originCell + new Vector3Int(0, -3, 0)) && fireP > 3)
                {
                    ExplodeCell(originCell + new Vector3Int(0, -4, 0));
                }
            }
        }
        
    }

    bool ExplodeCell (Vector3Int cell)
    {
        Tile tile = tilemap.GetTile<Tile>(cell);

        if(tile == wallTile)
        {
            return false;
        }

        if(tile == destrucableTile)
        {
            tilemap.SetTile(cell, null);
            Vector3 pos2 = tilemap.GetCellCenterWorld(cell);
            Instantiate(explosionPrefab, pos2, Quaternion.identity);
            GameObject rPu = RandomPickup(); 
            if (rPu != null)
            {
                Instantiate(rPu, pos2, Quaternion.identity);
            }
            
            return false;
        }

        Vector3 pos = tilemap.GetCellCenterWorld(cell);
        Instantiate(explosionPrefab, pos, Quaternion.identity);

        return true;

    }

    GameObject RandomPickup ()
    {
        // Randomly generate a pickup

        int mainRange = Random.Range(1, 5);

        int range = Random.Range(0, pickupPrefabs.Length);

        if(mainRange == 1)
        {
            return pickupPrefabs[range];
        }
        else
        {
            return null;
        }
    }

}
