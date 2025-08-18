using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameProject1.Utilities.Configs;
using System;

namespace MonoGameProject1;

public class Text : ConfigurableComponent
{
    public TextConfig textConfig { get; set; }
    
    // Effect state variables
    private float totalTime = 0f;
    private float shakeTimer = 0f;
    private float fadeTimer = 0f;
    private float scaleTimer = 0f;
    private float bounceTimer = 0f;
    private int typewriterIndex = 0;
    private float typewriterTimer = 0f;
    private Vector2 shakeOffset = Vector2.Zero;

    public Text() : base()
    {
    }

    public override void Initialize<T>(T config)
    {
        if (config is TextConfig textConfig)
        {
            this.textConfig = textConfig;
            
            // Initialize typewriter effect
            if (textConfig.EffectSettings.EnableTypewriter)
            {
                textConfig.EffectSettings.FullText = textConfig.Text;
                textConfig.Text = "";
                typewriterIndex = 0;
                typewriterTimer = 0f;
            }
            
            base.Initialize(config);
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        if (textConfig?.EffectSettings == null) return;
        
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        totalTime += deltaTime;
        
        UpdateEffects(deltaTime);
    }

    private void UpdateEffects(float deltaTime)
    {
        var effects = textConfig.EffectSettings;
        
        // Update shake effect
        if (effects.EnableShakeEffect && shakeTimer > 0)
        {
            shakeTimer -= deltaTime;
            float intensity = effects.ShakeIntensity * (shakeTimer / effects.ShakeDuration);
            shakeOffset = new Vector2(
                (float)(Random.Shared.NextDouble() - 0.5) * intensity * 2,
                (float)(Random.Shared.NextDouble() - 0.5) * intensity * 2
            );
        }
        else
        {
            shakeOffset = Vector2.Zero;
        }
        
        // Update fade effect
        if (effects.EnableFadeEffect)
        {
            fadeTimer += deltaTime;
            if (effects.FadeIn)
            {
                effects.FadeAlpha = MathHelper.Clamp(fadeTimer / effects.FadeDuration, 0f, 1f);
            }
            else
            {
                effects.FadeAlpha = MathHelper.Clamp(1f - (fadeTimer / effects.FadeDuration), 0f, 1f);
            }
        }
        
        // Update typewriter effect
        if (effects.EnableTypewriter && !string.IsNullOrEmpty(effects.FullText))
        {
            typewriterTimer += deltaTime;
            int targetIndex = (int)(typewriterTimer * effects.TypewriterSpeed);
            if (targetIndex > typewriterIndex && typewriterIndex < effects.FullText.Length)
            {
                typewriterIndex = Math.Min(targetIndex, effects.FullText.Length);
                textConfig.Text = effects.FullText.Substring(0, typewriterIndex);
            }
        }
        
        // Update bounce effect
        if (effects.EnableBounceEffect && bounceTimer > 0)
        {
            bounceTimer -= deltaTime;
        }
    }

    protected override void OnEnable()
    {
    }

    protected override void OnDisable()
    {
    }

    public void TriggerShake()
    {
        if (textConfig?.EffectSettings?.EnableShakeEffect == true)
        {
            shakeTimer = textConfig.EffectSettings.ShakeDuration;
        }
    }
    
    public void TriggerBounce()
    {
        if (textConfig?.EffectSettings?.EnableBounceEffect == true)
        {
            bounceTimer = textConfig.EffectSettings.BounceDuration;
        }
    }
    
    public void StartFade(bool fadeIn = true)
    {
        if (textConfig?.EffectSettings?.EnableFadeEffect == true)
        {
            textConfig.EffectSettings.FadeIn = fadeIn;
            fadeTimer = 0f;
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (textConfig == null || string.IsNullOrEmpty(textConfig.Text)) return;
        
        var effects = textConfig.EffectSettings;
        Vector2 basePosition = gameObject.Position + shakeOffset;
        Color finalColor = textConfig.Color;
        Vector2 finalScale = gameObject.Scale;
        
        // Apply fade effect
        if (effects.EnableFadeEffect)
        {
            finalColor = textConfig.Color * effects.FadeAlpha;
        }
        
        // Apply color cycling
        if (effects.EnableColorCycle && effects.ColorPalette?.Length > 0)
        {
            float cycleTime = totalTime * effects.ColorCycleSpeed;
            int colorIndex = (int)cycleTime % effects.ColorPalette.Length;
            int nextColorIndex = (colorIndex + 1) % effects.ColorPalette.Length;
            float lerp = cycleTime - (int)cycleTime;
            finalColor = Color.Lerp(effects.ColorPalette[colorIndex], effects.ColorPalette[nextColorIndex], lerp);
        }
        
        // Apply pulse effect
        if (effects.EnablePulseEffect)
        {
            float pulse = 1f + (float)Math.Sin(totalTime * effects.PulseSpeed) * effects.PulseAmount;
            finalScale *= pulse;
        }
        
        // Apply scale effect (for combo scaling)
        if (effects.EnableScaleEffect)
        {
            // You can implement combo-based scaling here
            // This is just a basic implementation
            finalScale *= effects.BaseScale;
        }
        
        // Apply bounce effect
        Vector2 bounceOffset = Vector2.Zero;
        if (effects.EnableBounceEffect && bounceTimer > 0)
        {
            float bounceProgress = 1f - (bounceTimer / effects.BounceDuration);
            float bounceHeight = (float)Math.Sin(bounceProgress * Math.PI) * effects.BounceHeight;
            bounceOffset.Y = -bounceHeight;
        }
        
        Vector2 finalPosition = basePosition + bounceOffset;
        
        // Draw shadow if enabled
        if (effects.EnableShadow)
        {
            DrawTextWithEffects(spriteBatch, finalPosition + effects.ShadowOffset, effects.ShadowColor * (finalColor.A / 255f), finalScale);
        }
        
        // Draw outline if enabled
        if (effects.EnableOutline)
        {
            for (int x = -effects.OutlineThickness; x <= effects.OutlineThickness; x++)
            {
                for (int y = -effects.OutlineThickness; y <= effects.OutlineThickness; y++)
                {
                    if (x != 0 || y != 0)
                    {
                        DrawTextWithEffects(spriteBatch, finalPosition + new Vector2(x, y), effects.OutlineColor * (finalColor.A / 255f), finalScale);
                    }
                }
            }
        }
        
        // Draw main text
        if (effects.EnableWaveEffect && effects.WavePerCharacter)
        {
            DrawWaveTextPerCharacter(spriteBatch, finalPosition, finalColor, finalScale);
        }
        else if (effects.EnableWaveEffect)
        {
            float waveOffset = (float)Math.Sin(totalTime * effects.WaveSpeed) * effects.WaveAmplitude;
            DrawTextWithEffects(spriteBatch, finalPosition + new Vector2(0, waveOffset), finalColor, finalScale);
        }
        else
        {
            DrawTextWithEffects(spriteBatch, finalPosition, finalColor, finalScale);
        }
    }
    
    private void DrawTextWithEffects(SpriteBatch spriteBatch, Vector2 position, Color color, Vector2 scale)
    {
        spriteBatch.DrawString(
            textConfig.Font,
            textConfig.Text,
            position,
            color,
            MathHelper.ToRadians(gameObject.Rotation),
            textConfig.TextCenter,
            scale,
            textConfig.SpriteEffects,
            textConfig.LayerDepth
        );
    }
    
    private void DrawWaveTextPerCharacter(SpriteBatch spriteBatch, Vector2 basePosition, Color color, Vector2 scale)
    {
        Vector2 charPosition = basePosition - textConfig.TextCenter * scale;
        
        for (int i = 0; i < textConfig.Text.Length; i++)
        {
            char character = textConfig.Text[i];
            
            // Calculate wave offset for this character
            float waveOffset = (float)Math.Sin((totalTime * textConfig.EffectSettings.WaveSpeed) + (i * textConfig.EffectSettings.WaveFrequency)) * textConfig.EffectSettings.WaveAmplitude;
            Vector2 finalCharPosition = charPosition + new Vector2(0, waveOffset);
            
            spriteBatch.DrawString(
                textConfig.Font,
                character.ToString(),
                finalCharPosition,
                color,
                MathHelper.ToRadians(gameObject.Rotation),
                Vector2.Zero,
                scale,
                textConfig.SpriteEffects,
                textConfig.LayerDepth
            );
            
            // Move to next character position
            Vector2 charSize = textConfig.Font.MeasureString(character.ToString()) * scale;
            charPosition.X += charSize.X;
        }
    }
}