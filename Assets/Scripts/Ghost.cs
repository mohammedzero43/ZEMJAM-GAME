using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Ghost : MonoBehaviour
{
    Vector3 StartPos;
    public GameObject player;
    public GameObject ghost;
    List<Vector3> ghost_steps= new List<Vector3>();
    public float record_timestep = 0.2f;
    bool recording = false;
    bool playing = false;
    int current_frame = 0;
    //var player_tween :Tween;  
    float recorder_timer;//:Timer = Timer.new();
    float playback_timer;//:Timer = Timer.new();
    bool secondphase = false;

    [HideInInspector] public float Timer = 0;
    public float GameTimer = 60;
    public float WinTimer = 0;
    public static int gold = 0;
    bool TimerStarted = false;
    bool isFinishedGame = false;
    public TMP_Text timer1, timer2;
    // Start is called before the first frame update
    void Start()
    {
        StartPos = player.transform.position;
	    recording = false;
        playing = false;
	    print("started");
	
	    ghost_steps=new List<Vector3>();
    }


    private void Update()
    {
        if (TimerStarted) Timer += Time.deltaTime;
        if (Timer > GameTimer)
        {
            print("GAMEOVER");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        timer1.text = ""+Timer;
        timer2.text = ""+GameTimer;
    }

    public void _on_end_reached(){

        isFinishedGame = true;
        if (secondphase)
            TimerStarted = false;
        else {
            secondphase = true;
        playing = false;
            recording = false;
            ShopManager.coins += (int)(GameTimer - Timer);

            WinTimer = Timer;
            GameTimer = WinTimer;
            TimerStarted = false;
            Timer = 0;

            player.transform.position = StartPos; 
        }
    }



    public void _on_start_point_body_entered() {
        TimerStarted = true;
        if (ghost_steps.Count<=0){
            playing = false;
            recording = true;
            StartCoroutine(RecordingTimer());
        }
        else{
            if (isFinishedGame)
            {
                if (!playing)
                {
                    playing = true;
                    recording = false;
                    Debug.Log("started playback");
                    StartCoroutine(PlaybackTimer());
                }
            }
        }
    }

    public IEnumerator RecordingTimer()
    {
        while (recording)
        {
            ghost_steps.Add(player.transform.position);
            Debug.Log("RECORDED FRAME");
            yield return new WaitForSeconds(record_timestep);
        }
    }

    public IEnumerator PlaybackTimer()
    {
        while (playing)
        {
            if (current_frame < ghost_steps.Count)
            {
                if (GetComponent<iTween>() != null) Destroy(GetComponent<iTween>());
                iTween.MoveTo(ghost, ghost_steps[current_frame], record_timestep);
                current_frame += 1;
                yield return new WaitForSeconds(record_timestep);
            }
            else
                break;
        }
    }
}




/*

# Called when the node enters the scene tree for the first time.

*/