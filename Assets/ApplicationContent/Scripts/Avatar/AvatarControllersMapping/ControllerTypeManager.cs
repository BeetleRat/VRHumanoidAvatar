using System.Collections.Generic;
using UnityEngine;
/**
Класс, отвечающий за переключение типов контроллеров на сервере

Данный класс используется в prefab-е игрока на сервере.
Когда локальный игрок меняет тип контроллера (с контроллеров на руки или наоборот),
данный класс изменяет меняет отображаемый тип контроллера на сервере.
@param controllers Список переключаемых контроллеров.
@see ControllerModel
 */
/// <summary>
/// <para>Class responsible for switching avatar controller types.</para>
/// This class is used in the prefab of the player's avatar.
/// When the local player changes controller type (from controllers to hands or vice versa),
/// this class changes the avatar's displayed controller type.
/// <param name="controllers">the list of switchable controllers</param>
/// </summary>
public sealed class ControllerTypeManager : MonoBehaviour
{
    [SerializeField] private List<ControllerModel> _controllers;

    private ControllerType _currentControllerType;
    private ControllerModel _currentController;

    private void Start()
    {
        SwitchControllerView(_currentControllerType);
    }

    private void OnDestroy()
    {
        _controllers.Clear();
    }

    /// <summary>
    /// <para>Switch the controller type to the specified controller type.</para>
    /// </summary>
    /// <param name="controllerType">the specified controller type</param>
    public void SwitchControllerView(ControllerType controllerType)
    {
        _currentControllerType = controllerType;
        if (_currentController == null || _currentController.Type != controllerType)
        {
            foreach (ControllerModel controller in _controllers)
            {
                if (controller.Type == controllerType)
                {
                    controller.SetActive(true);
                    _currentController = controller;
                }
                else
                {
                    controller.SetActive(false);
                }
            }
        }
    }
}
