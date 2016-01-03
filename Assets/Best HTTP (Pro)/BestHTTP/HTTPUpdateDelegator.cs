using UnityEngine;

namespace BestHTTP
{
    /// <summary>
    /// Delegates some U3D calls to the HTTPManager.
    /// </summary>
    [ExecuteInEditMode]
    sealed class HTTPUpdateDelegator : MonoBehaviour
    {
        private static HTTPUpdateDelegator instance;
        private static bool IsCreated;

        public static void CheckInstance()
        {
            try
            {
                if (!IsCreated)
                {
                    GameObject go = GameObject.Find("HTTP Update Delegator");
                    if (go != null)
                        instance = go.GetComponent<HTTPUpdateDelegator>();

                    if (instance == null)
                    {
                        go = new GameObject("HTTP Update Delegator");
                        go.hideFlags = HideFlags.HideAndDontSave;
                        //go.hideFlags = HideFlags.DontSave;
                        GameObject.DontDestroyOnLoad(go);

                        instance = go.AddComponent<HTTPUpdateDelegator>();
                    }
                    IsCreated = true;

#if UNITY_EDITOR
                    if (!UnityEditor.EditorApplication.isPlaying)
                    {
                        UnityEditor.EditorApplication.update -= instance.Update;
                        UnityEditor.EditorApplication.update += instance.Update;
                    }

                    UnityEditor.EditorApplication.playmodeStateChanged -= instance.OnPlayModeStateChanged;
                    UnityEditor.EditorApplication.playmodeStateChanged += instance.OnPlayModeStateChanged;
#endif
                }
            }
            catch
            {
                HTTPManager.Logger.Error("HTTPUpdateDelegator", "Please call the BestHTTP.HTTPManager.Setup() from one of Unity's event(eg. awake, start) before you send any request!");
            }
        }

        void Awake()
        {
#if !BESTHTTP_DISABLE_CACHING
            Caching.HTTPCacheService.SetupCacheFolder();
#endif

#if !BESTHTTP_DISABLE_COOKIES
            Cookies.CookieJar.SetupFolder();
            Cookies.CookieJar.Load();
#endif
        }

        void Update()
        {
            HTTPManager.OnUpdate();
        }

#if UNITY_EDITOR
        void OnPlayModeStateChanged()
        {
            if (UnityEditor.EditorApplication.isPlaying)
                UnityEditor.EditorApplication.update -= Update;
            else if (!UnityEditor.EditorApplication.isPlaying)
                UnityEditor.EditorApplication.update += Update;
        }
#endif

        void OnDisable()
        {
            OnApplicationQuit();
        }

        void OnApplicationQuit()
        {
            if (!IsCreated)
                return;

            IsCreated = false;
            HTTPManager.OnQuit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.update -= Update;
            UnityEditor.EditorApplication.playmodeStateChanged -= OnPlayModeStateChanged;
#endif
        }
    }
}