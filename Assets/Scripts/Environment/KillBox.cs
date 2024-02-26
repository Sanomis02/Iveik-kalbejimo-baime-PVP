using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillBox : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private MeshCollider floorCollider;

    void Start()
    {
        if(!floorCollider.isTrigger)
        {
            Debug.Log("Floor neturi trigger!");
        }
    }

    private void OnTriggerEnter(Collider zaidejo)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
