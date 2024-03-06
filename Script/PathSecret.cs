using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSecret : MonoBehaviour
{
    public void DestroyPath(int ennemiesCount)
    {
        if (ennemiesCount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
