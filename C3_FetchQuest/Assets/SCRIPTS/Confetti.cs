using UnityEngine;

public class Confetti : MonoBehaviour
{
    public ParticleSystem pSystem1;
    public ParticleSystem pSystem2;

    public void PlayEffect()
    {
        pSystem1.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        pSystem1.Play();

        pSystem2.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        pSystem2.Play();
    }


}
