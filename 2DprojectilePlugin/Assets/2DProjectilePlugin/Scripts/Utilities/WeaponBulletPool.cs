using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UpGamesWeapon2D.Weapons
{
    public class BulletPoolInstance
    {
        static GameObject masterPool;
        List<GameObject> pool;
        GameObject pooledObject;
        public GameObject parent;
        Weapon2DBase checker;
        public BulletPoolInstance(GameObject _pooledObject, int poolSize, Weapon2DBase _checker, string weaponName = "")
        {
            checker = _checker;
            if (checker.enabled)
            {
                if (masterPool == null)
                {
                    masterPool = new GameObject("BulletPools");
                }
                parent = new GameObject(_pooledObject.name + "_" + weaponName + "_BulletPool");
                parent.transform.SetParent(masterPool.transform);
                pool = new List<GameObject>();
                pooledObject = _pooledObject;
                for (int i = 0; i < poolSize; i++)
                {
                    GameObject go = MonoBehaviour.Instantiate(_pooledObject);
                    go.transform.SetParent(parent.transform);
                    go.SetActive(false);
                    pool.Add(go);
                }
            }
       
        }
        public GameObject SpawnObjectAt(Transform trans, Quaternion rotation)
        {

            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].activeInHierarchy)
                {
                    pool[i].SetActive(true);
                    pool[i].transform.rotation = rotation;
                    pool[i].transform.position = trans.position;
                    return pool[i];
                }
            }

            if (checker.enabled)
            {
                GameObject go = MonoBehaviour.Instantiate(pooledObject, trans.position, rotation);
                go.transform.SetParent(parent.transform);
                pool.Add(go);
                return go;
            }
            else
            {
                return null;
            }
         
            
            
        }
        public void DestructionSequence()
        {
            MonoBehaviour.FindObjectOfType<Weapon2D>().StartCoroutine(Destruction());           
        }
        IEnumerator Destruction()
        {
            while (HasActiveMembers)
            {
                yield return new WaitForSeconds(2);
            }
            
            MonoBehaviour.Destroy(parent);

        }
        bool HasActiveMembers
        {
            get
            {
                for(int i= 0; i< pool.Count; i++)
                {
                    if (pool[i].activeInHierarchy)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}

