using BaseLib.Abstracts;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LXY.Scripts;
public class AIM: CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override bool AllowNegative => false;
    public override string? CustomPackedIconPath => "res://lemuen/images/powers/Aim.png";
    public override string? CustomBigIconPath => "res://lemuen/images/powers/Aim.png";
	public override decimal ModifyDamageAdditive(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
	{
		if (base.Owner != dealer)
		{
			return 0m;
		}
		if (!props.IsPoweredAttack_())
		{
			return 0m;
		}
		if (base.Owner.HasPower<ZhuanZhu>())
		{
			return base.Amount+base.Amount*(base.Owner.GetPowerAmount<ZhuanZhu>()*0.25m);
		}
		else
		{
			return base.Amount;
		}
	}

	public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext, Creature? dealer, DamageResult result, ValueProp props, Creature target, CardModel? cardSource)
	{
		if (dealer != null && (dealer == base.Owner || dealer.PetOwner?.Creature == base.Owner) && props.IsPoweredAttack_() && !(cardSource is GuiXiangYaoYue))
		{
			if (base.Owner.HasPower<LunYiQiangShenPower>())
			{
				await PowerCmd.Apply<AIM>(choiceContext,base.Owner, -((base.Amount+3) >> 2) , base.Owner, null, silent: true);
			}
			else
			{
				await PowerCmd.Apply<AIM>(choiceContext,base.Owner, -((base.Amount+1) >> 1) , base.Owner, null, silent: true);
			}
		}
	}
}