using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSecret2 : MonoBehaviour
{
    public void DestroyPath2(int ennemiesCount2)
    {
        if (ennemiesCount2 <= 0)
        {
            Destroy(gameObject);
        }
    }
}