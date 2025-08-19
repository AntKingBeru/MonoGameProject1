using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace MonoGameProject1;
public static class AudioManager
{
    private static Dictionary<string, Song> songs = new();
    private static Dictionary<string, SoundEffect> SFXs = new();
    public static ContentManager ContentMan { set; get; }    
    
    public static void RegisterSong(string key, string songFilePath)
    {
        if (songs.ContainsKey(key)) return;
        Song song = ContentMan.Load<Song>(songFilePath);
        songs.Add(key, song);
    }

    public static Song PlaySong(string key)
    {
        Song song;
        if (!songs.TryGetValue(key, out song)) return null;
        MediaPlayer.Play(song);
        MediaPlayer.IsRepeating = true; // Set to true if you want the song to loop
        return song;
    }

    public static void RegisterSFX(string key, string songFilePath)
    {
        if (SFXs.ContainsKey(key)) return;

        SoundEffect SFX = ContentMan.Load<SoundEffect>(songFilePath);

        SFXs.Add(key, SFX);
    }

    private static SoundEffect CreateSFX(string key) // helper method to create or fetch SFX
    {
        SoundEffect SFX;
        
        if (!SFXs.TryGetValue(key, out SFX)) return null;
        
        return SFX;
    }

    public static SoundEffectInstance CreateSFXInstanceAndPlay(string key)
    {
        var SFX = CreateSFX(key);
        if (SFX == null) return null;
        var SFXInstance = SFX.CreateInstance();
        SFXInstance.Play();
        return SFXInstance;
    }
    
    private static SoundEffectInstance CreateSFXInstance(SoundEffect originalSound) => originalSound.CreateInstance();
}