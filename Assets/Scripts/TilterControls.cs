// GENERATED AUTOMATICALLY FROM 'Assets/TilterControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @TilterControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @TilterControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""TilterControls"",
    ""maps"": [
        {
            ""name"": ""Tilter"",
            ""id"": ""3d6c6db4-094e-4074-a054-7b4247b3dff1"",
            ""actions"": [
                {
                    ""name"": ""Tilt"",
                    ""type"": ""Value"",
                    ""id"": ""9127e31a-fbe4-43b6-85af-8708eecd0a70"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e42faf8e-ef70-4ab2-a000-2a8de86aee94"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tilt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""e9c7e946-45ab-429d-99ce-d14534831508"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tilt"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9cf73923-c4c8-4442-b04f-7211fe01dfda"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Gamepad"",
                    ""action"": ""Tilt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""31c212ea-b9d4-4937-88fd-8fd2fdb57eee"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Gamepad"",
                    ""action"": ""Tilt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b7fe4068-19b2-45ed-a185-cb2c97c181d3"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Gamepad"",
                    ""action"": ""Tilt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""be95ec86-2420-4c3a-9154-849145c7e788"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Gamepad"",
                    ""action"": ""Tilt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Cannon"",
            ""id"": ""df70784e-b94f-4e42-9ac1-0aa6f0e4bf35"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""671910cd-945e-4825-9747-1f7566fc52ba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""87a47245-5b53-48e9-8ed9-ad1f91fd3495"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""537f6e78-ce03-4193-9264-b82b55d10199"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d94bc9bd-c6df-4997-80b3-99a54698d2bc"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0e79b43e-aa12-4d05-93a3-3f975302528f"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Gamepad"",
            ""bindingGroup"": ""Keyboard and Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Tilter
        m_Tilter = asset.FindActionMap("Tilter", throwIfNotFound: true);
        m_Tilter_Tilt = m_Tilter.FindAction("Tilt", throwIfNotFound: true);
        // Cannon
        m_Cannon = asset.FindActionMap("Cannon", throwIfNotFound: true);
        m_Cannon_Newaction = m_Cannon.FindAction("New action", throwIfNotFound: true);
        m_Cannon_Shoot = m_Cannon.FindAction("Shoot", throwIfNotFound: true);
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

    // Tilter
    private readonly InputActionMap m_Tilter;
    private ITilterActions m_TilterActionsCallbackInterface;
    private readonly InputAction m_Tilter_Tilt;
    public struct TilterActions
    {
        private @TilterControls m_Wrapper;
        public TilterActions(@TilterControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Tilt => m_Wrapper.m_Tilter_Tilt;
        public InputActionMap Get() { return m_Wrapper.m_Tilter; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TilterActions set) { return set.Get(); }
        public void SetCallbacks(ITilterActions instance)
        {
            if (m_Wrapper.m_TilterActionsCallbackInterface != null)
            {
                @Tilt.started -= m_Wrapper.m_TilterActionsCallbackInterface.OnTilt;
                @Tilt.performed -= m_Wrapper.m_TilterActionsCallbackInterface.OnTilt;
                @Tilt.canceled -= m_Wrapper.m_TilterActionsCallbackInterface.OnTilt;
            }
            m_Wrapper.m_TilterActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Tilt.started += instance.OnTilt;
                @Tilt.performed += instance.OnTilt;
                @Tilt.canceled += instance.OnTilt;
            }
        }
    }
    public TilterActions @Tilter => new TilterActions(this);

    // Cannon
    private readonly InputActionMap m_Cannon;
    private ICannonActions m_CannonActionsCallbackInterface;
    private readonly InputAction m_Cannon_Newaction;
    private readonly InputAction m_Cannon_Shoot;
    public struct CannonActions
    {
        private @TilterControls m_Wrapper;
        public CannonActions(@TilterControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_Cannon_Newaction;
        public InputAction @Shoot => m_Wrapper.m_Cannon_Shoot;
        public InputActionMap Get() { return m_Wrapper.m_Cannon; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CannonActions set) { return set.Get(); }
        public void SetCallbacks(ICannonActions instance)
        {
            if (m_Wrapper.m_CannonActionsCallbackInterface != null)
            {
                @Newaction.started -= m_Wrapper.m_CannonActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_CannonActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_CannonActionsCallbackInterface.OnNewaction;
                @Shoot.started -= m_Wrapper.m_CannonActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_CannonActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_CannonActionsCallbackInterface.OnShoot;
            }
            m_Wrapper.m_CannonActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
            }
        }
    }
    public CannonActions @Cannon => new CannonActions(this);
    private int m_KeyboardandGamepadSchemeIndex = -1;
    public InputControlScheme KeyboardandGamepadScheme
    {
        get
        {
            if (m_KeyboardandGamepadSchemeIndex == -1) m_KeyboardandGamepadSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Gamepad");
            return asset.controlSchemes[m_KeyboardandGamepadSchemeIndex];
        }
    }
    public interface ITilterActions
    {
        void OnTilt(InputAction.CallbackContext context);
    }
    public interface ICannonActions
    {
        void OnNewaction(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
    }
}
