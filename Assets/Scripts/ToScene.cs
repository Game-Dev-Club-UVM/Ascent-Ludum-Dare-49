using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToScene : MonoBehaviour
{
    public void ToMenu(){
         SceneManager.LoadScene("Main Menu");
    }
     public void ToGame(){
         SceneManager.LoadScene("mainScene");
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
			ToMenu();
		}
	}
}
