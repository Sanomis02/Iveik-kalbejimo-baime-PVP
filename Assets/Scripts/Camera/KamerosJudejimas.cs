using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamerosJudejimas : MonoBehaviour
{
    public Transform kamerosPozicija;

    void Update()
    {
        transform.position = kamerosPozicija.position;
    }
}
