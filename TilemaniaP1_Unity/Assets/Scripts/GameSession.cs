using UnityEngine;

public class GameSession : MonoBehaviour
{
    void Awake()
    {
        int gameSessionCount = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;
        
        if (gameSessionCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
