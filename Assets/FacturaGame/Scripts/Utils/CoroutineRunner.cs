using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FacturaGame.Scripts.Utils
{
    public class CoroutineRunner : MonoBehaviour
    {
        private static CoroutineRunner _instance;
        
        private readonly List<Coroutine> _coroutines = new List<Coroutine>();
        
        public static void RunSelfStopCoroutine(IEnumerator coroutine)
        {
            if (_instance == null)
            {
                CreateInstance();
            }

            Coroutine c = _instance.StartCoroutine(coroutine);
        }

        public static Coroutine RunCoroutine(IEnumerator coroutine)
        {
            if (_instance == null)
            {
                CreateInstance();
            }
            
            Coroutine c = _instance.StartCoroutine(coroutine);
            _instance._coroutines.Add(c);

            return c;
        }

        public static void CancelCoroutine(Coroutine coroutine)
        {
            if (_instance == null)
            {
                CreateInstance();
            }
            
            _instance._coroutines.Remove(coroutine);
            _instance.StopCoroutine(coroutine);
        }

        private static void CreateInstance()
        {
            GameObject go = new GameObject(nameof(CoroutineRunner));
            CoroutineRunner coroutineRunner = go.AddComponent<CoroutineRunner>();
            DontDestroyOnLoad(go);
            _instance = coroutineRunner;
        }
    }
}