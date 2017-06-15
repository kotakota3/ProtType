using UnityEngine;
using System.Collections;

public class CreateAtPosition : MonoBehaviour
{ 
    public GameObject[] prefubs;

    void Start()
    {
        int randomIndex = Random.Range(0, prefubs.Length - 1);
        GameObject go = (GameObject)Instantiate(prefubs[randomIndex], Vector3.zero, Quaternion.identity);
        go.transform.SetParent(transform, false);
    }
}
