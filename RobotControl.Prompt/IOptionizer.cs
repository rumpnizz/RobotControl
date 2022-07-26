// See https://aka.ms/new-console-template for more information

namespace RobotControl.Prompt;

public interface IOptionizer<TEnum>
    where TEnum : Enum
{
    public TEnum PrintOptions();
}


