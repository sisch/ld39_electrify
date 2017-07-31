using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class WinningScreenScript : MonoBehaviour
{

    public Text Score;

    public Text Comment;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (!ScoreManager.Instance.isRunning)
	    {
	        int score = (int)ScoreManager.Instance.score;
	        string insert = "";
	        if (score < 300)
	        {
	            insert = "try again!";
	        } else if (score < 500)
	        {
	            insert = "keep up the good work!";
	        } else if (score >= 500)
	        {
	            insert = "fight the sword master!";
	        }
	        Score.text = string.Format("Your score: {0:000}", ScoreManager.Instance.score);
	        Comment.text = string.Format("You should {0}", insert);
	    }
	}

    public void MenuClicked()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartClicked()
    {
        GameManager.Instance.Initialize();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextClicked()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }
}
