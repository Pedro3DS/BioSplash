using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
   public GameObject p1, p2;
    public EventSystem p1Event;
    [SerializeField] private GameObject playerModel1, playerModel2;
    [SerializeField] private Transform playerPoint1, playerPoint2;
    public static PlayerManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
         if (GameObject.FindGameObjectsWithTag("Player").Length == 2)
        {
            p1 = GameObject.FindGameObjectsWithTag("Player")[0];
            p2 = GameObject.FindGameObjectsWithTag("Player")[1];
            FindPlayers();
            if (TimerSystem.instance != null)
            {
                TimerSystem.instance.isActive = true;
                TimerSystem.instance.Begin();
            }
        }



        if (p1 != null && p2 != null)
        {
            Debug.Log("ai");
            p1.GetComponent<PlayerMovement>().DisableAll();
            p2.GetComponent<PlayerMovement>().DisableAll();
            p1.transform.position = playerPoint1.position;
            p2.transform.position = playerPoint2.position;
        }
    }

    public void FindPlayers()
    {
        if (p1 != null && p2 != null && !p1.GetComponent<PlayerMovement>().modelReady && !p2.GetComponent<PlayerMovement>().modelReady)
        {
            Instantiate(playerModel1, p1.transform);
            Instantiate(playerModel2, p2.transform);
            p1.transform.position = playerPoint1.position;
            p2.transform.position = playerPoint2.position;
            p1.GetComponent<PlayerMovement>().modelReady = true;
            p2.GetComponent<PlayerMovement>().modelReady = true;
        }
    }
    public void ITooCanMakeNavigationJumps(GameObject obj)
    {
        p1Event.SetSelectedGameObject(obj);
    }

}
