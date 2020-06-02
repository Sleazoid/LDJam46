// GENERATED AUTOMATICALLY FROM 'Assets/Input/inputControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""inputControls"",
    ""maps"": [
        {
            ""name"": ""Gamepad"",
            ""id"": ""5d42f52d-e57d-4e44-8cde-4e2ec9afa0b7"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""0f4bd7bb-f430-43c6-9053-ca2b8a32e548"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftStick"",
                    ""type"": ""Value"",
                    ""id"": ""2038bd8a-9856-4c12-9d9e-4013e3bc6165"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""b3aba1f5-2444-4cd9-bced-262d257084ed"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PrepareAim"",
                    ""type"": ""Button"",
                    ""id"": ""731d3bba-bc51-405e-962b-4dd55fe91698"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ClimbBtn"",
                    ""type"": ""Button"",
                    ""id"": ""0fbb53d2-5b9c-48cf-92db-2d9dd20b3a29"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Roll"",
                    ""type"": ""Button"",
                    ""id"": ""f18a02ce-2c7b-4a76-a404-9ab9fadc50ff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Restart"",
                    ""type"": ""Button"",
                    ""id"": ""9eea4ac7-efb7-4a9c-aa39-54feeba0cc90"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""b150bd11-fdca-4d4f-8e48-4372ebae85ef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""34aaf5b1-4fc4-42f0-b292-631e404bc013"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5e3006e-ff5e-4c1e-ab1e-2ca3cf8aedd8"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b91cecbe-a936-4d1f-b1dd-7e2f09ec6971"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c367099b-5160-46d6-8bd0-4c0b5168a574"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrepareAim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0bcf2334-9387-4573-804b-3edacdc99153"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClimbBtn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ce8a08b2-724d-4447-9e38-e1cc9cfa173f"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""33838dfa-5b1c-44b5-9f07-e1f379677fc3"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Restart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cae73a8c-20c6-4cbc-b5bb-fe9ed6590613"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gamepad
        m_Gamepad = asset.FindActionMap("Gamepad", throwIfNotFound: true);
        m_Gamepad_Jump = m_Gamepad.FindAction("Jump", throwIfNotFound: true);
        m_Gamepad_LeftStick = m_Gamepad.FindAction("LeftStick", throwIfNotFound: true);
        m_Gamepad_Shoot = m_Gamepad.FindAction("Shoot", throwIfNotFound: true);
        m_Gamepad_PrepareAim = m_Gamepad.FindAction("PrepareAim", throwIfNotFound: true);
        m_Gamepad_ClimbBtn = m_Gamepad.FindAction("ClimbBtn", throwIfNotFound: true);
        m_Gamepad_Roll = m_Gamepad.FindAction("Roll", throwIfNotFound: true);
        m_Gamepad_Restart = m_Gamepad.FindAction("Restart", throwIfNotFound: true);
        m_Gamepad_Interaction = m_Gamepad.FindAction("Interaction", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Gamepad
    private readonly InputActionMap m_Gamepad;
    private IGamepadActions m_GamepadActionsCallbackInterface;
    private readonly InputAction m_Gamepad_Jump;
    private readonly InputAction m_Gamepad_LeftStick;
    private readonly InputAction m_Gamepad_Shoot;
    private readonly InputAction m_Gamepad_PrepareAim;
    private readonly InputAction m_Gamepad_ClimbBtn;
    private readonly InputAction m_Gamepad_Roll;
    private readonly InputAction m_Gamepad_Restart;
    private readonly InputAction m_Gamepad_Interaction;
    public struct GamepadActions
    {
        private @InputControls m_Wrapper;
        public GamepadActions(@InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Gamepad_Jump;
        public InputAction @LeftStick => m_Wrapper.m_Gamepad_LeftStick;
        public InputAction @Shoot => m_Wrapper.m_Gamepad_Shoot;
        public InputAction @PrepareAim => m_Wrapper.m_Gamepad_PrepareAim;
        public InputAction @ClimbBtn => m_Wrapper.m_Gamepad_ClimbBtn;
        public InputAction @Roll => m_Wrapper.m_Gamepad_Roll;
        public InputAction @Restart => m_Wrapper.m_Gamepad_Restart;
        public InputAction @Interaction => m_Wrapper.m_Gamepad_Interaction;
        public InputActionMap Get() { return m_Wrapper.m_Gamepad; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GamepadActions set) { return set.Get(); }
        public void SetCallbacks(IGamepadActions instance)
        {
            if (m_Wrapper.m_GamepadActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_GamepadActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GamepadActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GamepadActionsCallbackInterface.OnJump;
                @LeftStick.started -= m_Wrapper.m_GamepadActionsCallbackInterface.OnLeftStick;
                @LeftStick.performed -= m_Wrapper.m_GamepadActionsCallbackInterface.OnLeftStick;
                @LeftStick.canceled -= m_Wrapper.m_GamepadActionsCallbackInterface.OnLeftStick;
                @Shoot.started -= m_Wrapper.m_GamepadActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_GamepadActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_GamepadActionsCallbackInterface.OnShoot;
                @PrepareAim.started -= m_Wrapper.m_GamepadActionsCallbackInterface.OnPrepareAim;
                @PrepareAim.performed -= m_Wrapper.m_GamepadActionsCallbackInterface.OnPrepareAim;
                @PrepareAim.canceled -= m_Wrapper.m_GamepadActionsCallbackInterface.OnPrepareAim;
                @ClimbBtn.started -= m_Wrapper.m_GamepadActionsCallbackInterface.OnClimbBtn;
                @ClimbBtn.performed -= m_Wrapper.m_GamepadActionsCallbackInterface.OnClimbBtn;
                @ClimbBtn.canceled -= m_Wrapper.m_GamepadActionsCallbackInterface.OnClimbBtn;
                @Roll.started -= m_Wrapper.m_GamepadActionsCallbackInterface.OnRoll;
                @Roll.performed -= m_Wrapper.m_GamepadActionsCallbackInterface.OnRoll;
                @Roll.canceled -= m_Wrapper.m_GamepadActionsCallbackInterface.OnRoll;
                @Restart.started -= m_Wrapper.m_GamepadActionsCallbackInterface.OnRestart;
                @Restart.performed -= m_Wrapper.m_GamepadActionsCallbackInterface.OnRestart;
                @Restart.canceled -= m_Wrapper.m_GamepadActionsCallbackInterface.OnRestart;
                @Interaction.started -= m_Wrapper.m_GamepadActionsCallbackInterface.OnInteraction;
                @Interaction.performed -= m_Wrapper.m_GamepadActionsCallbackInterface.OnInteraction;
                @Interaction.canceled -= m_Wrapper.m_GamepadActionsCallbackInterface.OnInteraction;
            }
            m_Wrapper.m_GamepadActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @LeftStick.started += instance.OnLeftStick;
                @LeftStick.performed += instance.OnLeftStick;
                @LeftStick.canceled += instance.OnLeftStick;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @PrepareAim.started += instance.OnPrepareAim;
                @PrepareAim.performed += instance.OnPrepareAim;
                @PrepareAim.canceled += instance.OnPrepareAim;
                @ClimbBtn.started += instance.OnClimbBtn;
                @ClimbBtn.performed += instance.OnClimbBtn;
                @ClimbBtn.canceled += instance.OnClimbBtn;
                @Roll.started += instance.OnRoll;
                @Roll.performed += instance.OnRoll;
                @Roll.canceled += instance.OnRoll;
                @Restart.started += instance.OnRestart;
                @Restart.performed += instance.OnRestart;
                @Restart.canceled += instance.OnRestart;
                @Interaction.started += instance.OnInteraction;
                @Interaction.performed += instance.OnInteraction;
                @Interaction.canceled += instance.OnInteraction;
            }
        }
    }
    public GamepadActions @Gamepad => new GamepadActions(this);
    public interface IGamepadActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnLeftStick(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnPrepareAim(InputAction.CallbackContext context);
        void OnClimbBtn(InputAction.CallbackContext context);
        void OnRoll(InputAction.CallbackContext context);
        void OnRestart(InputAction.CallbackContext context);
        void OnInteraction(InputAction.CallbackContext context);
    }
}
