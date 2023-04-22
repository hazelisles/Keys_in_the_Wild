using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject keyPrefab;
    
    // list for initial keys position
    private List<CollectableKey> keys = new List<CollectableKey>();
    [SerializeField] private UIController ui;
    [SerializeField] private WelcomePopup welcomePopup;

    public float timeRemaining;
    
    private void Awake()
    {
        Messenger.AddListener(GameEvent.GAME_RESTART, OnGameRestart);

        GameObject keyParent = GameObject.FindWithTag("Keys");
        foreach (Transform child in keyParent.transform)
        {
            //child is your child transform
            keys.Add(child.GetComponent<CollectableKey>());
        }
        //Debug.Log("keys:" + keys.Count);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.GAME_RESTART, OnGameRestart);
    }
    // Start is called before the first frame update
    void Start()
    {
        welcomePopup.Open();
        timeRemaining = 601f;       // Set game timer

        //initPoints[0] = new Vector3(-53.86f,1,1.4f);
        //initPoints[1] = new Vector3(-20.03243f, 1.1f, 20.15111f);
        //initPoints[2] = new Vector3(-49.43243f, 1, 64.05111f);
        //initPoints[3] = new Vector3(39.56757f, 1.2f, 9.051113f);
        //initPoints[4] = new Vector3(-64.56243f, 1, -62.49889f);
        
        //for (int i = 0; i < keys.Length; i++)
        //{
        //    if (keys[i] == null)
        //    {
        //        keys[i] = Instantiate(keyPrefab) as GameObject;
        //        keys[i].transform.position = initPoints[i];
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale > 0)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0;
                Messenger.Broadcast(GameEvent.GAME_OVER);
            }
        }
    }


    private void OnGameRestart()
    {
        SceneManager.LoadScene(0);      
    }
}
