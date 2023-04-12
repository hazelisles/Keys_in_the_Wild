using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject keyPrefab;
    // list for random point initial keys position
    private Vector3[] initPoints;
    private GameObject[] keys;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.GAME_RESTART, OnGameRestart);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.GAME_RESTART, OnGameRestart);
    }
    // Start is called before the first frame update
    void Start()
    {
        initPoints = new Vector3[5];
        initPoints[0] = new Vector3(-53.86f,1,1.4f);
        initPoints[1] = new Vector3(-20.03243f, 1.1f, 20.15111f);
        initPoints[2] = new Vector3(-49.43243f, 1, 64.05111f);
        initPoints[3] = new Vector3(39.56757f, 1.2f, 9.051113f);
        initPoints[4] = new Vector3(-64.56243f, 1, -62.49889f);
        keys = new GameObject[5];
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i] = Instantiate(keyPrefab) as GameObject;
            keys[i].transform.position = initPoints[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGameRestart()
    {
        SceneManager.LoadScene(0);
    }
}
