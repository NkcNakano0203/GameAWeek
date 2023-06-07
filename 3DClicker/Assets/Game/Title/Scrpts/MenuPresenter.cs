using UnityEngine;
using UniRx;

namespace TitleUI
{
    public class MenuPresenter : MonoBehaviour
    {
        [SerializeField]
        MenuModel menuModel;
        [SerializeField]
        MenuView menuView;

        void Start()
        {
            menuView.StartButton.Subscribe(x =>
            {
                Debug.Log("start");
                menuModel.GameStart();
            });

            menuView.QuitButton.Subscribe(x =>
            {
                Debug.Log("Quit");
                menuModel.GameQuit();
            });
        }
    }
}