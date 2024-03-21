using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Fog : MonoBehaviour
{
    public int ViewSize;
    public int subViewSize;
    public Tilemap mMap;
    public Tile fog;

    private Vector3Int up = new Vector3Int(0, 1, 0);
    private Vector3Int down = new Vector3Int(0, -1, 0);
    private Vector3Int left = new Vector3Int(-1, 0, 0);
    private Vector3Int right = new Vector3Int(1, 0, 0);

    void OnTriggerStay2D (Collider2D other)
    {
        Debug.Log("1");
        if (!other.CompareTag("Player"))
        {
            return;
        }
        Vector3Int nowPos = new Vector3Int((int)other.transform.position.x, (int)other.transform.position.y, (int)other.transform.position.z);
        TileBase Tbase = mMap.GetTile(nowPos);
        List<Vector3Int> tempPos = new List<Vector3Int>();
        for (int x = -ViewSize ; x < ViewSize ; x++)
        {
            for (int y = -ViewSize; y< ViewSize ; y++)
            {
                if (mMap.GetTile(nowPos + new Vector3Int(x, y, 0)) == null)
                {
                    continue;
                }
                tempPos.Add(nowPos + new Vector3Int(x, y, 0));
            }
        }
        foreach (var item in tempPos)
        {
            mMap.SetTile(item , null);
        }
        if (subViewSize == 0)
        {
            return;
        }
        List<Vector3Int> subPos = new List<Vector3Int>();
        foreach (var item in tempPos)
        {
            for (int c = 1; c <= subViewSize; c++)
            {
                if(!tempPos.Contains(item + up*c) && (mMap.GetTile(item + up*c) != null))
                {
                    subPos.Add(item + up*c);
                }
                if (!tempPos.Contains(item + down * c) && (mMap.GetTile(item + down * c) != null))
                {
                    subPos.Add(item + down * c);
                }
                if (!tempPos.Contains(item + left * c) && (mMap.GetTile(item + left * c) != null))
                {
                    subPos.Add(item + left * c);
                }
                if (!tempPos.Contains(item + right * c) && (mMap.GetTile(item + right * c) != null))
                {
                    subPos.Add(item + right * c);
                }
            }
        }
        foreach (var item in subPos)
        {
            mMap.SetTile(item,fog);
            tempPos.Add(item);
        }
    }
    
}
