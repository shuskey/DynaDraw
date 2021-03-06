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
                    ""processors"": ""Scale"",
                    ""groups"": ""Keyboard and Gamepad"",
                    ""action"": ""Tilt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""35b654f9-72e8-413b-b0c7-a35dd3e6b69f"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tilt"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7a531683-3255-4b4d-87ca-72447b255cb2"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Gamepad"",
                    ""action"": ""Tilt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2833a115-882f-40f6-b78c-1e6eaf50f5c4"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Gamepad"",
                    ""action"": ""Tilt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""dc850f3b-80eb-40db-93ac-c7ba9b9e43cf"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Gamepad"",
                    ""action"": ""Tilt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""76f10ee2-98ee-4ec6-b008-458cbeba6ac5"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Gamepad"",
                    ""action"": ""Tilt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""4e3d337e-6866-4a14-9e1d-2452ade14ee1"",
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
                    ""id"": ""8e3c2aaa-2850-47b9-b022-195902bf1c8c"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Gamepad"",
                    ""action"": ""Tilt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0064b87b-d236-4a0d-9cca-70225b591ae2"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Gamepad"",
                    ""action"": ""Tilt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e06a0d1d-338b-4e80-80e3-5b203a9f5558"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Gamepad"",
                    ""action"": ""Tilt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""36bb16f3-68a4-4414-9353-bfd94d1c95aa"",
                    ""path"": ""<Gamepad>/dpad/right"",
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
                    ""id"": ""000c54a8-caa5-4e04-b79f-b5c58468aca7"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Gamepad"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""baf61f77-fd19-4de7-ac8c-92e7582a35d9"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Gamepad"",
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
    private readonly InputAction m_Cannon_Shoot;
    public struct CannonActions
    {
        private @TilterControls m_Wrapper;
        public CannonActions(@TilterControls wrapper) { m_Wrapper = wrapper; }
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
                @Shoot.started -= m_Wrapper.m_CannonActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_CannonActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_CannonActionsCallbackInterface.OnShoot;
            }
            m_Wrapper.m_CannonActionsCallbackInterface = instance;
            if (instance != null)
            {
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
        void OnShoot(InputAction.CallbackContext context);
    }
}
