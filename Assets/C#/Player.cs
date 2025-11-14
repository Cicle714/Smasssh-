using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;

    private Vector3 m_Position1;
    private Vector3 m_Position2;
    [SerializeField]
    private Vector3 playerVec;

    [SerializeField]
    private GameObject Arrow;
    [SerializeField]
    private float ArrowX;
    [SerializeField]
    private float ArrowZ;
    public float debug;

    private bool isAttack = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    public void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_Position1 = Input.mousePosition;
            Arrow.SetActive(true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(m_Position1);
            Arrow.SetActive(false);
            m_Position2 = Input.mousePosition;
            playerVec = m_Position1 - m_Position2;
            if (playerVec.magnitude > 500)
                playerVec = playerVec.normalized * 500;
            rb.linearVelocity = new Vector3(playerVec.x, 0, playerVec.y) * 0.05f;
            rb.AddTorque(Vector3.up * (Mathf.Abs((transform.position.x - worldPos.x ) + (transform.position.z- worldPos.z))) *(playerVec.magnitude) / 5.0f);
            debug=(((transform.position.x + transform.position.z) - (worldPos.x + worldPos.z)));
            isAttack = true;
        }

        if (Arrow.activeSelf)
        {
            m_Position2 = Input.mousePosition;
            playerVec = m_Position1 - m_Position2;
            if (playerVec.magnitude > 500)
                playerVec = playerVec.normalized * 500;
            Arrow.transform.localScale = new Vector3(1, playerVec.magnitude / 200);
            Arrow.transform.position = transform.position;
            Arrow.transform.rotation = Quaternion.Euler(90, 0, (Mathf.Atan2(playerVec.y, playerVec.x) * 180.0f / Mathf.PI) - 90);
        }
        if (rb.linearVelocity == Vector3.zero && isAttack)
        {
            FindObjectOfType<GameManager>().myTurn = false;
            isAttack = false;
        }
    }
}
