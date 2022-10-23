using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unbrick : MonoBehaviour
{
    public GameObject brick;
    private bool isCollect = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& !isCollect)
        {
            isCollect = true;
            brick.SetActive(true);
            //neu cham tag player thi goi ham addbrick
            other.GetComponent<PlayerMovement>().RemoveBrick();
            //khi goi ham addbrick xong thi xoa gameobj 
            
        }
    }
}
