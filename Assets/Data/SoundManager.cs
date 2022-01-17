using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SoundManager")]
public class SoundManager : ScriptableObject
{
    public AudioClip mainGameSound;
    public AudioClip startMenuSound;
    public AudioClip gameOverSound;
    public AudioClip doorSlamSound;
    public AudioClip doorOpenSound;
    public AudioClip characterHurtSound;
    public AudioClip merchantSound;
    public AudioClip knightSound;
}
