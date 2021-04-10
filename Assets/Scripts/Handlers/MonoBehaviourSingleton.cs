using UnityEngine;

public class MonoBehaviourSingleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T instance;
    private static object _lock = new object();
    private static bool isPlaceOnDontDestroy;
    public static bool applicationIsQuitting = false;


    protected virtual void Awake()
    {
        if(!isPlaceOnDontDestroy)
        {
            DontDestroyOnLoad(gameObject);
            isPlaceOnDontDestroy = true;
        }
    }
    protected virtual void OnDestroy()
    {
        applicationIsQuitting = true;
    }


    public static T Instance
    {
        get
        {
            //if (applicationIsQuitting)
            //{
            //    return null;
            //}

            //lock (_lock)
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();

                    if (instance == null)
                    {
                        var singleton = new GameObject("[SINGLETON] " + typeof(T));
                        instance = singleton.AddComponent<T>();
                        DontDestroyOnLoad(singleton);
                        isPlaceOnDontDestroy = true;
                    }

                }

                return instance;
            }
        }
    }
}