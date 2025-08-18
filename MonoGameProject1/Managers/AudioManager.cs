using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace MonoGameProject1;
public static class AudioManager
{
    private static Dictionary<string, Song> songsCache = new();
    private static Dictionary<string, SoundEffect> SFXCache = new();
    public static ContentManager ContentMan { set; get; }    
    
    public static void RegisterSong(string songFileName)
    {
        if (songsCache.ContainsKey(songFileName)) return;

        Song song = ContentMan.Load<Song>(Path.Combine("Audio/Songs", songFileName));

        songsCache.Add(songFileName, song);
    }

    public static Song PlaySong(string songFileName)
    {
        Song song;
        bool fetchedFromCache = false;
        if (!songsCache.TryGetValue(songFileName, out song))
        {
            song = ContentMan.Load<Song>(Path.Combine("Audio/Songs", songFileName));
        }
        else
        {
            fetchedFromCache = true;
        }
        MediaPlayer.Play(song);

        if (!fetchedFromCache) songsCache[songFileName] = song;

        return song;
    }

    public static void RegisterSFX(string fileName)
    {
        if (SFXCache.ContainsKey(fileName)) return;

        SoundEffect SFX = ContentMan.Load<SoundEffect>(Path.Combine("Audio/SFX", fileName));

        SFXCache.Add(fileName, SFX);
    }

    private static SoundEffect CreateSFX(string SFXFileName) // helper method to create or fetch SFX
    {
        SoundEffect SFX;
        bool fetchedFromCache = false;
        if (!SFXCache.TryGetValue(SFXFileName, out SFX))
            SFX = ContentMan.Load<SoundEffect>(Path.Combine("Audio/SFX", SFXFileName));
        else fetchedFromCache = true;

        if (!fetchedFromCache) SFXCache[SFXFileName] = SFX;

        return SFX;
    }

    public static SoundEffectInstance CreateSFXInstanceAndPlay(string filaName)
    {
        var SFX = CreateSFX(filaName);
        var SFXInstance = SFX.CreateInstance();
        SFXInstance.Play();
        return SFXInstance;
    }
    
    private static SoundEffectInstance CreateSFXInstance(SoundEffect originalSound) => originalSound.CreateInstance();
}