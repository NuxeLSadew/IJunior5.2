using UnityEngine.Events;

public interface IHasHealth
{
    public int GetMaxHealth();
    public int GetCurrentHealth();
    public void AddListenerOnChangeHealthEvent(UnityAction unityAction);
}
