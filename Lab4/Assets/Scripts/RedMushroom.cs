using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedMushroom : MonoBehaviour, ConsumableInterface
{
	public  Texture t;
    private int index = 1;
	public  void  consumedBy(GameObject player){
		// give player jump boost
		player.GetComponent<PlayerController>().upSpeed  +=  10;
		StartCoroutine(removeEffect(player));
	}

	IEnumerator  removeEffect(GameObject player){
		yield  return  new  WaitForSeconds(5.0f);
		player.GetComponent<PlayerController>().upSpeed  -=  10;
	}
    void  OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player")){
            // update UI
            CentralManager.centralManagerInstance.addPowerup(t, index, this);
            GetComponent<Collider2D>().enabled  =  false;
        }
    }

}
