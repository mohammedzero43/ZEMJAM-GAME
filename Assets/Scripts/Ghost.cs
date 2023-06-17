using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Ghost : MonoBehaviour
{


    Vector3[] ghost_steps;
    public float record_timestep = 0.2;
    bool recording = true;
    bool playing = false;
    int current_frame = 0;
    //var player_tween :Tween;  
    float recorder_timer;//:Timer = Timer.new();
    float playback_timer;//:Timer = Timer.new();

    // Start is called before the first frame update
    void Start()
    {

	Globals.ghost =  this;
	recording = true;
	print("started");
	
	ghost_steps=new Vector3[];
    }


    func on_recorder_timeout(){
        if (recording){
            ghost_steps.append(Globals.player.global_position);
            Debug.Log("recorded");
        }
        else{
            recorder_timer.stop();
        }
    }
    func on_playback_timer(){
        if (playing)
        {
            if (current_frame < ghost_steps.size()){
                if (player_tween!= null)  player_tween.stop();
                player_tween = create_tween() ;
                player_tween.tween_property(Globals.ghost,"global_position",ghost_steps[current_frame],record_timestep);
                current_frame+=1;
            }
            else { 
                playback_timer.stop();
            }
        }
    }
    func _on_end_reached(Collider body){
        playing=false;
        recording= false;
    }



    func _on_start_point_body_entered(Collider body){
        
        if (ghost_steps.size()<=0){
            add_child(recorder_timer);
            recorder_timer.timeout.connect(on_recorder_timeout);
            recorder_timer.start(record_timestep);
            playing = false;
            recording = true;
        }
        else{
            if (!playing){
                add_child(playback_timer);
                playback_timer.timeout.connect(on_playback_timer);
                playback_timer.start(record_timestep);
                playing =true;
                recording = false;
                Debug.Log("started playback");
            }
        }
    }
}




/*

# Called when the node enters the scene tree for the first time.

*/