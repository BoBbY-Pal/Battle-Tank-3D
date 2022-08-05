using System.Collections.Generic;
using UnityEngine;
public class ServicePool<T>: MonoGenericSingleton<ServicePool<T>> where T: class
{
    private List<PooledItem<T>> pooledItems = new List<PooledItem<T>>();
    public virtual T GetItem()
    {
        if(pooledItems.Count > 0)
        {
            PooledItem<T> item = pooledItems.Find(i => i.b_IsUsed == false);
            if (item != null)
            {
                item.b_IsUsed = true;
                return item.Item;
            }
        }
        return CreateNewPooledItem();
    }
    
    private T CreateNewPooledItem()
    {
        {
            PooledItem<T> pooledItem = new PooledItem<T>
            {
                Item = CreateItem(),
                b_IsUsed = true
            };
            pooledItems.Add(pooledItem);
            Debug.Log("New item added to the pool " + pooledItems.Count);
            return pooledItem.Item;
        }
    }

    public virtual void ReturnItem(T item)
    {
        PooledItem<T> pooledItem = pooledItems.Find(i => i.Item.Equals(item));
        pooledItem.b_IsUsed = false;
    }
    
    protected virtual T CreateItem()
    {
        return(T)null;
    }

    private class PooledItem<T>
    {
        public T Item;
        public bool b_IsUsed;
    }
}
