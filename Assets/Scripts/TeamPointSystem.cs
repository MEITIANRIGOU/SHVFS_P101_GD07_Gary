using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  Class_3
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T instance = null;

        public static T Instance
        {
            get
            {
                if (instance != null) return instance;

                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    instance = new GameObject(typeof(T).Name).AddComponent<T>();
                }

                DontDestroyOnLoad(instance.gameObject);

                return instance;
            }
        }
    }



    //public List<Team> teams = new List<Team>();
}