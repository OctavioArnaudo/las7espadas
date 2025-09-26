public class InitMixer : InitMesh
{
    public virtual void SetVolume(string parameterName, bool mute)
    {
        mixer.SetFloat(parameterName, mute ? -80f : 0f);
    }

    public virtual void MuteMusic(bool mute) => SetVolume("MusicVolume", mute);
    public virtual void MuteSfx(bool mute) => SetVolume("SFXVolume", mute);
}