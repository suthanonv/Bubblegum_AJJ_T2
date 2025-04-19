using UnityEngine;

public class RockMovingSound : MonoBehaviour
{
    public void PlayRockSound()
    {
        SoundManager.PlaySound(SoundType.Effect_RockMove, 1f);
    }
}
