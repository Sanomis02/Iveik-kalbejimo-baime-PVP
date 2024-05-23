using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitlesMove : MonoBehaviour
{
    public GameObject sub;
    public Transform head;
    public float spawnDistance = 1;
    // Start is called before the first frame update
    void Start()
    {
       // this.sub = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        sub.transform.position = head.position + new Vector3(head.forward.x, -1, head.forward.z).normalized * spawnDistance;
        sub.transform.LookAt(head.transform);
        sub.transform.forward *= -1;
    }
}
