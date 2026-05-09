using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;

namespace LXY.Scripts;

[Pool(typeof(TokenCardPool))]
public class BaoLieLiMing: CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(50m, ValueProp.Move)];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    public BaoLieLiMing() : base(0, CardType.Attack, CardRarity.Token, TargetType.AnyEnemy,true)
    {
    }
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
		ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
		await using AttackContext context = await AttackCommand.CreateContextAsync(base.CombatState, choiceContext , this);
		List<DamageResult> list = (await CreatureCmd.Damage(choiceContext, cardPlay.Target, base.DynamicVars.Damage.BaseValue, ValueProp.Move, this)).ToList();
		await CreatureCmd.Stun(cardPlay.Target);
		context.AddHit(list);
		DamageResult damageResult = list.FirstOrDefault();
		if (damageResult != null)
		{
			List<Creature> list2 = (from e in base.CombatState.GetTeammatesOf(damageResult.Receiver)where e.IsHittable select e).ToList();
			if (list2.Count != 0)
			{
				AttackContext attackContext = context;
				attackContext.AddHit(await CreatureCmd.Damage(choiceContext, list2, (damageResult.TotalDamage + damageResult.OverkillDamage) >> 1, ValueProp.Unpowered | ValueProp.Move, base.Owner.Creature, this));
			}
		}
    }
	protected override void OnUpgrade()
	{
		base.DynamicVars.Damage.UpgradeValueBy(10m);
	}
}
