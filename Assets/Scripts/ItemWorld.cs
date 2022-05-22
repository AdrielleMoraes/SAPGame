//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;

//public class ItemWorld : MonoBehaviour
//{

//    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
//    {
//        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

//        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
//        itemWorld.SetItem(item);

//        return itemWorld;
//    }

//    private Item item;
//    private SpriteRenderer spriteRenderer;

//    public UnityEvent swordPickup;

//    private void Awake()
//    {
//        spriteRenderer = GetComponent<SpriteRenderer>();

//    }
//    public void SetItem(Item item)
//    {
//        this.item = item;
//        spriteRenderer.sprite = item.GetSprite();
//    }

//    public Item GetItem()
//    {
//        return item;
//    }

//    private void OnTriggerEnter2D(Collider2D collider)
//    {
//        if(collider.gameObject.tag == "Player")
//        {
//            swordPickup.Invoke();

//            DestroySelf();
//        }
//    }
//    public void DestroySelf()
//    {
//        Destroy(gameObject);
//    }
//}
