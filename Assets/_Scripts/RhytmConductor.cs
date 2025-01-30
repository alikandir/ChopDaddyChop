using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhytmConductor : MonoBehaviour
{
    // resource paper: www.gamedeveloper.com/audio/coding-to-the-beat---under-the-hood-of-a-rhythm-game-in-unity
    [SerializeField] private float _songBpm;
    private float _secPerBeat; // Number of seconds for each beat
    private float _songPosition;
    private float _songPositionInBeats;
    private float _dspSongTime;
    private AudioSource _musicSource;
    [SerializeField] private float _firstBeatOffset; // if the start of the song is silent, this will help to sync the beats
    
    public static RhytmConductor instance;
    //Loop Related
    [SerializeField] private float _beatsPerLoop;
    private int _completedLoops=0;
    private float _loopPositionInBeats;
    private float _loopPositionInNormalized; // position in loop between 0 and 1.0
    private bool _songStarted;
    [SerializeField] private ButtonImageHandler _buttonImageHandler;
    private void Awake() {
        instance = this;
    }
    private void Start() {
        _musicSource = GetComponent<AudioSource>();
        _musicSource.loop = true;
    }
    public void SetAudioClip(AudioClip clip, float bpm, float firstBeatOffset, float beatsPerLoop){
        _musicSource.clip = clip;
        _songBpm = bpm;
        _firstBeatOffset = firstBeatOffset;
        _beatsPerLoop = beatsPerLoop;
    }
    public void StartSong(){
        _secPerBeat = 60f / _songBpm;
        _dspSongTime = (float) AudioSettings.dspTime;
        _songStarted = true;
        _buttonImageHandler.SecPerBeat = _secPerBeat;
        _musicSource.Play();
    }
    public void StopSong(){
        _musicSource.Stop();
    }

    private void Update() {
        if (!_songStarted) return;
        _songPosition = (float) (AudioSettings.dspTime - _dspSongTime - _firstBeatOffset); // Current time of the song
        _songPositionInBeats = _songPosition / _secPerBeat; // Current position in beats

        //Loop Related
        if (_songPositionInBeats >= (_completedLoops + 1) * _beatsPerLoop){
            _completedLoops++;
        }
        _loopPositionInBeats = _songPositionInBeats - (_completedLoops * _beatsPerLoop);

        _loopPositionInNormalized = _loopPositionInBeats / _beatsPerLoop;
    }


}
