using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour, IObserver
{
  [SerializeField] private Subject _playerSubject;
  [SerializeField] private int _playerHealth = 3;

  void Awake()
  {
    _playerSubject = 
      GameObject.FindGameObjectWithTag("Player").
      GetComponent<Subject>();
  }
  void OnEnable()
  {
    _playerSubject.AddObserver(this);
  }
  void OnDisable()
  {
    _playerSubject.RemoveObserver(this);
  }
  public void OnNotify(PlayerEnums playerEnums)
  {
    switch (playerEnums)
    {
      case PlayerEnums.Died:
        PlayerDied();
        break;
      default:
        break;
    }
    
  }
  private void PlayerDied()
  {
    _playerHealth -= 1;
    if (_playerHealth <= 0)
    {
      Debug.Log($"Player truly died");
      SceneController.Instance.ChangeSceneByName("GameOver");
    }
  }

  public void SaveGameIntoFile()
  {
    SaveGameManager.Instance().SaveGame(_playerSubject.transform);
  }
}
