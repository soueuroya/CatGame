using UnityEngine;

public class Death : MonoBehaviour
{
    public AudioSource levelMusic;
    public AudioSource deathSong;

    public bool levelSong = true;
    public bool DeathSong = false;

    public void LevelMusic()
    {
        levelSong = true;
        DeathSong = false;
        levelMusic.Play();
    }

    public void DeathSound()
    {
        if (levelMusic.isPlaying)
            levelSong = false;
        {
            levelMusic.Stop();
        }
        if (!deathSong.isPlaying && DeathSong == false)
        {
            deathSong.Play();
            DeathSong = true;
        }
    }
}
