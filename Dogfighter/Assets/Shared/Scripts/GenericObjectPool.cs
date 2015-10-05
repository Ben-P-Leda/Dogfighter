using UnityEngine;

namespace Shared.Scripts
{
    public abstract class GenericObjectPool<T> : MonoBehaviour
    {
        private T[] _objects;

        public GameObject ObjectPrefab;
        public int PoolSize;

        protected virtual void Awake()
        {
            _objects = new T[PoolSize];

            for (int i = 0; i < PoolSize; i++)
            {
                GameObject newObject = (GameObject)Instantiate(ObjectPrefab);
                newObject.transform.parent = transform;
                _objects[i] = newObject.GetComponent<T>();
            }
        }

        protected virtual void Start()
        {
            for (int i = 0; i < PoolSize; i++) { StartObject(_objects[i]); }
        }

        protected virtual void StartObject(T sourceGameObject) { }

        protected T GetFirstAvailableObject()
        {
            T firstAvailable = default(T);
            for (int i = 0; ((i < PoolSize) && (firstAvailable == null)); i++)
            {
                if (ObjectIsAvailable(_objects[i])) { firstAvailable = _objects[i]; }
            }

            return firstAvailable;
        }

        protected abstract bool ObjectIsAvailable(T objectToCheck);

        protected bool HasActiveObjects()
        {
            bool activeObjects = false;
            for (int i = 0; ((i < PoolSize) && (!activeObjects)); i++)
            {
                if (!ObjectIsAvailable(_objects[i])) { activeObjects = true; }
            }

            return activeObjects;
        }
    }
}
