using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathSecret : MonoBehaviour
{

    //public Tilemap PathBoss;
    public void DestroyPath(int ennemiesCount)
    {
        if (ennemiesCount <= 0)
        {
            //PathBoss.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
