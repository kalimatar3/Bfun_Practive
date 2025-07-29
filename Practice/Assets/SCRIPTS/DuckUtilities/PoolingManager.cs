using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace DuckGame.Ultilities
{
    public class PoolingManager : Singleton<PoolingManager>
    {
        private ObjectPool<GameObject> poolRagdoll;
        public ObjectPool<GameObject> poolSkid;
        [SerializeField] GameObject ragdollPrefab;
        [SerializeField] GameObject skidPrefab;
        [SerializeField] private int defaultCapacityRagdoll;
        [SerializeField] private int maxCapacityRagdoll;
        [SerializeField] private int defaultCapacitySkid;
        [SerializeField] private int maxCapacitySkid;

        private void Start() {
            poolRagdoll = new ObjectPool<GameObject>(() => {
                return Instantiate(ragdollPrefab);
            }, ragdoll => { ragdoll.SetActive(true);
            }, ragdoll => { ragdoll.SetActive(false);
            }, ragdoll => { Destroy(ragdoll);
            }, false, defaultCapacityRagdoll, maxCapacityRagdoll);

            poolSkid = new ObjectPool<GameObject>(() => {
                return Instantiate(skidPrefab);
            }, skid => { skid.SetActive(true);
            }, skid => { skid.SetActive(false);
            }, skid => { Destroy(skid);
            }, false, defaultCapacitySkid, maxCapacitySkid);
        }

        public GameObject MakeRagDoll(Vector3 pos, Quaternion rot)
        {
            GameObject ragdoll = poolRagdoll.Get();
            ragdoll.transform.position = pos;
            ragdoll.transform.rotation = rot;
            return ragdoll;
        }
    }
}
