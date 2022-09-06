using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcculsionCulling : MonoBehaviour
{
    [SerializeField]
    private GameObject [] gameObjects;

    private void Start()
    {
        for(int i = 0; i < 12; i++)
        {
            if(i < 6)
            {
                gameObjects[i].transform.position = new Vector3(i * 2, 0, i * 2);
            }
            else
            {
                gameObjects[i].transform.position = new Vector3(- (i - 6) * 2, 0, -(i - 6) * 2);
            }
        }
    }
}
