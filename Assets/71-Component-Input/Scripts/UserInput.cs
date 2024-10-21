public class UserInput
{
    static UserInputActions _actions;

    static UserInputActions actions
    {
        get
        {
            if (_actions == null)
            {
                _actions = new UserInputActions();
                _actions.Enable();
            }

            return _actions;
        }
    }

    public static float PlaneRoll
    {
        get
        {
            return actions.Move.Roll.ReadValue<float>();
        }
    }

    public static float PlaneRollMouse
    {
        get
        {
            return actions.Move.RollMouse.ReadValue<float>();
        }
    }


    public static float PlaneYaw
    {
        get
        {
            return actions.Move.Yaw.ReadValue<float>();
        }
    }

    public static float PlanePitch
    {
        get
        {
            return actions.Move.Pitch.ReadValue<float>();
        }
    }

    public static float PlanePitchMouse
    {
        get
        {
            return actions.Move.PitchMouse.ReadValue<float>();
        }
    }

    public static float PlaneAcceleration
    {
        get
        {
            return actions.Move.Acceleration.ReadValue<float>();
        }
    }
}