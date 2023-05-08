using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    Pool<Collectable> poolOfCoins;
    public ColisionController CContoller;
    public float rotSpeed = 50;
    public float useTime = 20;
    public float countTime = 0;

    public void Init(Pool<Collectable> _pool)
    {
        poolOfCoins = _pool;
        //TryGetComponent(out CContoller);
        Debug.Log("Subscribing colliderHandler");
        CContoller.collisionEnter += ColliderHandler;
        countTime = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right * rotSpeed * Time.deltaTime);
        countTime += Time.deltaTime;
        if(countTime >= useTime)
        {
            RecyclingCoin();
        }
    }

    public void ColliderHandler(Collider objetCollided)
    {
        if (objetCollided.TryGetComponent(out ThirdPersonMovement player))
        {
            GameManager.OnlyInstance.AddCoins(1);
            Debug.Log(" ");
            Debug.Log("Coing collected");
            RecyclingCoin();
            Debug.Log("Recicle la moneda");
            
        }Debug.Log("you ain't collliding a coin" + objetCollided);
    }
    public void RecyclingCoin()
    {
        poolOfCoins.RecycleItem(this);
        gameObject.SetActive(false);
        CContoller.collisionEnter -= ColliderHandler;
    }


}
