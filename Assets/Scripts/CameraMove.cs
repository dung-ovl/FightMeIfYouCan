using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Vector3 moveToPosition;
    float speed = 8f;

    private static CameraMove instance;

    public static CameraMove Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CameraMove>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        this.MoveTo(this.transform.position);
    }

    void Update()
    {
        
    }

    public bool Move()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, moveToPosition, speed * Time.deltaTime);
        if (this.transform.position == moveToPosition)
        {
            return true;
        }
        return false;
    }

    public void MoveTo(Vector3 position)
    {
        moveToPosition = position;
    }
}