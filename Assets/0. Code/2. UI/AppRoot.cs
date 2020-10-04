namespace Messiah.UI {
  using UnityEngine;
  using Logic.GameCoreNS;
  using Logic;

  public class AppRoot : MonoBehaviour {
    public ViewManager viewManager;

    async void Start() {
      DontDestroyOnLoad(gameObject);

#if UNITY_EDITOR
      gameObject.ToggleSiblings(false);
#elif DEVELOPMENT_BUILD
      await viewManager.SetupDebugPanel();
#endif

      GameCore.Init();
      await LuaManager.Init();
      await viewManager.Init();

      AutoLogin();
      if (GameCore.userData.currentGameData != null)
        GameCore.FAM.Fire(GameStateTrigger.FoundLastGameData);
      else
        GameCore.FAM.Fire(GameStateTrigger.NoLastGameData);
    }

    void AutoLogin() {
      GameCore.userData = UserData.GetLastUser();
    }


  }

  [XLua.CSharpCallLua]
  public interface T {
    string k { get; set; }
    int number { get; set; }
    void log();
  }
}