using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusScore : MonoBehaviour {
    private int bonusScore = 0;
    public Text bonusText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Enemy")){
            if (other.GetComponent<EnemyController>().GetPassed() == false)
            {
                bonusScore += 10;
                bonusText.text = "BonusScore:" + bonusScore.ToString() + "점";
            }
        }
    }
}
