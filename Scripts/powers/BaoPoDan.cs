using BaseLib.Abstracts;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LXY.Scripts;
public class BaoPoDan: CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
	protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(5m, ValueProp.Unpowered | ValueProp.Move)];
    public override string? CustomPackedIconPath => "res://lemuen/images/powers/BaoPoDan.png";
    public override string? CustomBigIconPath => "res://lemuen/images/powers/BaoPoDan.png";
	public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
	{
		if (base.Owner != dealer)
		{
			return 1m;
		}
		if (cardSource == null)
		{
			return 1m;
		}
		if (!props.IsPoweredAttack_())
		{
			return 1m;
		}
		return 1.5m;
	}

	public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext, Creature? dealer, DamageResult result, ValueProp props, Creature target, CardModel? cardSource)
	{
		if (dealer != null && (dealer == base.Owner || dealer.PetOwner?.Creature == base.Owner) && props.IsPoweredAttack_() && result.TotalDamage > 0)
		{
        	if (base.Owner.CombatState.HittableEnemies != null)
        	{
           		await CreatureCmd.Damage(choiceContext, base.Owner.CombatState.HittableEnemies, base.DynamicVars.Damage, base.Owner);
        	}
			await PowerCmd.Remove<BaoPoDan>(base.Owner);
		}
	}
}