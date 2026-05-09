using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Powers;
namespace LXY.Scripts;

public class LunYiQiangShenPower: CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    public override string? CustomPackedIconPath => "res://lemuen/images/powers/LunYiQiangShenPower.png";
    public override string? CustomBigIconPath => "res://lemuen/images/powers/LunYiQiangShenPower.png";
}