using System;
using Microsoft.Xna.Framework;
using MonoGameProject1.Core;
using MonoGameProject1.UI;
using MonoGameProject1.Utilities.Configs;
using MonoGameProject1.Visuals;

namespace MonoGameProject1;

public static class ComboManager
{
    public static ComboText comboText;

    public static DepthMeter depthMeter;

    public const int MAXCOMBO = 10;
    private const int SCOREFISH = 100;
    private const float MAXCOMBOTIME = 3.0f;
    private const float DEPTHUNIT = 10f;

    private static int score = 0;
    private static int combo = 0;
    private static int lastCombo = 0;
    private static float comboTimer = 0;
    private static float currentDepth = 0f;
    private static float totalDistance = 0f;

    public static int TotalFishCaught { get; private set; } = 0;
    private static float MaxDepthReached { get; set; } = 0f;
    public static int HighestCombo { get; set; } = 0;
    public static int BestScore { get; private set; } = 0;

    public static float CurrentDepth => currentDepth;

    private static void UpdateFishCaught() => TotalFishCaught++;

    private static void UpdateMaxDepth(float depth)
    {
        if (depth > MaxDepthReached)
            MaxDepthReached = depth;
    }

    private static void UpdateHighestCombo(int combo)
    {
        if (combo > HighestCombo)
            HighestCombo = combo;
    }

    private static void UpdateBestScore(int score)
    {
        if (score > BestScore)
            BestScore = score;
    }

    public static void Reset()
    {
        score = 0;
        combo = 0;
        lastCombo = 0;
        comboTimer = 0;
        currentDepth = 0f;
        totalDistance = 0f;
        TotalFishCaught = 0;
        MaxDepthReached = 0f;
        HighestCombo = 0;
        BestScore = 0;
    }

    public static void IncreaseCombo(Fish fish = null, bool isAboomnpha = false)
    {
        if (isAboomnpha)
        {
            combo = 0;
            return;
        }

        if (combo >= MAXCOMBO) return;

        lastCombo = combo;
        combo++;
        comboTimer = MAXCOMBOTIME;

        UpdateHighestCombo(combo);
        UpdateFishCaught();

        int fishScore = SCOREFISH * combo;
        score = (TotalFishCaught * fishScore) + (int)currentDepth;
        UpdateBestScore(score);

        comboText.Text.TriggerShake();
        comboText.Text.TriggerBounce();

        if (combo >= MAXCOMBO / 2)
        {
            comboText.TextConfig.EffectSettings.WaveAmplitude = 8f;
            comboText.TextConfig.EffectSettings.ColorCycleSpeed = 3f;
        }
    }

    public static void UpdateDepth(float speed, float deltaTime)
    {
        float depthIncrement = (speed / 100f) * deltaTime;
        totalDistance += depthIncrement;
        currentDepth = totalDistance / DEPTHUNIT;

        UpdateMaxDepth(currentDepth);

        int fishScore = SCOREFISH * (combo > 0 ? combo : 1);
        score = (TotalFishCaught * fishScore) + (int)currentDepth;
    }

    public static void UpdateScore(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        comboTimer -= deltaTime;
        if (comboTimer <= 0 && combo > 0)
        {
            combo = 0;
            comboTimer = 0;

            comboText.TextConfig.EffectSettings.WaveAmplitude = 5f;
            comboText.TextConfig.EffectSettings.ColorCycleSpeed = 2f;
        }

        UpdateUITexts();

        float scaleMultiplier = 1f + ((float)combo / MAXCOMBO) * (comboText.TextConfig.EffectSettings.MaxScale - 1f);
        comboText.TextConfig.EffectSettings.BaseScale = scaleMultiplier;
    }

    private static void UpdateUITexts()
    {
        // Update combo text
        comboText.TextConfig.Text = $"Combo: {combo}x";


        // Update depth meter using the new method
        if (depthMeter != null)
        {
            depthMeter.UpdateDepthDisplay(currentDepth);
        }
    }
}