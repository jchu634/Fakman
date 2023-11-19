using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;



public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public LayerMask MazeCollider;
    [SerializeField] private TMPro.TextMeshProUGUI Score;
    [SerializeField] private GameObject OptionsMenu;

    public Transform TeleportBlockLeft;
    public Transform TeleportBlockRight;

    public Transform movePoint;
    private bool Teleported = false;

    public Animator animator;

    private int currentMode = 1;
    private int Horizontal;
    private int Vertical;
    private Vector3 movementLock = new Vector3(0f, 0f, 0f);
    [SerializeField] private Transform LiveIndicator1;
    [SerializeField] private Transform LiveIndicator2;
    [SerializeField] private Transform LiveIndicator3;
    public GameObject gameOverMenu;

    void Start()
    {
        Screen.SetResolution(Globals.currentResolution.width, Globals.currentResolution.height, Screen.fullScreen);
        Time.timeScale = 1;
        gameOverMenu.SetActive(false);
        switch (Globals.lives)
        {
            case 2:
                LiveIndicator1.gameObject.SetActive(false);
                break;
            case 1:
                LiveIndicator2.gameObject.SetActive(false);
                goto case 2;
            case 0:
                LiveIndicator3.gameObject.SetActive(false);
                goto case 1;
        }
        Globals.PlayerDead = false;
        Globals.PelletsCollected = 0;
        Globals.GhostKillable = false;
        movePoint.parent = null;
        ChangeMode();
    }
    private void ChangeMode()
    {
        
        switch (currentMode)
        {
            case 1:
            case 3:
                
                Globals.IsModeScatter = true;
                switch (Globals.Level)
                {
                    case int n when (n < 5):
                        StartCoroutine(ChangeModeTimer(7f));
                        break;
                    default:
                        StartCoroutine(ChangeModeTimer(5f));
                        break;
                }
                break;
            case 5:
                Globals.IsModeScatter = true;
                StartCoroutine(ChangeModeTimer(5f));
                break;
            case 7:
                Globals.IsModeScatter = true;
                if (Globals.Level == 1){ StartCoroutine(ChangeModeTimer(5)); }
                else
                {
                    StartCoroutine(ChangeModeTimer(0.017f));
                }
                break;
            case 2:
            case 4:
                Globals.IsModeScatter = false;
                StartCoroutine(ChangeModeTimer(20f));
                break;
            case 6:
                switch (Globals.Level)
                {
                    case 1:
                        StartCoroutine(ChangeModeTimer(20f));
                        break;
                    case int n when (n < 5):
                        StartCoroutine(ChangeModeTimer(1033f));
                        break;
                    default:
                        StartCoroutine(ChangeModeTimer(1037f));
                        break;
                }
                break;
            case 8:
                Globals.IsModeScatter = false;
                break;
        }
        
    }
    void setSpeed()
    {
        switch (Globals.Level)
        {
            case 1:
                moveSpeed = Globals.basePacmanSpeed * .8f;
                break;
            case int n when ((n < 21) && (n > 4)):
                moveSpeed = Globals.basePacmanSpeed;
                break;
            default:
                moveSpeed = Globals.basePacmanSpeed * .9f;
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Intersection") { Debug.Log("PlayerCollisionIntersectionHit"); }
        //else if (collision.gameObject.tag == "Ghosts") { Debug.Log("GhostPlayerCollisionHit"); }
        if (!Teleported & collision.gameObject.tag == "TeleportBlockRight")
        {
            transform.position = TeleportBlockLeft.position;
            movePoint.position = TeleportBlockLeft.position + new Vector3(1,0);
            StartCoroutine(EnableTeleportBlock(.5f));
        }
        else if (!Teleported & collision.gameObject.tag == "TeleportBlockLeft")
        {
            transform.position = TeleportBlockRight.position;
            movePoint.position = TeleportBlockRight.position + new Vector3(-1,0);
            StartCoroutine(EnableTeleportBlock(.5f));
        }
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown("escape")|Input.GetKey("escape"))
        {
            OptionsMenu.SetActive(true);
            Time.timeScale = 0;
        }
        Score.text = Globals.score.ToString();
        if (/*false*/Globals.PlayerDead == true)
        {
            animator.SetBool("IsDead", true);
            StartCoroutine(KillAfterDelay(.9f)); 
        }
        else if (Globals.PelletsCollected == 187)
        {
            Globals.Level += 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("NextLevel");
        }
        else 
        { 
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
            {
                if ((int)Input.GetAxisRaw("Horizontal") != 0 ^ (int)Input.GetAxisRaw("Vertical") != 0)
                {
                Horizontal = (int)Input.GetAxisRaw("Horizontal");
                Vertical = (int)Input.GetAxisRaw("Vertical");
            }
            if (Mathf.Abs(Horizontal) == 1f)                                                                                            //Move Horizontal
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Horizontal, 0f, 0f), .2f, MazeCollider))                  //Collision Check
                {
                    movementLock.x = Horizontal;
                    movementLock.y = 0f;
                    movePoint.position += movementLock;
                    animator.SetFloat("Horizontal", movementLock.x);                                                                        //Set Animation
                    animator.SetFloat("Vertical", movementLock.y);
                }
                else if (!Physics2D.OverlapCircle(movePoint.position + movementLock, .2f, MazeCollider))
                {
                    movePoint.position += movementLock;
                }
            }
            else if (Mathf.Abs(Vertical) == 1f)                                                                                         //Move Vertical
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Vertical, 0f), .2f, MazeCollider))                    //Collision Check
                {
                    movementLock.x = 0f;
                    movementLock.y = Vertical;
                    movePoint.position += movementLock;
                    animator.SetFloat("Horizontal", movementLock.x);                                                                        //Set Animation
                    animator.SetFloat("Vertical", movementLock.y);
                }
                else if (!Physics2D.OverlapCircle(movePoint.position + movementLock, .2f, MazeCollider))
                {
                    movePoint.position += movementLock;
                }
            }
        }
        }
}
    
 IEnumerator ChangeModeTimer(float time)
    {
        
        yield return new WaitForSeconds(time);
        
        currentMode += 1;
        ChangeMode();
    }
    IEnumerator KillAfterDelay(float time)
    {
        
        yield return new WaitForSeconds(time);
        // Code to execute after the delay
        if (!(Globals.lives == 0))
        {
            Globals.lives -= 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            gameOverMenu.SetActive(true);
            Time.timeScale = 0;
        }
        //Time.timeScale = 0; //Destroy(gameObject);
    }
    IEnumerator EnableTeleportBlock(float time)
    {
        Teleported = true;
        yield return new WaitForSeconds(time);
        //Debug.Log("EnabledTeleport");
        Teleported = false;
    }
}