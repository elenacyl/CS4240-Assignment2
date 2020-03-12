using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{

	public Text countText;
	public Text winText;

	public static int count;
    // Start is called before the first frame update
    void Start()
    {
    	count = 0;
    	SetCountText ();
        winText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        SetCountText ();
    }

    void SetCountText()
    {
    	countText.text = "Score: " + count.ToString ();
        if (count >= 6)
        {
            winText.text = "You Win!";
        }
    }
}
