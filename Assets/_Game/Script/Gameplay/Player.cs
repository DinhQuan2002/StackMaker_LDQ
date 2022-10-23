using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] public LayerMask layer;
    [SerializeField] private Transform playerPos;
    [SerializeField] private GameObject stackBrick;
    public enum Direct { forward,back,right,left,none};
    public Transform playerBrickPrefabs;
    public Transform brickHolder;

    [SerializeField] private float dragDistance =1f;
    private Vector3 firstPoint;
    private Vector3 lastPoint;
    Vector3 finishPoint = Vector3.zero;

    private int count;
    private List<Transform> playerBricks = new List<Transform>();

    public Direct direct;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        //transform.position = startPoint.position;
        // startPoint = transform.position;
        finishPoint = transform.position;
        playerPos.position = transform.position;
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            firstPoint = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            lastPoint = Input.mousePosition;
            Swipe();
        }
        Move();
        
    }


    public Vector3 FindFinishPoint(Direct direct)
    {
        RaycastHit hit;
        Vector3 dir = Vector3.zero;
        Vector3 finish = Vector3.zero;
        int i = 1;
        switch (direct)
        {
            case Direct.forward:
                dir = Vector3.forward;
                break;
            case Direct.back:
                dir = Vector3.back;
                break;
            case Direct.right:
                dir = Vector3.right;
                break;
            case Direct.left:
                dir = Vector3.left;
                break;
            case Direct.none:
                break;
            default:
                break;
        }
        //Debug.LogWarning(dir);
        bool isa = Physics.Raycast(transform.position + dir * i, Vector3.down, out hit, Mathf.Infinity, layer);

        Debug.DrawRay(transform.position + dir * i, Vector3.down, Color.blue, Mathf.Infinity);
        while (isa)
        {
            i++;
            isa = Physics.Raycast(transform.position + dir * i, Vector3.down, out hit, Mathf.Infinity, layer);
            finish = transform.position+dir*(i-1);
            if (isa == true)
            {
                finishPoint = hit.collider.transform.position;
            }
            Debug.LogError(finish);
        }
        //Debug.DrawRay(finish, Vector3.down, Color.red, 10f);
        Debug.LogError(finish);
        return finish;
}

    
    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, finishPoint, Time.deltaTime * 10f);
    }
    //public Vector3 Move(Direct point)
    //{
    //    if (point == Direct.forward)
    //    {
    //        startPoint.position = Vector3.MoveTowards(startPoint.position,FindFinishPoint(Direct.forward),Time.deltaTime);
    //    }
    //    if (point == Direct.back)
    //    {
    //        startPoint.position = Vector3.MoveTowards(startPoint.position, FindFinishPoint(Direct.back), Time.deltaTime);
    //    }
    //    if (point == Direct.left)
    //    {
    //        startPoint.position = Vector3.MoveTowards(startPoint.position, FindFinishPoint(Direct.left), Time.deltaTime);
    //    }
    //    if (point == Direct.right)
    //    {
    //        startPoint.position = Vector3.MoveTowards(startPoint.position, FindFinishPoint(Direct.right), Time.deltaTime);
    //    }
    //    return Vector3.zero;
    //}
    public void Swipe()
    {
        //neu diem x cuoi - diem x dau > khoang cach keo hoac diem y cuoi - diem y dau > khoang cach keo thi
        if (Mathf.Abs(lastPoint.x - firstPoint.x) > dragDistance || Mathf.Abs(lastPoint.y - firstPoint.y) > dragDistance)
        {
            //neu diem x cuoi - diem x dau > diem y cuoi - diem y dau thi
            if (Mathf.Abs(lastPoint.x - firstPoint.x) > Mathf.Abs(lastPoint.y - firstPoint.y))
            {
                //neu diem x cuoi > diem x dau thi tuc la dang di chuyen qua phai
                if (lastPoint.x > firstPoint.x)
                {
                    Debug.Log("right");
                    //Move(Direct.right);
                    FindFinishPoint(Direct.right);
                }
                //ko thi la dang vuot qua trai
                else
                {
                    Debug.Log("left");
                    //Move(Direct.left);
                    FindFinishPoint(Direct.left);
                }
            }
            //nguoc lai neu diem x cuoi - diem x dau < diem y cuoi - diem y dau thi
            else
            {
                //neu diem y cuoi - diem y dau thi tuc la dang vuot len
                if (lastPoint.y > firstPoint.y)
                {
                    Debug.Log("Up");
                    //Move(Direct.forward);
                    FindFinishPoint(Direct.forward);
                }
                //khong thi dang vuot xuong
                else
                {
                    Debug.Log("Down");
                    //Move(Direct.back);
                    FindFinishPoint(Direct.back);
                }
            }
        }
        //neu cac diem ko vuot qua khoang cach keo thi chi la cham vao man hinh
        else
        {
            //just one tap
            Debug.Log("Tap");
            direct = Direct.none;
        }
    }

    

    public void OnInit()
    {
        
        
    }

    public void AddBrick()
    {
        int i = playerBricks.Count;
        Transform playerBrick = Instantiate(playerBrickPrefabs, brickHolder);
        
        //playerPos.position += Vector3.up * 0.5f;
        //Instantiate(stackBrick, playerPos.position - Vector3.up * 0.5f, Quaternion.Euler(new Vector3(-90, 0, -180)), transform);
    }

    public void RemoveBrick()
    {
        //giam chieu cao nhan vat va xoa 1 vien gach
    }
    public void ClearBrick()
    {
        //dua nhan vat ve goc
        //xoa tat ca gach
    }
}
