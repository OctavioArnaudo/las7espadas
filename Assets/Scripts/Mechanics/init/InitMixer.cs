using System.Collections;
using System;

[Serializable]
public class InitMixer : InitMesh
{
    public virtual IEnumerator SetVolume(string parameterName, bool mute)
    {
        mixer.SetFloat(parameterName, mute ? -80f : 0f);
        yield return null;
    }

    public virtual IEnumerator MuteMusic(bool mute) => SetVolume("MusicVolume", mute);
    public virtual IEnumerator MuteSfx(bool mute) => SetVolume("SFXVolume", mute);
}