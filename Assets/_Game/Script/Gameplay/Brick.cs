using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameObject brick;
    private bool isCollect = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")&& !isCollect)
        {
            isCollect = true;
            brick.SetActive(false);
            //neu cham tag player thi goi ham addbrick
            other.GetComponent<PlayerMovement>().AddBrick();
            //khi goi ham addbrick xong thi xoa gameobj 
            
        }
    }
}
