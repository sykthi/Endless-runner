using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 1 * Time.deltaTime * 100f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "player")
        {
            GameManager.noOfCoins += 1;
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().playSound("coin");
        }
    }
}
