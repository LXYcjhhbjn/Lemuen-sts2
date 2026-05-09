using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace LXY.Scripts;

public sealed class DiQiTinShuJiPower : CustomPowerModel
{
	public override PowerType Type => PowerType.Buff;
	public override PowerStackType StackType => PowerStackType.Counter;
    public override bool AllowNegative => false;
    public override string? CustomPackedIconPath => "res://lemuen/images/powers/DiQiTinShuJiPower.png";
    public override string? CustomBigIconPath => "res://lemuen/images/powers/DiQiTinShuJiPower.png";
	public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext,PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
	{
		if (applier == base.Owner && power is AMMO && amount > 0)
		{
			await PowerCmd.Apply<PlatingPower>(choiceContext, base.Owner, base.Amount, base.Owner, null);
		}
	}
}
