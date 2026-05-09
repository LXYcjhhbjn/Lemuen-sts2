using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Powers;
namespace LXY.Scripts;

public class AMMO: CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override bool AllowNegative => false;
    public override string? CustomPackedIconPath => "res://lemuen/images/powers/Ammo.png";
    public override string? CustomBigIconPath => "res://lemuen/images/powers/Ammo.png";
}