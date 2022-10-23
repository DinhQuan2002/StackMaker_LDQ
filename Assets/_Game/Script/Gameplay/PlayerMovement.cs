using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum Direct { Forward,Back,Right,Left,None}
    public LayerMask layerBrick;
    public float speed = 5f;

    public Transform brickHolder;
    public Transform playerPrefab;
    public Transform playerSkin;


    private List<Transform> playerBricks = new List<Transform>();
    private Vector3 mouseDown, mouseUp;
    private bool isMoving;
    private bool isControl;
    private Vector3 moveNextPoint;



    // Update is called once per frame
    void Update()
    {
        //neu gamemanager la gameplay va khong di chuyen
        if (GameManager.Instance.IsState(GameState.Gameplay) && !isMoving)
        {
            //lay vi tri khi cham vao man hinh 
            if (Input.GetMouseButtonDown(0) && !isControl)
            {
                isControl = true;
                mouseDown = Input.mousePosition;
            }
            //lay vi tri khi nhac tay ra khoi man hinh
            if (Input.GetMouseButtonUp(0) && isControl)
            {
                isControl = false;
                mouseUp = Input.mousePosition;

                Direct direct = GetDirect(mouseDown, mouseUp);
                if(direct != Direct.None)
                {
                    moveNextPoint = FindFinishPoint(direct);
                    isMoving = true;
                }
            }
        }
        else if(isMoving)
        {
            if (Vector3.Distance(transform.position, moveNextPoint) < 0.1f)
            {
                isMoving = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, moveNextPoint, Time.deltaTime * speed);
        }


    }

    public void OnInit()
    {
        isMoving = false;
        isControl = false;
        ClearBrick();
        playerSkin.localPosition = Vector3.zero;
    }

    private Direct GetDirect(Vector3 mouseDown,Vector3 mouseUp)
    {
        //khai bao bien direct = direct.none nghia la khong co huong
        Direct direct = Direct.None;

        float deltaX = mouseUp.x - mouseDown.x;
        float deltaY = mouseUp.y - mouseDown.y;

        //neu khoang cach tu vi tri dat xuong va vi tri nhac len < 100 
        if (Vector3.Distance(mouseDown, mouseUp) < 100)
        {
            //thi khong tinh di chuyen
            direct = Direct.None;
        }
        else
        {
            //neu deltaY > DeltaX
            if(Mathf.Abs(deltaY) > Mathf.Abs(deltaX))
            {
                //va deltaY > 0
                if (deltaY > 0)
                {
                    //Di chuyen theo truc Y duong
                    direct = Direct.Forward;
                }
                else
                {
                    //khong thi di chuyen theo truc Y am
                    direct = Direct.Back;
                }
            }
            else
            {
                //delataX > 0
                if (deltaX > 0)
                {
                    //di chuyen theo truc X duong
                    direct = Direct.Right;
                }
                else
                {
                    //di chuyen theo truc X am
                    direct = Direct.Left;
                }
            }
        }
        //xu ly xong tra ve direct huong di chuyen
        return direct;
    }

    private Vector3 FindFinishPoint(Direct direct)
    {
        
        RaycastHit hit;
        Vector3 nextPoint = transform.position;
        Vector3 dir = Vector3.zero;

        switch (direct)
        {
            case Direct.Forward:
                dir = Vector3.forward;
                break;
            case Direct.Back:
                dir = Vector3.back;
                break;
            case Direct.Right:
                dir = Vector3.right;
                break;
            case Direct.Left:
                dir = Vector3.left;
                break;
            case Direct.None:
                break;
            default:
                break;
        }

        for(int i = 1; i < 100; i++)
        {
            //ban 1 tia raycast huong xuong duoi va o truoc mat nhan vat

            if (Physics.Raycast(transform.position + dir * i + Vector3.up * 2, Vector3.down,out hit, 10f, layerBrick))
            {
                nextPoint = hit.collider.transform.position;
            }
            else
            {
                break;
            }
        }

        return nextPoint;
    }
    public void AddBrick()
    {
        int index = playerBricks.Count;

        //tao ra playerBrick
        Transform playerBrick = Instantiate(playerPrefab, brickHolder);

        playerBrick.localPosition = Vector3.down + index * 0.25f * Vector3.up;

        playerBricks.Add(playerBrick);

        playerSkin.localPosition = playerSkin.localPosition + Vector3.up * 0.25f;

    }

    public void RemoveBrick()
    {
        int index = playerBricks.Count - 1;

        if(index >= 0)
        {
            Transform playerBrick = playerBricks[index];
            playerBricks.RemoveAt(index);
            Destroy(playerBrick.gameObject);

            playerSkin.localPosition = playerSkin.localPosition - Vector3.up * 0.25f;
        }

    }

    public void ClearBrick()
    {
        for(int i = 0;i < playerBricks.Count; i++)
        {
            Destroy(playerBricks[i].gameObject); 
        }

        playerBricks.Clear();
    }
}
