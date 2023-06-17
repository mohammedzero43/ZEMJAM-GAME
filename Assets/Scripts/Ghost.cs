using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Ghost : MonoBehaviour
{

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

    

    // Start is called before the first frame update
    void Start()
    {

	    recording = false;
        playing = false;
	    print("started");
	
	    ghost_steps=new List<Vector3>();
    }




    public void _on_end_reached(){


        playing =false;
        recording= false;
    }



    public void _on_start_point_body_entered(){
        if (ghost_steps.Count<=0){
            playing = false;
            recording = true;
            StartCoroutine(RecordingTimer());
        }
        else{
            if (!playing){
                playing =true;
                recording = false;
                Debug.Log("started playback");
                StartCoroutine(PlaybackTimer());
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