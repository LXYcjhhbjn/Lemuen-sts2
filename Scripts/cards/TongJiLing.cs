using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class TongJiLing: CustomCardModel
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<TongJi>(1m)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<AIM>(),
        HoverTipFactory.FromPower<TongJi>()
    ];
	public TongJiLing() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
        if (CombatState.HittableEnemies != null)
        {
            await PowerCmd.Apply<TongJi>(choiceContext,CombatState.HittableEnemies, base.Owner.Creature.GetPowerAmount<AIM>() >> 2, base.Owner.Creature, null);
        }
	}

	protected override void OnUpgrade()
	{
		base.EnergyCost.UpgradeBy(-1);
	}
}