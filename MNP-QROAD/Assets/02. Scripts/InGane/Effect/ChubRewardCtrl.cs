using UnityEngine;
using System.Collections;
using DG.Tweening;
using PathologicalGames;

public class ChubRewardCtrl : MonoBehaviour {

	public int _value;

    readonly string spriteFish = "ico_fish_go";
    readonly string spriteTicket = "freeticket";

    [SerializeField]
    tk2dSprite _sprite;

    public void OnSpawned() {
		
		// 임의의 방향으로 AddForce
		this.GetComponent<Rigidbody>().velocity = Vector3.zero;
		this.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-500,500), Random.Range(800,1200), 0));
		
	
	}
	
    public void SpawnChub() {
        _sprite.SetSprite(spriteFish);
        GameSystem.Instance.InGameChub++;
        Invoke("OnCompleteMove", 3);
    }

    public void SpawnTicket() {
        _sprite.SetSprite(spriteTicket);
        GameSystem.Instance.InGameTicket++;
        Invoke("OnCompleteMove", 3);
    }
	
	public void OnCompleteMove() {
		PoolManager.Pools [PuzzleConstBox.objectPool].Despawn (this.transform);
	}
}
